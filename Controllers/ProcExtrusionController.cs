using BagproWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceReference1;
using System.ServiceModel;

namespace BagproWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ProcExtrusionController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public ProcExtrusionController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/ProcExtrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcExtrusion>>> GetProcExtrusions()
        {
            if (_context.ProcExtrusions == null)
            {
                return NotFound();
            }
            return await _context.ProcExtrusions.ToListAsync();
        }

        // GET: api/ProcExtrusion/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcExtrusion>> GetProcExtrusion(int id)
        {
            if (_context.ProcExtrusions == null)
            {
                return NotFound();
            }
            var procExtrusion = await _context.ProcExtrusions.FindAsync(id);

            if (procExtrusion == null)
            {
                return NotFound();
            }

            return procExtrusion;
        }

        // Consulta por OT agrupada por proceso
        [HttpGet("OT/{ot}")]
        public ActionResult<ProcExtrusion> GetOT(string ot)
        {
            var con = from pe in _context.Set<ProcExtrusion>()
                      where pe.Ot == ot
                      group pe by new
                      {
                          pe.NomStatus,
                          pe.Ot
                      } into pe
                      select new
                      {
                          Proceso = pe.Key.NomStatus,
                          Total = pe.Sum(x => x.Extnetokg),
                      };

            return Ok(con);
        }

        [HttpGet("FechaFinOT/{ot}")]
        public ActionResult<ProcExtrusion> GetFechaFinOT(string ot)
        {
            var procExtrusion = (from ProcExt in _context.Set<ProcExtrusion>()
                                 where ProcExt.Ot == ot
                                       && ProcExt.NomStatus == "EMPAQUE"
                                 orderby ProcExt.Item descending
                                 select new
                                 {
                                     ProcExt.Item,
                                     ProcExt.Ot,
                                     ProcExt.Fecha,
                                     ProcExt.NomStatus,
                                 }).FirstOrDefault();

            if (procExtrusion == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(procExtrusion);
            }
        }

        /** Obtener datos por procesos de la tabla procextrusion y procsellado */
        [HttpGet("getObtenerDatosxProcesos/{OT}/{status}")]
        public ActionResult<ProcExtrusion> GetObtenerDatosxProcesos(string OT, string status)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var query = _context.ProcExtrusions.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == status)
                                                 .OrderByDescending(proExt => proExt.Item)
                                                 .Select(ProExtru => new
                                                 {
                                                     Rollo = ProExtru.Item,
                                                     OT = ProExtru.Ot,
                                                     Id_Cliente = ProExtru.Cliente.Trim(),
                                                     Cliente = ProExtru.ClienteNombre,
                                                     Item = ProExtru.ClienteItem.Trim(),
                                                     Referencia = ProExtru.ClienteItemNombre,
                                                     Peso1 = ProExtru.Extnetokg,
                                                     Peso2 = ProExtru.Extnetokg,
                                                     Unidad = Convert.ToString("Kg"),
                                                     Operario = ProExtru.Operador,
                                                     Maquina = Convert.ToString(ProExtru.Maquina),
                                                     Fecha = Convert.ToString(ProExtru.Fecha),
                                                     Hora = ProExtru.Hora.Trim(),
                                                     Proceso = ProExtru.NomStatus,
                                                     Turno = ProExtru.Turno.Trim(),
                                                     EnviadoZeus = ProExtru.EnvioZeus.Trim()
                                                 }).ToList();

            var query2 = _context.ProcSellados.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == status)
                                                 .OrderByDescending(proExt => proExt.Item)
                                                 .Select(ProSella => new
                                                 {
                                                     Rollo = ProSella.Item,
                                                     OT = ProSella.Ot.Trim(),
                                                     Id_Cliente = ProSella.CodCliente.Trim(),
                                                     Cliente = ProSella.Cliente,
                                                     Item = ProSella.Referencia.Trim(),
                                                     Referencia = ProSella.NomReferencia,
                                                     Peso1 = ProSella.Peso,
                                                     Peso2 = ProSella.Qty,
                                                     Unidad = Convert.ToString(ProSella.Unidad),
                                                     Operario = ProSella.Operario,
                                                     Maquina = Convert.ToString(ProSella.Maquina).Trim(),
                                                     Fecha = Convert.ToString(ProSella.FechaEntrada),
                                                     Hora = ProSella.Hora,
                                                     Proceso = ProSella.NomStatus,
                                                     Turno = ProSella.Turnos.Trim(),
                                                     EnviadoZeus = ProSella.EnvioZeus.Trim(),
                                                 }).ToList();


            if (query == null && query2 == null) return BadRequest("No se encontraron registros en este proceso de la OT seleccionada");
            else return Ok(query.Concat(query2));
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        /** Mostrar Datos Consolidados de ProcExtrusión Para modal en Vista Estados Procesos OT*/
        [HttpGet("getDatosConsolidados/{OT}/{Proceso}")]
        public ActionResult<ProcExtrusion> GetDatosConsolidados(string OT, string Proceso)
        {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            var query1 = _context.ProcExtrusions.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Proceso)
                                                 .GroupBy(agr => new { agr.Fecha, agr.Operador, agr.ClienteItemNombre, agr.Ot, agr.NomStatus })
                                                 .Select(ProEx => new
                                                 {
                                                     SumaCantidad1 = ProEx.Sum(ProSel => ProSel.Extnetokg),
                                                     SumaCantidad2 = ProEx.Sum(ProSel => ProSel.Extnetokg),
                                                     Ot = ProEx.Key.Ot,
                                                     Referencia = ProEx.Key.ClienteItemNombre,
                                                     Operario = ProEx.Key.Operador,
                                                     Fecha = Convert.ToString(ProEx.Key.Fecha),
                                                     Proceso = ProEx.Key.NomStatus,
                                                     Registros = ProEx.Count(),
                                                 }).ToList();

            var query2 = _context.ProcSellados.Where(prExt => prExt.Ot == OT
                                                      && prExt.NomStatus == Proceso)
                                                .GroupBy(agr => new { agr.FechaEntrada, agr.Operario, agr.NomReferencia, agr.Ot, agr.NomStatus })
                                                .Select(ProSella => new
                                                {
                                                    SumaCantidad1 = ProSella.Sum(ProSel => ProSel.Peso),
                                                    SumaCantidad2 = ProSella.Sum(ProSel => ProSel.Qty),
                                                    Ot = ProSella.Key.Ot,
                                                    Referencia = ProSella.Key.NomReferencia,
                                                    Operario = ProSella.Key.Operario,
                                                    Fecha = Convert.ToString(ProSella.Key.FechaEntrada),
                                                    Proceso = ProSella.Key.NomStatus,
                                                    Registros = ProSella.Count(),
                                                }).ToList();


            if (query1 == null && query2 == null) return BadRequest("No se encontraron rollos consolidados para mostrar");
            else return Ok(query1.Concat(query2));
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
#pragma warning restore CS8604 // Possible null reference argument.
        }

        //Consultar toda la informacion de un rollo
        [HttpGet("consultarRollo/{rollo}")]
        public ActionResult consultarRollo(int rollo)
        {
            var con = from x in _context.Set<ProcExtrusion>()
                      where x.Item == rollo && x.NomStatus == "EXTRUSION"
                      select x;
            return Ok(con);
        }

        // Consulta los rollos pesados en bagpro de los procesos de Extrusion, Empaque y Sellado
        [HttpGet("getRollosExtrusion_Empaque_Sellado/{fechaInicial}/{fechaFinal}/{proceso}")]
        public ActionResult GetRollosExtrusion_Empaque_Sellado(DateTime fechaInicial, DateTime fechaFinal, string proceso, string? ot = "", string? rollo = "")
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var extrusion = from pe in _context.Set<ProcExtrusion>()
                            where pe.NomStatus == proceso
                                  && pe.Fecha >= fechaInicial
                                  && pe.Fecha <= fechaFinal
                                  && pe.Ot.Contains(ot)
                                  && Convert.ToString(pe.Item).Contains(rollo)
                            select new
                            {
                                Orden = Convert.ToString(pe.Ot),
                                Id_Producto = Convert.ToString(pe.ClienteItem),
                                Producto = Convert.ToString(pe.ClienteItemNombre),
                                Rollo = Convert.ToString(pe.Item),
                                Cantidad = Convert.ToString(pe.Extnetokg),
                                Presentacion = Convert.ToString("Kg"),
                                Proceso = Convert.ToString(pe.NomStatus),
                            };

            var sellado = from ps in _context.Set<ProcSellado>()
                          where ps.NomStatus == proceso
                                && ps.FechaEntrada >= fechaInicial
                                && ps.FechaEntrada <= fechaFinal
                                && ps.Ot.Contains(ot)
                                && Convert.ToString(ps.Item).Contains(rollo)
                          select new
                          {
                              Orden = Convert.ToString(ps.Ot),
                              Id_Producto = Convert.ToString(ps.Referencia),
                              Producto = Convert.ToString(ps.NomReferencia),
                              Rollo = Convert.ToString(ps.Item),
                              Cantidad = Convert.ToString(ps.Qty),
                              Presentacion = Convert.ToString(ps.Unidad),
                              Proceso = Convert.ToString(ps.NomStatus),
                          };

            return Ok(extrusion.Concat(sellado));
