using System.Data;
using System.Text.RegularExpressions;
using BagproWebAPI.Models;
using BagproWebAPI.Models.Dtos;
using BagproWebAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace BagproWebAPI.Controllers
{
    /// <summary>
    /// Creación de items desde Plasticaribe. Al insertar en ClientesOT_Item, el trigger TR_CreacionProductosEnPBDD
    /// crea el producto, la existencia, el inventario mensual y la relación con el cliente en Plasticaribe.
    /// </summary>
    public partial class ClientesOtItemsController
    {
        private const string UsuarioCreacion = "APP PLASTICARIBE";
        private const string RecursoBloqueo = "CrearClienteItem";
        private const int TiempoBloqueoMs = 10000;
        private static readonly Regex NombreBaseValido = new(@"^[A-Za-z0-9_]+$", RegexOptions.Compiled);
        private static readonly Regex ServidorValido = new(@"^[A-Za-z0-9_.\-]+$", RegexOptions.Compiled);

        /// <summary>
        /// Indica si el item de la ficha ya existe por nombre normalizado o por ficha técnica.
        /// </summary>
        /// <param name="dto">Ficha del item.</param>
        /// <returns>200 { duplicado:false } o 409 { tipo:"nombre"|"ficha", codigo, nombre }.</returns>
        [HttpPost("validarDuplicado")]
        public async Task<IActionResult> ValidarDuplicado([FromBody] CrearItemDto dto)
        {
            var duplicado = await BuscarDuplicadoAsync(dto);
            return duplicado != null ? Conflict(duplicado) : Ok(new { duplicado = false });
        }

        /// <summary>
        /// Crea un item con una presentación. En una transacción: bloqueo de numeración (sp_getapplock),
        /// revalidación de duplicados, código = mayor entre MAX(ClienteItems), MAX(Productos.Prod_Id) y MAX(Articulo.Codigo de Zeus) + 1, comprobación de que el código no exista
        /// en Plasticaribe (Productos) e inserción de una fila en ClientesOT_Item.
        /// </summary>
        /// <param name="dto">Ficha del item.</param>
        /// <returns>201 { clienteItems, nombre, presentacion }; 400 si un catálogo no existe; 409 si hay duplicado, la numeración está ocupada o el código ya existe en Plasticaribe.</returns>
        [HttpPost("crear")]
        public async Task<IActionResult> CrearItem([FromBody] CrearItemDto dto)
        {
            string? baseDatos = ObtenerBasePlasticaribe();
            if (baseDatos == null) return Problem("Falta la configuración CreacionItems:BasePlasticaribe o no es válida.");

            var zeus = ObtenerZeus();
            if (zeus == null) return Problem("Falta la configuración CreacionItems:ServidorZeus / CreacionItems:BaseZeus o no es válida.");

            var item = await ConstruirFilaAsync(dto);
            if (item == null) return ValidationProblem(ModelState);

            // Máximo de Zeus fuera de la transacción, para no involucrar al servidor vinculado en ella. Las altas web
            // se serializan con el bloqueo sobre BagPro; Zeus solo evita reutilizar códigos que ya existen allá.
            int maxZeus;
            try { maxZeus = await MaxArticuloZeusAsync(zeus.Value.Servidor, zeus.Value.Base); }
            catch (SqlException ex) { return Problem($"No se pudo consultar el consecutivo de Zeus: {ex.Message}", statusCode: StatusCodes.Status502BadGateway); }

            var estrategia = _context.Database.CreateExecutionStrategy();
            return await estrategia.ExecuteAsync<IActionResult>(async () =>
            {
                _context.ChangeTracker.Clear();
                await using var transaccion = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

                if (!await ObtenerBloqueoAsync()) return Conflict(new { mensaje = "Numeración ocupada, reintente." });

                var duplicado = await BuscarDuplicadoAsync(dto);
                if (duplicado != null) return Conflict(duplicado);

                // El consecutivo es el mayor entre BagPro, Plasticaribe y Zeus, por si las bases se desfasan.
                int maxBagPro = await _context.ClientesOtItems.MaxAsync(c => c.ClienteItems) ?? 0;
                int maxPlasticaribe = await MaxProductoPlasticaribeAsync(baseDatos);
                int codigo = Math.Max(Math.Max(maxBagPro, maxPlasticaribe), maxZeus) + 1;
                if (await ExisteProductoPlasticaribeAsync(baseDatos, codigo))
                    return Conflict(new { mensaje = $"El código {codigo} ya existe en Plasticaribe (Productos). Revise la numeración antes de reintentar.", codigo });

                item.Id = 0;
                item.ClienteItems = codigo;
                _context.ClientesOtItems.Add(item);
                try
                {
                    await _context.SaveChangesAsync();
                    await transaccion.CommitAsync();
                }
                catch (DbUpdateException ex)
                {
                    return Problem(ex.InnerException?.Message ?? ex.Message);
                }

                var respuesta = new { clienteItems = codigo, nombre = item.ClienteItemsNom, presentacion = item.PtPresentacionNom };
                return Created($"api/ClientesOtItems/{codigo}/ficha", respuesta);
            });
        }

        /// <summary>
        /// Agrega una presentación a un item existente copiando la primera fila del item (menor Id) y cambiando
        /// la presentación y el precio.
        /// </summary>
        /// <param name="codigo">Código del item (ClienteItems).</param>
        /// <param name="dto">Presentación y precio.</param>
        /// <returns>201 { clienteItems, nombre, presentacion }; 404 si el item no existe; 409 si la presentación ya existe o la numeración está ocupada.</returns>
        [HttpPost("{codigo:int}/presentaciones")]
        public async Task<IActionResult> AgregarPresentacion(int codigo, [FromBody] AgregarPresentacionDto dto)
        {
            if (!NombreItemNormalizador.TryPresentacion(dto.Presentacion, out string presentacion, out int codigoPresentacion))
            {
                ModelState.AddModelError(nameof(dto.Presentacion), "Presentación no válida. Use Kilo, Unidad, Paquete o MTS.");
                return ValidationProblem(ModelState);
            }

            var estrategia = _context.Database.CreateExecutionStrategy();
            return await estrategia.ExecuteAsync<IActionResult>(async () =>
            {
                _context.ChangeTracker.Clear();
                await using var transaccion = await _context.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

                if (!await ObtenerBloqueoAsync()) return Conflict(new { mensaje = "Numeración ocupada, reintente." });

                var origen = await _context.ClientesOtItems.AsNoTracking()
                    .Where(c => c.ClienteItems == codigo)
                    .OrderBy(c => c.Id)
                    .FirstOrDefaultAsync();
                if (origen == null) return NotFound(new { mensaje = $"El item {codigo} no existe." });

                bool existe = await _context.ClientesOtItems.AsNoTracking()
                    .AnyAsync(c => c.ClienteItems == codigo && c.PtPresentacionNom!.Trim() == presentacion);
                if (existe) return Conflict(new { mensaje = $"El item {codigo} ya tiene la presentación {presentacion}.", codigo, presentacion });

                decimal precio = Math.Round(dto.Precio ?? 0, 2);
                origen.Id = 0;
                origen.Item = 0;
                origen.PtPresentacion = codigoPresentacion.ToString();
                origen.PtPresentacionNom = presentacion;
                origen.DatosvalorBolsa = precio;
                origen.DatosValorKg = presentacion == "Kilo" ? precio : null;
                origen.UsrCrea = UsuarioCreacion;
                origen.FechaCrea = DateTime.Now;
                origen.FechaModifica = DateTime.Now;
                _context.ClientesOtItems.Add(origen);
                try
                {
                    await _context.SaveChangesAsync();
                    await transaccion.CommitAsync();
                }
                catch (DbUpdateException ex)
                {
                    return Problem(ex.InnerException?.Message ?? ex.Message);
                }

                var respuesta = new { clienteItems = codigo, nombre = origen.ClienteItemsNom.Trim(), presentacion };
                return Created($"api/ClientesOtItems/{codigo}/presentaciones", respuesta);
            });
        }

        /// <summary>
        /// Lista las presentaciones que tiene un item en ClientesOT_Item.
        /// </summary>
        /// <param name="codigo">Código del item (ClienteItems).</param>
        /// <returns>200 [{ presentacion, codigoPresentacion, precio, nombre, clienteNom }] o 404 si el item no existe.
        /// El nombre y el cliente son los guardados en la primera fila de cada presentación.</returns>
        [HttpGet("{codigo:int}/presentaciones")]
        public async Task<IActionResult> GetPresentaciones(int codigo)
        {
            var filas = await _context.ClientesOtItems.AsNoTracking()
                .Where(c => c.ClienteItems == codigo)
                .OrderBy(c => c.Id)
                .Select(c => new { c.PtPresentacionNom, c.PtPresentacion, c.DatosvalorBolsa, c.ClienteItemsNom, c.ClienteNom })
                .ToListAsync();
            if (filas.Count == 0) return NotFound(new { mensaje = $"El item {codigo} no existe." });

            var presentaciones = filas
                .GroupBy(f => (f.PtPresentacionNom ?? string.Empty).Trim())
                .Select(g => new
                {
                    presentacion = g.Key,
                    codigoPresentacion = g.First().PtPresentacion?.Trim(),
                    precio = g.First().DatosvalorBolsa,
                    nombre = g.First().ClienteItemsNom?.Trim(),
                    clienteNom = g.First().ClienteNom?.Trim(),
                })
                .ToList();
            return Ok(presentaciones);
        }

        /// <summary>
        /// Busca items por nombre (contiene) o por código (empieza por), para los autocompletados de la creación de items.
        /// </summary>
        /// <param name="texto">Texto a buscar; mínimo 3 caracteres.</param>
        /// <returns>200 con máximo 20 items [{ codigo, nombre }] distintos por código, del más reciente al más antiguo;
        /// 400 si el texto tiene menos de 3 caracteres.</returns>
        [HttpGet("buscar")]
        public async Task<IActionResult> Buscar([FromQuery] string? texto)
        {
            texto = texto?.Trim() ?? string.Empty;
            if (texto.Length < 3) return BadRequest(new { mensaje = "Digite al menos 3 caracteres." });

            bool esNumero = texto.All(char.IsDigit);
            var items = await _context.ClientesOtItems.AsNoTracking()
                .Where(c => c.ClienteItems != null
                    && (c.ClienteItemsNom.Contains(texto) || (esNumero && c.ClienteItems.ToString()!.StartsWith(texto))))
                .GroupBy(c => c.ClienteItems!.Value)
                .Select(g => new { codigo = g.Key, nombre = g.Min(c => c.ClienteItemsNom) })
                .OrderByDescending(x => x.codigo)
                .Take(20)
                .ToListAsync();

            return Ok(items.Select(x => new { x.codigo, nombre = x.nombre?.Trim() }));
        }

        /// <summary>
        /// Devuelve la ficha técnica de la primera fila del item (menor Id), con la forma de CrearItemDto
        /// más los nombres de catálogo, para "Copiar desde item".
        /// </summary>
        /// <param name="codigo">Código del item (ClienteItems).</param>
        /// <returns>200 con la ficha o 404 si el item no existe.</returns>
        [HttpGet("{codigo:int}/ficha")]
        public async Task<IActionResult> GetFicha(int codigo)
        {
            var c = await _context.ClientesOtItems.AsNoTracking()
                .Where(x => x.ClienteItems == codigo)
                .OrderBy(x => x.Id)
                .FirstOrDefaultAsync();
            if (c == null) return NotFound(new { mensaje = $"El item {codigo} no existe." });

            var tintas = new[] { c.ImpTinta1, c.ImpTinta2, c.ImpTinta3, c.ImpTinta4, c.ImpTinta5, c.ImpTinta6, c.ImpTinta7, c.ImpTinta8 }
                .Select(EnteroONulo).Where(t => t is > 0).Select(t => t!.Value).ToList();
            var capas = new[]
            {
                (Capa: c.LamCapa1, Calibre: c.LamCalibre1, Cantidad: c.Cant1),
                (Capa: c.LamCapa2, Calibre: c.LamCalibre2, Cantidad: c.Cant2),
                (Capa: c.LamCapa3, Calibre: c.LamCalibre3, Cantidad: c.Cant3),
            }
            .Where(x => EnteroONulo(x.Capa) is > 0)
            .Select(x => new { capa = EnteroONulo(x.Capa), calibre = x.Calibre, cantidad = x.Cantidad })
            .ToList();

            return Ok(new
            {
                clienteItems = c.ClienteItems,
                cliente = c.Cliente,
                clienteNom = c.ClienteNom?.Trim(),
                nombre = c.ClienteItemsNom?.Trim(),
                formato = EnteroONulo(c.PtFormatopt),
                formatoNom = c.PtFormatoptNom?.Trim(),
                formatoExtrusion = EnteroONulo(c.ExtFormato),
                formatoExtrusionNom = c.ExtFormatoNom?.Trim(),
                material = EnteroONulo(c.ExtMaterial),
                materialNom = c.ExtMaterialNom?.Trim(),
                pigmento = EnteroONulo(c.ExtPigmento),
                pigmentoNom = c.ExtPigmentoNom?.Trim(),
                tratado = EnteroONulo(c.ExtTratado),
                tratadoNom = c.ExtTratadoNom?.Trim(),
                unidad = EnteroONulo(c.ExtUnidades),
                unidadNom = c.ExtUnidadesNom?.Trim(),
                ancho = c.PtAnchopt ?? 0,
                largo = c.PtLargopt ?? 0,
                fuelle = c.PtFuelle ?? 0,
                calibre = c.ExtCalibre ?? 0,
                anchoExtrusion1 = c.ExtAcho1,
                anchoExtrusion2 = c.ExtAcho2,
                anchoExtrusion3 = c.ExtAcho3,
                extrusion = NombreItemNormalizador.EsSi(c.Extrusion),
                impresion = NombreItemNormalizador.EsSi(c.Impresion),
                laminado = NombreItemNormalizador.EsSi(c.Laminado2),
                rotograbado = NombreItemNormalizador.EsSi(c.Lamiando),
                corte = NombreItemNormalizador.EsSi(c.Corte),
                sellado = NombreItemNormalizador.EsSi(c.Pterminado),
                cyrel = NombreItemNormalizador.EsSi(c.Cyrel),
                tipoImpresion = EnteroONulo(c.ImpFlexo),
                tipoImpresionNom = c.ImpFlexoNom?.Trim(),
                rodillo = c.ImpRodillo,
                pista = c.ImpPista,
                tintas,
                capasLaminado = capas,
                tipoSellado = EnteroONulo(c.PtSelladoPt),
                tipoSelladoNom = c.PtSelladoPtNom?.Trim(),
                cantidadPaquete = c.PtQtyPquete,
                cantidadBulto = c.PtQtyBulto,
                pesoMillar = c.PtPesoMillar,
                peso = c.PtPesopt,
                presentacion = c.PtPresentacionNom?.Trim(),
                precio = c.DatosvalorBolsa,
                vendedor = c.UsrModifica?.Trim(),
                observacion = c.Observacion?.Trim(),
            });
        }

        /// <summary>
        /// Busca un item duplicado: primero por nombre normalizado (en todos los items) y luego por ficha
        /// (mismo cliente, formato, material, ancho, largo, fuelle y calibre). Ambos bloquean la creación.
        /// </summary>
        /// <param name="dto">Ficha del item.</param>
        /// <returns>null si no hay duplicado; si lo hay, { tipo, codigo, nombre }.</returns>
        private async Task<object?> BuscarDuplicadoAsync(CrearItemDto dto)
        {
            string nombre = NombreItemNormalizador.Normalizar(dto.Nombre);

            // La normalización no se puede expresar en SQL: se traen los nombres distintos y se comparan en memoria.
            var nombres = await _context.ClientesOtItems.AsNoTracking()
                .Select(c => new { c.ClienteItems, c.ClienteItemsNom })
                .Distinct()
                .ToListAsync();
            var porNombre = nombres
                .Where(c => NombreItemNormalizador.Normalizar(c.ClienteItemsNom) == nombre)
                .OrderBy(c => c.ClienteItems)
                .FirstOrDefault();
            if (porNombre != null) return new { tipo = "nombre", codigo = porNombre.ClienteItems, nombre = porNombre.ClienteItemsNom?.Trim() };

            string formato = dto.Formato!.Value.ToString();
            string material = dto.Material!.Value.ToString();
            decimal ancho = Math.Round(dto.Ancho, 2), largo = Math.Round(dto.Largo, 2), fuelle = Math.Round(dto.Fuelle, 2), calibre = Math.Round(dto.Calibre, 2);
            var porFicha = await _context.ClientesOtItems.AsNoTracking()
                .Where(c => c.Cliente == dto.Cliente
                    && c.PtFormatopt!.Trim() == formato
                    && c.ExtMaterial!.Trim() == material
                    && (c.PtAnchopt ?? 0) == ancho
                    && (c.PtLargopt ?? 0) == largo
                    && (c.PtFuelle ?? 0) == fuelle
                    && (c.ExtCalibre ?? 0) == calibre)
                .OrderBy(c => c.ClienteItems)
                .Select(c => new { c.ClienteItems, c.ClienteItemsNom })
                .FirstOrDefaultAsync();
            return porFicha == null ? null : new { tipo = "ficha", codigo = porFicha.ClienteItems, nombre = porFicha.ClienteItemsNom.Trim() };
        }

        /// <summary>
        /// Arma la fila de ClientesOT_Item a partir de la ficha: resuelve los nombres de catálogo de BagPro y aplica
        /// los valores por defecto de SPCrearItem_DesdePlasticaribeBDD (Item = 0, UsrCrea = 'APP PLASTICARIBE',
        /// indicadores '1'/'0', Etiqueta '0'). ClienteItems se asigna después, dentro de la transacción.
        /// </summary>
        /// <param name="dto">Ficha del item.</param>
        /// <returns>La fila, o null si algún código de catálogo o el vendedor no existe (errores en ModelState).</returns>
        private async Task<ClientesOtItem?> ConstruirFilaAsync(CrearItemDto dto)
        {
            NombreItemNormalizador.TryPresentacion(dto.Presentacion, out string presentacion, out int codigoPresentacion);

            string? formatoNom = await _context.FormatoPts.AsNoTracking().Where(x => x.Id == dto.Formato).Select(x => x.NombreFormato).FirstOrDefaultAsync();
            if (formatoNom == null) ModelState.AddModelError(nameof(dto.Formato), "El formato no existe.");

            string? materialNom = await _context.Materials.AsNoTracking().Where(x => x.Id == dto.Material).Select(x => x.NombreMaterial ?? string.Empty).FirstOrDefaultAsync();
            if (materialNom == null) ModelState.AddModelError(nameof(dto.Material), "El material no existe.");

            string? unidadNom = await _context.Unidades.AsNoTracking().Where(x => x.Id == dto.Unidad).Select(x => x.NombreUnidades).FirstOrDefaultAsync();
            if (unidadNom == null) ModelState.AddModelError(nameof(dto.Unidad), "La unidad no existe.");

            string? formatoExtNom = null, pigmentoNom = null, tratadoNom = null, selladoNom = null, flexoNom = null;
            if (dto.FormatoExtrusion != null)
            {
                formatoExtNom = await _context.Formatos.AsNoTracking().Where(x => x.Id == dto.FormatoExtrusion).Select(x => x.NombreFormato).FirstOrDefaultAsync();
                if (formatoExtNom == null) ModelState.AddModelError(nameof(dto.FormatoExtrusion), "El formato de extrusión no existe.");
            }
            if (dto.Pigmento != null)
            {
                pigmentoNom = await _context.Pigmentos.AsNoTracking().Where(x => x.Id == dto.Pigmento).Select(x => x.NombreMaterial).FirstOrDefaultAsync();
                if (pigmentoNom == null) ModelState.AddModelError(nameof(dto.Pigmento), "El pigmento no existe.");
            }
            if (dto.Tratado != null)
            {
                tratadoNom = await _context.Tratados.AsNoTracking().Where(x => x.Id == dto.Tratado).Select(x => x.NombreTratado).FirstOrDefaultAsync();
                if (tratadoNom == null) ModelState.AddModelError(nameof(dto.Tratado), "El tratado no existe.");
            }
            if (dto.TipoSellado != null)
            {
                selladoNom = await _context.SelladoPts.AsNoTracking().Where(x => x.Id == dto.TipoSellado).Select(x => x.NombreFormato).FirstOrDefaultAsync();
                if (selladoNom == null) ModelState.AddModelError(nameof(dto.TipoSellado), "El tipo de sellado no existe.");
            }
            if (dto.Impresion && dto.TipoImpresion != null)
            {
                flexoNom = await _context.ImpresionFlexos.AsNoTracking().Where(x => x.Id == dto.TipoImpresion).Select(x => x.NombreMaterial).FirstOrDefaultAsync();
                if (flexoNom == null) ModelState.AddModelError(nameof(dto.TipoImpresion), "El tipo de impresión no existe.");
            }

            var tintas = dto.Impresion ? (dto.Tintas ?? new List<int>()) : new List<int>();
            var nombresTintas = tintas.Count == 0 ? new Dictionary<int, string>()
                : await _context.ImpresionTinta.AsNoTracking().Where(x => tintas.Contains(x.Id)).ToDictionaryAsync(x => x.Id, x => x.NombreMaterial);
            foreach (int t in tintas.Where(t => !nombresTintas.ContainsKey(t)).Distinct())
                ModelState.AddModelError(nameof(dto.Tintas), $"La tinta {t} no existe.");

            var capas = dto.Laminado ? (dto.CapasLaminado ?? new List<CapaLaminadoDto>()) : new List<CapaLaminadoDto>();
            var idsCapas = capas.Select(x => x.Capa).ToList();
            var nombresCapas = idsCapas.Count == 0 ? new Dictionary<int, string>()
                : await _context.LaminadoCapas.AsNoTracking().Where(x => idsCapas.Contains(x.Id)).ToDictionaryAsync(x => x.Id, x => x.NombreMaterial);
            foreach (int c in idsCapas.Where(c => !nombresCapas.ContainsKey(c)).Distinct())
                ModelState.AddModelError(nameof(dto.CapasLaminado), $"La capa de laminado {c} no existe.");

            string? vendedor = string.IsNullOrWhiteSpace(dto.Vendedor) ? null : dto.Vendedor.Trim();
            if (vendedor != null && !await _context.Vendedores.AsNoTracking().AnyAsync(v => v.Id == vendedor))
                ModelState.AddModelError(nameof(dto.Vendedor), "El vendedor no existe.");

            if (!ModelState.IsValid) return null;

            decimal precio = Math.Round(dto.Precio ?? 0, 2);
            string? Tinta(int i) => i < tintas.Count ? tintas[i].ToString() : null;
            string? TintaNom(int i) => i < tintas.Count ? Recortar(nombresTintas[tintas[i]], 30) : null;
            CapaLaminadoDto? Capa(int i) => i < capas.Count ? capas[i] : null;
            string? CapaNom(int i) => i < capas.Count ? Recortar(nombresCapas[capas[i].Capa], 30) : null;
            DateTime ahora = DateTime.Now;

            return new ClientesOtItem
            {
                Item = 0,
                Cliente = dto.Cliente,
                ClienteNom = dto.ClienteNom.Trim(),
                ClienteItemsNom = NombreItemNormalizador.LimpiarParaGuardar(dto.Nombre),
                Extrusion = Indicador(dto.Extrusion),
                ExtMaterial = dto.Material!.Value.ToString(),
                ExtMaterialNom = Recortar(materialNom, 30),
                ExtFormato = dto.FormatoExtrusion?.ToString(),
                ExtFormatoNom = Recortar(formatoExtNom, 30),
                ExtPigmento = dto.Pigmento?.ToString(),
                ExtPigmentoNom = Recortar(pigmentoNom, 30),
                ExtUnidades = dto.Unidad!.Value.ToString(),
                ExtUnidadesNom = Recortar(unidadNom, 30),
                ExtCalibre = Math.Round(dto.Calibre, 2),
                ExtAcho1 = Redondear(dto.AnchoExtrusion1),
                ExtAcho2 = Redondear(dto.AnchoExtrusion2),
                ExtAcho3 = Redondear(dto.AnchoExtrusion3),
                ExtTratado = dto.Tratado?.ToString(),
                ExtTratadoNom = Recortar(tratadoNom, 30),
                Impresion = Indicador(dto.Impresion),
                ImpFlexo = dto.Impresion ? dto.TipoImpresion?.ToString() : null,
                ImpFlexoNom = Recortar(flexoNom, 30),
                ImpRodillo = dto.Impresion ? dto.Rodillo : null,
                ImpPista = dto.Impresion ? dto.Pista : null,
                ImpTinta1 = Tinta(0), ImpTinta1Nom = TintaNom(0),
                ImpTinta2 = Tinta(1), ImpTinta2Nom = TintaNom(1),
                ImpTinta3 = Tinta(2), ImpTinta3Nom = TintaNom(2),
                ImpTinta4 = Tinta(3), ImpTinta4Nom = TintaNom(3),
                ImpTinta5 = Tinta(4), ImpTinta5Nom = TintaNom(4),
                ImpTinta6 = Tinta(5), ImpTinta6Nom = TintaNom(5),
                ImpTinta7 = Tinta(6), ImpTinta7Nom = TintaNom(6),
                ImpTinta8 = Tinta(7), ImpTinta8Nom = TintaNom(7),
                Laminado2 = Indicador(dto.Laminado),
                LamCapa1 = Capa(0)?.Capa.ToString(), LamCapa1Nom = CapaNom(0), LamCalibre1 = Redondear(Capa(0)?.Calibre), Cant1 = Capa(0)?.Cantidad,
                LamCapa2 = Capa(1)?.Capa.ToString(), LamCapa2Nom = CapaNom(1), LamCalibre2 = Redondear(Capa(1)?.Calibre), Cant2 = Capa(1)?.Cantidad,
                LamCapa3 = Capa(2)?.Capa.ToString(), LamCapa3Nom = CapaNom(2), LamCalibre3 = Redondear(Capa(2)?.Calibre), Cant3 = Capa(2)?.Cantidad,
                Lamiando = Indicador(dto.Rotograbado),
                Pterminado = Indicador(dto.Sellado),
                PtFormatopt = dto.Formato!.Value.ToString(),
                PtFormatoptNom = Recortar(formatoNom, 50),
                PtPesopt = Redondear(dto.Peso) ?? 0,
                PtMargen = 0,
                PtPesoMillar = Redondear(dto.PesoMillar) ?? 0,
                PtPesoRollo = 0,
                PtQtyBulto = dto.CantidadBulto,
                PtPresentacion = codigoPresentacion.ToString(),
                PtPresentacionNom = presentacion,
                PtQtyPquete = dto.CantidadPaquete,
                PtSelladoPt = dto.TipoSellado?.ToString(),
                PtSelladoPtNom = Recortar(selladoNom, 50),
                PtAnchopt = Math.Round(dto.Ancho, 2),
                PtLargopt = Math.Round(dto.Largo, 2),
                PtFuelle = Math.Round(dto.Fuelle, 2),
                DatosvalorBolsa = precio,
                DatosValorKg = presentacion == "Kilo" ? precio : null,
                Corte = Indicador(dto.Corte),
                UsrCrea = UsuarioCreacion,
                FechaCrea = ahora,
                UsrModifica = vendedor,
                FechaModifica = ahora,
                Pedido = string.Empty,
                Cyrel = Indicador(dto.Cyrel),
                Observacion = string.IsNullOrWhiteSpace(dto.Observacion) ? null : dto.Observacion.Trim(),
                Etiqueta = "0",
                EtiquetaLargo = "0",
                EtiquetaFuelle = "0",
                Dia = string.Empty,
                Noche = string.Empty,
            };
        }

        /// <summary>
        /// Toma el bloqueo exclusivo de aplicación 'CrearClienteItem' para la transacción actual (espera hasta 10 s).
        /// Serializa la asignación de MAX(ClienteItems) + 1 y el alta de presentaciones.
        /// </summary>
        /// <returns>true si se obtuvo el bloqueo.</returns>
        private async Task<bool> ObtenerBloqueoAsync()
        {
            var resultado = new SqlParameter("@resultado", SqlDbType.Int) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync(
                "EXEC @resultado = sp_getapplock @Resource = N'" + RecursoBloqueo + "', @LockMode = 'Exclusive', @LockOwner = 'Transaction', @LockTimeout = " + TiempoBloqueoMs + ";",
                resultado);
            return resultado.Value is int r && r >= 0;
        }

        /// <summary>
        /// Indica si el código ya existe en Productos de la base de Plasticaribe del mismo servidor.
        /// </summary>
        /// <param name="baseDatos">Nombre validado de la base de Plasticaribe.</param>
        /// <param name="codigo">Código candidato (Prod_Id).</param>
        /// <returns>true si el producto existe.</returns>
        private async Task<bool> ExisteProductoPlasticaribeAsync(string baseDatos, int codigo)
        {
            var existe = new SqlParameter("@existe", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync(
                $"SET @existe = CASE WHEN EXISTS (SELECT 1 FROM [{baseDatos}].dbo.Productos WHERE Prod_Id = @codigo) THEN 1 ELSE 0 END;",
                existe, new SqlParameter("@codigo", SqlDbType.Int) { Value = codigo });
            return existe.Value is bool b && b;
        }

        /// <summary>
        /// Mayor Prod_Id de Productos en la base de Plasticaribe del mismo servidor.
        /// </summary>
        /// <param name="baseDatos">Nombre validado de la base de Plasticaribe.</param>
        /// <returns>El mayor código, o 0 si no hay productos.</returns>
        private async Task<int> MaxProductoPlasticaribeAsync(string baseDatos)
        {
            var maximo = new SqlParameter("@maximo", SqlDbType.Int) { Direction = ParameterDirection.Output };
            await _context.Database.ExecuteSqlRawAsync(
                $"SELECT @maximo = ISNULL(MAX(Prod_Id), 0) FROM [{baseDatos}].dbo.Productos;", maximo);
            return maximo.Value is int m ? m : 0;
        }

        /// <summary>
        /// Mayor código numérico de 6 dígitos de los artículos de Zeus, leído con OPENQUERY por el servidor vinculado
        /// (la consulta se ejecuta en Zeus y solo regresa el máximo).
        /// </summary>
        /// <param name="servidor">Servidor vinculado validado (ej. 192.168.0.85).</param>
        /// <param name="baseZeus">Base validada de Zeus (Inventario_Prea21 en pruebas, Inventario en producción).</param>
        /// <returns>El mayor código, o 0 si no hay.</returns>
        private async Task<int> MaxArticuloZeusAsync(string servidor, string baseZeus)
        {
            var maximo = new SqlParameter("@maximo", SqlDbType.Int) { Direction = ParameterDirection.Output };
            string consultaRemota = $"SELECT MAX(CONVERT(int, RTRIM(Codigo))) AS Maximo FROM [{baseZeus}].dbo.Articulo WHERE LEN(RTRIM(Codigo)) = 6 AND RTRIM(Codigo) NOT LIKE ''%[^0-9]%''";
            await _context.Database.ExecuteSqlRawAsync(
                $"SELECT @maximo = ISNULL(Maximo, 0) FROM OPENQUERY([{servidor}], '{consultaRemota}');", maximo);
            return maximo.Value is int m ? m : 0;
        }

        /// <summary>
        /// Lee CreacionItems:ServidorZeus y CreacionItems:BaseZeus.
        /// </summary>
        /// <returns>Servidor y base si ambos son válidos; si no, null.</returns>
        private (string Servidor, string Base)? ObtenerZeus()
        {
            string? servidor = _configuration["CreacionItems:ServidorZeus"];
            string? baseZeus = _configuration["CreacionItems:BaseZeus"];
            bool valido = !string.IsNullOrWhiteSpace(servidor) && ServidorValido.IsMatch(servidor)
                && !string.IsNullOrWhiteSpace(baseZeus) && NombreBaseValido.IsMatch(baseZeus);
            return valido ? (servidor!, baseZeus!) : null;
        }

        /// <summary>
        /// Lee CreacionItems:BasePlasticaribe (PlasticaribeBDD en producción, PlasticaribeBDD_Test en pruebas).
        /// </summary>
        /// <returns>El nombre si solo tiene letras, números y guion bajo; si no, null.</returns>
        private string? ObtenerBasePlasticaribe()
        {
            string? baseDatos = _configuration["CreacionItems:BasePlasticaribe"];
            return !string.IsNullOrWhiteSpace(baseDatos) && NombreBaseValido.IsMatch(baseDatos) ? baseDatos : null;
        }

        /// <summary>Convierte un indicador al formato de BagPro.</summary>
        /// <param name="valor">Indicador.</param>
        /// <returns>'1' o '0'.</returns>
        private static string Indicador(bool valor) => valor ? "1" : "0";

        /// <summary>Recorta un texto al largo de la columna de destino.</summary>
        /// <param name="texto">Texto.</param>
        /// <param name="largo">Largo máximo.</param>
        /// <returns>Texto recortado y sin espacios en los extremos, o null.</returns>
        private static string? Recortar(string? texto, int largo)
        {
            if (texto == null) return null;
            texto = texto.Trim();
            return texto.Length > largo ? texto[..largo] : texto;
        }

        /// <summary>Redondea a 2 decimales (columnas numeric(8,2)).</summary>
        /// <param name="valor">Valor.</param>
        /// <returns>Valor redondeado o null.</returns>
        private static decimal? Redondear(decimal? valor) => valor == null ? null : Math.Round(valor.Value, 2);

        /// <summary>Convierte un código guardado como texto en entero.</summary>
        /// <param name="valor">Texto.</param>
        /// <returns>El entero o null si no es numérico.</returns>
        private static int? EnteroONulo(string? valor) => int.TryParse(valor?.Trim(), out int n) ? n : null;
    }
}