#pragma warning restore CS8604 // Posible argumento de referencia nulo
        }

        // Consulta los rollos pesados en bagpro de los procesos de Extrusion, Empaque y Sellado
        [HttpGet("getProcExtrusion_ProcSellado/{fechaInicial}/{fechaFinal}")]
        public ActionResult GetProcExtrusion_ProcSellado(DateTime fechaInicial, DateTime fechaFinal, string? ot = "", string? rollo = "")
        {
#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var extrusion = from pe in _context.Set<ProcExtrusion>()
                            where pe.Fecha >= fechaInicial
                                  && pe.Fecha <= fechaFinal
                                  && pe.Ot.Contains(ot)
                                  && Convert.ToString(pe.Item).Contains(rollo)
                            select new
                            {
                                Orden = Convert.ToString(pe.Ot),
                                Id_Producto = Convert.ToString(pe.ClienteItem),
                                Producto = Convert.ToString(pe.ClienteItemNombre),
                                Rollo = Convert.ToString(pe.Item),
                                Cantidad = Convert.ToString(pe.Extnetokg),
                                Presentacion = Convert.ToString("Kg"),
                                Proceso = Convert.ToString(pe.NomStatus),
                            };

            var sellado = from ps in _context.Set<ProcSellado>()
                          where ps.FechaEntrada >= fechaInicial
                                && ps.FechaEntrada <= fechaFinal
                                && ps.Ot.Contains(ot)
                                && Convert.ToString(ps.Item).Contains(rollo)
                          select new
                          {
                              Orden = Convert.ToString(ps.Ot),
                              Id_Producto = Convert.ToString(ps.Referencia),
                              Producto = Convert.ToString(ps.NomReferencia),
                              Rollo = Convert.ToString(ps.Item),
                              Cantidad = Convert.ToString(ps.Qty),
                              Presentacion = Convert.ToString(ps.Unidad),
                              Proceso = Convert.ToString(ps.NomStatus),
                          };

            if (extrusion.Concat(sellado) != null) return Ok(extrusion.Concat(sellado));
            else return BadRequest("No se han encontrado la información solicitada");
#pragma warning restore CS8604 // Posible argumento de referencia nulo
        }

        // Consulta que devolverá la información de una orden de trabajo en un proceso en especifico
        [HttpGet("getInformacionOrden_Proceso/{orden}/{proceso}")]
        public ActionResult GetInformacionOrden_Proceso(string orden, string proceso)
        {
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            List<string> procesos = new List<string>();
            foreach (var item in proceso.Split("|"))
            {
                procesos.Add(item);
            }

            var extrusion = from ext in _context.Set<ProcExtrusion>()
                            where ext.Ot == orden &&
                                  procesos.Contains(ext.NomStatus.Trim())
                            select ext;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
#pragma warning restore CS8604 // Possible null reference argument.
            return extrusion.Any() ? Ok(extrusion) : BadRequest();
        }

        [HttpGet("getOtControlCalidadExtrusion/{OT}/{status}")]
        public ActionResult<ProcExtrusion> GetOtControlCalidadExtrusion(string OT, string status)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            var query = from cl in _context.Set<ClientesOt>()
                        from pe in _context.Set<ProcExtrusion>()
                        where Convert.ToString(cl.Item).Trim() == pe.Ot.Trim() &&
                        pe.Ot == OT &&
                        Convert.ToString(cl.Item) == OT &&
                        pe.NomStatus == status
                        select new
                        {
                            Rollo = pe.Item,
                            OT = cl.Item,
                            Id_Cliente = cl.Cliente,
                            Cliente = cl.ClienteNom,
                            Item = cl.ClienteItems,
                            Referencia = cl.ClienteItemsNom,
                            Maquina = Convert.ToString(pe.Maquina),
                            Proceso = Convert.ToString(pe.NomStatus),
                            Turno = Convert.ToString(pe.Turno).Trim(),
                            PigmentoId = cl.ExtPigmento.Trim(),
                            Pigmento = cl.ExtPigmentoNom.Trim(),
                            Calibre = cl.ExtCalibre,
                            Ancho = cl.PtAnchopt,
                            Largo = cl.PtLargopt,
                            CantBolsasxPaq = cl.PtQtyPquete,
                            AnchoFuelle_Derecha = cl.ExtAcho1,
                            AnchoFuelle_Izquierda = cl.ExtAcho2,
                            AnchoFuelle_Abajo = cl.ExtAcho3,
                            TratadoId = Convert.ToString(cl.ExtTratado).Trim(),
                            Tratado = Convert.ToString(cl.ExtTratadoNom).Trim(),
                            Impresion = Convert.ToString(cl.Impresion).Trim(),
                        };

            var query2 = from cl in _context.Set<ClientesOt>()
                         from ps in _context.Set<ProcSellado>()
                         where Convert.ToString(cl.Item).Trim() == ps.Ot.Trim() &&
                         ps.Ot == OT &&
                         Convert.ToString(cl.Item) == OT &&
                         ps.NomStatus == status
                         select new
                         {
                             Rollo = ps.Item,
                             OT = cl.Item,
                             Id_Cliente = cl.Cliente,
                             Cliente = cl.ClienteNom,
                             Item = cl.ClienteItems,
                             Referencia = cl.ClienteItemsNom,
                             Maquina = Convert.ToString(ps.Maquina),
                             Proceso = Convert.ToString(ps.NomStatus),
                             Turno = Convert.ToString(ps.Turnos).Trim(),
                             PigmentoId = cl.ExtPigmento.Trim(),
                             Pigmento = cl.ExtPigmentoNom.Trim(),
                             Calibre = cl.ExtCalibre,
                             Ancho = cl.PtAnchopt,
                             Largo = cl.PtLargopt,
                             CantBolsasxPaq = cl.PtQtyPquete,
                             AnchoFuelle_Derecha = cl.ExtAcho1,
                             AnchoFuelle_Izquierda = cl.ExtAcho2,
                             AnchoFuelle_Abajo = cl.ExtAcho3,
                             TratadoId = Convert.ToString("No aplica"),
                             Tratado = Convert.ToString("No aplica"),
                             Impresion = Convert.ToString(cl.Impresion).Trim(),
                         };

            if (query == null && query2 == null) return BadRequest("La OT consultada no se encuentra en el proceso seleccionado!");
            return Ok(query.Concat(query2));
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // Consulta que devolverá la información de las cantidades producidas por cada ara en cada uno de los meses del año que le sea pasado
        [HttpGet("getProduccionAreas/{anio}")]
        public ActionResult GetProduccionAreas(int anio)
        {
#pragma warning disable CS8629 // Nullable value type may be null.
            var producidoExt = from pe in _context.Set<ProcExtrusion>()
                               where pe.Fecha.Value.Year == anio
                               orderby pe.Fecha.Value.Year descending, pe.Fecha.Value.Month descending
                               group pe by new
                               {
                                   Area = Convert.ToString(pe.NomStatus),
                                   Mes = Convert.ToInt32(pe.Fecha.Value.Month),
                                   Anio = Convert.ToInt32(pe.Fecha.Value.Year),
                               } into pe
                               select new
                               {
                                   pe.Key.Area,
                                   pe.Key.Mes,
                                   pe.Key.Anio,
                                   Producido = pe.Sum(x => x.Extnetokg),
                               };

            var producidoSell = from ps in _context.Set<ProcSellado>()
                                where ps.FechaEntrada.Year == anio
                                orderby ps.FechaEntrada.Year descending, ps.FechaEntrada.Month descending
                                group ps by new
                                {
                                    Area = Convert.ToString(ps.NomStatus),
                                    Mes = Convert.ToInt32(ps.FechaEntrada.Month),
                                    Anio = Convert.ToInt32(ps.FechaEntrada.Year),
                                } into ps
                                select new
                                {
                                    ps.Key.Area,
                                    ps.Key.Mes,
                                    ps.Key.Anio,
                                    Producido = ps.Sum(x => x.Peso),
                                };

            var desperdicios = from desp in _context.Set<Procdesperdicio>()
                               where desp.Fecha.Value.Year == anio
                               orderby desp.Fecha.Value.Year descending, desp.Fecha.Value.Month descending
                               group desp by new
                               {
                                   Area = Convert.ToString(desp.NomStatus),
                                   Mes = Convert.ToInt32(desp.Fecha.Value.Month),
                                   Anio = Convert.ToInt32(desp.Fecha.Value.Year),
                               } into desp
                               select new
                               {
                                   desp.Key.Area,
                                   desp.Key.Mes,
                                   desp.Key.Anio,
                                   Producido = desp.Sum(x => x.Extnetokg),
                               };

            return Ok(producidoExt.Concat(producidoSell).Concat(desperdicios));
#pragma warning restore CS8629 // Nullable value type may be null.
        }

        // Consulta que devolverá la producción de las áreas en un rango de fechas
        [HttpGet("getProduccionDetalladaAreas/{fechaInicio}/{fechaFin}")]
        public ActionResult GetProduccionDetalladaAreas(DateTime fechaInicio, DateTime fechaFin, string? orden = "", string? proceso = "", string? cliente = "", string? producto = "", string? turno = "", string? envioZeus = "")
        {
#pragma warning disable IDE0075 // Simplify conditional expression
#pragma warning disable CS8629 // Nullable value type may be null.
#pragma warning disable CS8604 // Possible null reference argument.

            List<string> turnosDia = new List<string>();
            turnosDia.Add("DIA");
            turnosDia.Add("RD"); 

            List<string> turnosNoche = new List<string>();
            turnosNoche.Add("NOCHE");
            turnosNoche.Add("RN");

            var ProcExt = from ext in _context.Set<ProcExtrusion>()
                          where ext.Fecha >= fechaInicio &&
                                ext.Fecha <= fechaFin &&
                                (orden != "" ? ext.Ot.Trim() == orden : true) &&
                                (proceso != "" ? ext.NomStatus == proceso : true) &&
                                (cliente != "" ? ext.Cliente == cliente : true) &&
                                (producto != "" ? ext.ClienteItem == producto : true) &&
                                (turno != "" ? turno == "DIA" ? turnosDia.Contains(ext.Turno) : turno == "NOCHE" ? turnosNoche.Contains(ext.Turno) : true : true) &&
                                (envioZeus != "" ? envioZeus == ext.EnvioZeus : true)
                          select new
                          {
                              Rollo = Convert.ToInt32(ext.Item),
                              Orden = Convert.ToInt32(ext.Ot),
                              IdCliente = Convert.ToString(ext.Cliente),
                              Cliente = Convert.ToString(ext.ClienteNombre),
                              Item = Convert.ToString(ext.ClienteItem),
                              Referencia = Convert.ToString(ext.ClienteItemNombre),
                              Cantidad = Convert.ToDecimal(ext.ExtBruto),
                              Peso = Convert.ToDecimal(ext.Extnetokg),
                              Presentacion = Convert.ToString("KLS"),
                              Turno = Convert.ToString(ext.Turno),
                              Fecha = ext.Fecha.Value,
                              Hora = Convert.ToString(ext.Hora),
                              Maquina = Convert.ToInt16(ext.Maquina),
                              EnvioZeus = Convert.ToString(ext.EnvioZeus),
                              Proceso = ext.NomStatus,
                          };

            var ProcSel = from sel in _context.Set<ProcSellado>()
                          where sel.FechaEntrada >= fechaInicio &&
                                sel.FechaEntrada <= fechaFin &&
                                (orden != "" ? sel.Ot.Trim() == orden : true) &&
                                (proceso != "" ? sel.NomStatus == proceso : true) &&
                                (cliente != "" ? sel.Cliente == cliente : true) &&
                                (producto != "" ? sel.Referencia == producto : true) &&
                                (turno != "" ? turno == "DIA" ? turnosDia.Contains(sel.Turnos) : turno == "NOCHE" ? turnosNoche.Contains(sel.Turnos) : true : true) &&
                                (envioZeus != "" ? envioZeus == sel.EnvioZeus : true)
                          select new
                          {
                              Rollo = Convert.ToInt32(sel.Item),
                              Orden = Convert.ToInt32(sel.Ot),
                              IdCliente = Convert.ToString(sel.CodCliente),
                              Cliente = Convert.ToString(sel.Cliente),
                              Item = Convert.ToString(sel.Referencia),
                              Referencia = Convert.ToString(sel.NomReferencia),
                              Cantidad = Convert.ToDecimal(sel.Qty),
                              Peso = Convert.ToDecimal(sel.Peso),
                              Presentacion = Convert.ToString(sel.Unidad),
                              Turno = Convert.ToString(sel.Turnos),
                              Fecha =sel.FechaEntrada,
                              Hora = Convert.ToString(sel.Hora),
                              Maquina = Convert.ToInt16(sel.Maquina),
                              EnvioZeus = Convert.ToString(sel.EnvioZeus),
                              Proceso = sel.NomStatus,
                          };

            var procesos = ProcExt.Concat(ProcSel);
            return procesos.Any() ? Ok(procesos) : BadRequest("¡No se encontró información!");

#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8629 // Nullable value type may be null.
#pragma warning restore IDE0075 // Simplify conditional expression
        }

        [HttpPost("ajusteExistencia")]
        public async Task<ActionResult> AjusteExistencia([FromBody] List<int> rollos)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            int count = 0;
            foreach(var rollo in rollos)
            {
                var datos = (from pro in _context.Set<ProcExtrusion>()
                             join ot in _context.Set<ClientesOt>() on Convert.ToString(pro.Ot) equals Convert.ToString(ot.Item)
                             where pro.Item == rollo &&
                                   pro.EnvioZeus.Trim() == "0"
                             select new
                             {
                                 Orden = pro.Ot,
                                 Item = pro.ClienteItem,
                                 Presentacion = ot.PtPresentacionNom,
                                 Rollo = pro.Item,
                                 Cantidad = pro.Extnetokg,
                                 Costo = ot.DatoscantKg
                             }).FirstOrDefault();

                await EnviarAjuste(datos.Orden, datos.Item, datos.Presentacion, datos.Rollo, datos.Cantidad, Convert.ToDecimal(datos.Costo));
                //PutEnvioZeus(datos.Rollo);
                CreatedAtAction("PutEnvioZeus", new { rollo = datos.Rollo }, datos.Rollo);
                count++;
                if (count == rollos.Count) return Ok();
            }

            return Ok();
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpGet("EnviarAjuste")]
        public async Task<ActionResult> EnviarAjuste(string ordenTrabajo, string articulo, string presentacion, int rollo, decimal cantidad, decimal costo)
        {
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            SoapRequestAction request = new SoapRequestAction();
            request.User = "wsZeusInvProd";
            request.Password = "wsZeusInvProd";
            request.Body = $"<Ajuste>" +
                                $"<Op>I</Op>" +
                                $"<Cabecera>" +
                                    $"<Detalle>{ordenTrabajo}</Detalle>" +
                                    "<Concepto>001</Concepto>" +
                                    "<Consecutivo>0</Consecutivo>" +
                                    $"<Fecha>{today}</Fecha>" +
                                    "<Estado></Estado>" +
                                    "<Solicitante>7200000</Solicitante>" +
                                    "<Aprueba></Aprueba>" +
                                    "<Fuente>MA</Fuente>" +
                                    "<Serie>00</Serie>" +
                                    "<Usuario>zeussystem</Usuario>" +
                                    "<Documento></Documento>" +
                                    "<Documentorevertido></Documentorevertido>" +
                                    "<Bodega>003</Bodega>" +
                                    "<Grupo></Grupo>" +
                                    "<Origen>I</Origen>" +
                                    "<ConsecutivoRecosteo>0</ConsecutivoRecosteo>" +
                                    "<TipoDocumentoExterno></TipoDocumentoExterno>" +
                                    "<ConsecutivoExterno></ConsecutivoExterno>" +
                                    "<EsAjustePorDistribucion></EsAjustePorDistribucion>" +
                                    "<ItemsBodegaVirtual></ItemsBodegaVirtual>" +
                                    "<Clasificaciones></Clasificaciones>" +
                                    "<Propiedad1></Propiedad1>" +
                                    "<Propiedad2></Propiedad2>" +
                                    "<Propiedad3></Propiedad3>" +
                                    "<Propiedad4></Propiedad4>" +
                                    "<Propiedad5></Propiedad5>" +
                                    "<EsInicioNIIF></EsInicioNIIF>" +
                                    "<UtilizarZmlSpId></UtilizarZmlSpId>" +
                                    "<DatoExterno1></DatoExterno1>" +
                                    "<DatoExterno2></DatoExterno2>" +
                                    "<DatoExterno3></DatoExterno3>" +
                                    "<Moneda></Moneda>" +
                                    "<TasaCambio>1</TasaCambio>" +
                                    "<BU>Local</BU>" +
                                "</Cabecera>" +
                                "<Productos>" +
                                    $"<CodigoArticulo>{articulo}</CodigoArticulo>" +
                                    $"<Presentacion>{presentacion}</Presentacion>" +
                                    "<CodigoLote>0</CodigoLote>" +
                                    "<CodigoBodega>003</CodigoBodega>" +
                                    "<CodigoUbicacion></CodigoUbicacion>" +
                                    "<CodigoClasificacion>0</CodigoClasificacion>" +
                                    "<CodigoReferencia></CodigoReferencia>" +
            "<Serial>0</Serial>" +
                                    $"<Detalle>{rollo}</Detalle>" +
            $"<Cantidad>{cantidad}</Cantidad>" +
                                    $"<PrecioUnidad>{costo}</PrecioUnidad>" +
                                    $"<PrecioUnidad2>{costo}</PrecioUnidad2>" +
                                    "<Concepto_Codigo></Concepto_Codigo>" +
                                    "<TemporalItems_ValorAjuste></TemporalItems_ValorAjuste>" +
                                    "<Servicios>" +
                                    "<CodigoServicios>001</CodigoServicios>" +
                                    "<Referencia></Referencia>" +
                                    "<Detalle></Detalle>" +
                                    "<AuxiliarAbierto></AuxiliarAbierto>" +
                                    "<CentroCosto>0202</CentroCosto>" +
                                    "<Tercero>800188732</Tercero>" +
                                    "<Proveedor></Proveedor>" +
                                    "<TipoDocumentoCartera></TipoDocumentoCartera>" +
                                    "<DocumentoCartera></DocumentoCartera>" +
                                    "<Vencimiento></Vencimiento>" +
                                    "<Cliente></Cliente>" +
                                    "<Vendedor></Vendedor>" +
                                    "<ItemsContable></ItemsContable>" +
                                    "<Propiedad1></Propiedad1>" +
                                    "<Propiedad2></Propiedad2>" +
                                    "<Propiedad3></Propiedad3>" +
                                    "<Propiedad4></Propiedad4>" +
                                    "<Propiedad5></Propiedad5>" +
                                    "<CuentaMovimiento></CuentaMovimiento>" +
                                    "<Moneda></Moneda>" +
                                    "<Moneda></Moneda>" +
                                    "</Servicios>" +
                                "</Productos>" +
                            "</Ajuste>";
            request.DynamicProperty = "4";
            request.Action = "Inventario";
            request.TypeSQL = "true";

            var binding = new BasicHttpBinding()
            {
                Name = "BasicHttpBinding_IFakeService",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };

            var endpoint = new EndpointAddress("http://192.168.0.85/wsGenericoZeus/ServiceWS.asmx");
            WebservicesGenericoZeusSoapClient client = new WebservicesGenericoZeusSoapClient(binding, endpoint);
            SoapResponse response = await client.ExecuteActionSOAPAsync(request);
            return Convert.ToString(response.Status) == "SUCCESS" ? Ok(response) : BadRequest(response);
        }

        [HttpGet("getDatosRollosPesados/{orden}/{proceso}")]
        public ActionResult GetDatosRollosPesados(string orden, string proceso)
        {
            var rollosPesados = from pe in _context.Set<ProcExtrusion>()
                                where pe.Ot == orden &&
                                      pe.NomStatus == proceso
                                select pe;
            return rollosPesados.Any() ? Ok(rollosPesados) : BadRequest();
        }

        // PUT: api/ProcExtrusion/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcExtrusion(int id, ProcExtrusion procExtrusion)
        {
            if (id != procExtrusion.Item)
            {
                return BadRequest();
            }

            _context.Entry(procExtrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcExtrusionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("putEnvioZeus{rollo}")]
        public async Task<IActionResult> PutEnvioZeus(int rollo)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var procExtrusion = (from pro in _context.Set<ProcExtrusion>() where pro.Item == rollo select pro).FirstOrDefault();
            procExtrusion.EnvioZeus = "1";
            _context.Entry(procExtrusion).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcExtrusionExists(rollo)) return NotFound();
                else throw;
            }

            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // POST: api/ProcExtrusion
        [HttpPost]
        public async Task<ActionResult<ProcExtrusion>> PostProcExtrusion(ProcExtrusion procExtrusion)
        {
            if (_context.ProcExtrusions == null)
            {
                return Problem("Entity set 'plasticaribeContext.ProcExtrusions'  is null.");
            }
            _context.ProcExtrusions.Add(procExtrusion);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcExtrusion", new { id = procExtrusion.Item }, procExtrusion);
        }

        // DELETE: api/ProcExtrusion/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcExtrusion(int id)
        {
            if (_context.ProcExtrusions == null)
            {
                return NotFound();
            }
            var procExtrusion = await _context.ProcExtrusions.FindAsync(id);
            if (procExtrusion == null)
            {
                return NotFound();
            }

            _context.ProcExtrusions.Remove(procExtrusion);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //Eliminar Rollo del ingreso
        [HttpDelete("EliminarRolloProcExtrusion/{rollo}")]
        public ActionResult EliminarRolloIngresados(int rollo)
        {
            var x = (from y in _context.Set<ProcExtrusion>()
                     where y.Item == rollo
                     orderby y.Item descending
                     select y).FirstOrDefault();

            if (x == null)
            {
                return NotFound();
            }

            _context.ProcExtrusions.Remove(x);
            _context.SaveChanges();

            return NoContent();
        }

        private bool ProcExtrusionExists(int id)
        {
            return (_context.ProcExtrusions?.Any(e => e.Item == id)).GetValueOrDefault();
        }
    }
}
