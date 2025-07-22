using BagproWebAPI.Models;
using iText.Forms.Form.Element;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ServiceReference1;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
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
                      && pe.Observacion != "Etiqueta eliminada desde App Plasticaribe"
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
                                       && ProcExt.Observacion != "Etiqueta eliminada desde App Plasticaribe"
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
                                                       && prExt.NomStatus == status
                                                       && prExt.Observacion != "Etiqueta eliminada desde App Plasticaribe")
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
                                                       && prExt.NomStatus == status
                                                       && prExt.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe")
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
                                                       && prExt.NomStatus == Proceso
                                                       && prExt.Observacion != "Etiqueta eliminada desde App Plasticaribe")
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
                                                      && prExt.NomStatus == Proceso
                                                      && prExt.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe")
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
                      where x.Item == rollo && x.NomStatus == "EXTRUSION" && x.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                      select x;
            return Ok(con);
        }

        //Consultar toda la informacion de un item por ot y por rollo 
        [HttpGet("getInformationRoll/{rollo}/{ot}")]
        public ActionResult getInformationRoll(int rollo, string ot)
        {
            var con = from x in _context.Set<ProcExtrusion>()
                      where x.Item == rollo && x.Ot == ot && x.NomStatus == "EXTRUSION" && x.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                      select new
                      {
                          Production = x,
                          NitClient = (from cl in _context.Clientes where cl.CodBagpro == Convert.ToString(x.Cliente) select cl.IdentNro).FirstOrDefault(),
                          Price = (from c in _context.ClientesOts where Convert.ToString(c.Item) == Convert.ToString(x.Ot) select c.ExtUnidadesNom == "Kilo" ? c.DatosValorKg : c.DatosvalorBolsa).FirstOrDefault(),
                      }; 
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
                                  && pe.Observacion != "Etiqueta eliminada desde App Plasticaribe"
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
                                && ps.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe"
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
                                  && pe.Observacion != "Etiqueta eliminada desde App Plasticaribe"
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
                                && ps.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe"
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
                                  procesos.Contains(ext.NomStatus.Trim()) &&
                                  ext.Observacion != "Etiqueta eliminada desde App Plasticaribe"
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
                        pe.NomStatus == status &&
                        pe.Observacion != "Etiqueta eliminada desde App Plasticaribe"
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
                         ps.NomStatus == status &&
                         ps.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe"
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
            string[] maquinas = { "35", "37", "38", "39" };
#pragma warning disable CS8629 // Nullable value type may be null.
            var producidoExt = from pe in _context.Set<ProcExtrusion>()
                               where pe.Fecha.Value.Year == anio &&
                               pe.Observacion != "Etiqueta eliminada desde App Plasticaribe"
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
                                where ps.FechaEntrada.Year == anio &&
                                ps.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe" && 
                                !maquinas.Contains(ps.Maquina) && 
                                ps.Cedula != "0"
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

            var producidoCamisillas = from ps in _context.Set<ProcSellado>()
                                where ps.FechaEntrada.Year == anio &&
                                ps.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe" &&
                                maquinas.Contains(ps.Maquina) &&
                                ps.Cedula != "0"
                                      orderby ps.FechaEntrada.Year descending, ps.FechaEntrada.Month descending
                                group ps by new
                                {
                                    Area = Convert.ToString(ps.NomStatus),
                                    Mes = Convert.ToInt32(ps.FechaEntrada.Month),
                                    Anio = Convert.ToInt32(ps.FechaEntrada.Year),
                                } into ps
                                select new
                                {
                                    Area = ps.Key.Area.Replace("SELLADO", "CAMISILLA"),
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

            return Ok(producidoExt.Concat(producidoSell).Concat(producidoCamisillas).Concat(desperdicios));
#pragma warning restore CS8629 // Nullable value type may be null.
        }

        // Consulta que devolverá la producción de las áreas en un rango de fechas
        [HttpGet("getProduccionDetalladaAreas/{fechaInicio}/{fechaFin}")]
        public ActionResult GetProduccionDetalladaAreas(DateTime fechaInicio, DateTime fechaFin, string? orden = "", string? proceso = "", string? cliente = "", string? producto = "", string? turno = "", string? envioZeus = "", string? maquina = "", string? operario = "")
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

            string[] machines = { "35", "37", "38", "39" };

            var ProcExt = (from ext in _context.Set<ProcExtrusion>()
                           from cl in _context.Set<ClientesOt>()
                           where ext.Fecha >= fechaInicio &&
                                 ext.Fecha <= fechaFin.AddDays(1) &&
                                 (orden != "" ? ext.Ot.Trim() == orden : true) &&
                                 (proceso != "" ? ext.NomStatus == proceso : true) &&
                                 (cliente != "" ? ext.Cliente == cliente : true) &&
                                 (producto != "" ? ext.ClienteItem == producto : true) &&
                                 (turno != "" ? turno == "DIA" ? turnosDia.Contains(ext.Turno) : turno == "NOCHE" ? turnosNoche.Contains(ext.Turno) : true : true) &&
                                 (envioZeus != "" ? envioZeus != "Todo" ? envioZeus == ext.EnvioZeus : true : true) &&
                                 (Convert.ToInt32(ext.Ot.Trim()) == cl.Item) &&
                                 ext.Item >= FirstRollDayExtrusion(fechaInicio).FirstOrDefault() &&
                                 (RollsNightExtrusion(fechaFin.AddDays(1)).Any() ? ext.Item <= (RollsNightExtrusion(fechaFin.AddDays(1)).FirstOrDefault()) : ext.Fecha <= fechaFin) &&
                                 ext.Observacion != "Etiqueta eliminada desde App Plasticaribe" &&
                                 (maquina != "" ? Convert.ToString(ext.Maquina) == maquina : true) &&
                                 (operario != "" ? Convert.ToString(ext.Operador) == operario : true)
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
                              Presentacion = Convert.ToString(cl.PtPresentacionNom) == "Kilo" ? "KLS" : Convert.ToString(cl.PtPresentacionNom) == "Unidad" ? "UND" : "PAQ", 
                              PesoTeorico = Convert.ToDecimal(0),
                              Desviacion = Convert.ToDecimal(0),
                              Turno = Convert.ToString(ext.Turno),
                              Fecha = ext.Fecha.Value,
                              Hora = Convert.ToString(ext.Hora),
                              Maquina = Convert.ToInt32(ext.Maquina),
                              EnvioZeus = Convert.ToString(ext.EnvioZeus),
                              Proceso = ext.NomStatus,
                              CantPedida = cl.DatosotKg,
                              Observacion = ext.Observaciones,
                              Operario = ext.Operador,
                          }).ToList();

            var ProcSel = (from sel in _context.Set<ProcSellado>()
                          from cl in _context.Set<ClientesOt>()
                          where sel.FechaEntrada >= fechaInicio &&
                                sel.FechaEntrada <= fechaFin.AddDays(1) &&
                                (orden != "" ? sel.Ot.Trim() == orden : true) &&
                                (proceso != "" ? proceso == "SELLADO" ? sel.NomStatus == proceso && !machines.Contains(sel.Maquina) || (sel.NomStatus == "Wiketiado" && sel.Maquina == "50") : proceso == "Wiketiado" ? (sel.NomStatus == proceso && sel.Maquina != "50") : proceso == "CAMISILLA" ? sel.NomStatus == "SELLADO" && machines.Contains(sel.Maquina) : false : true) &&
                                (cliente != "" ? sel.Cliente == cliente : true) &&
                                (producto != "" ? sel.Referencia == producto : true) &&
                                (turno != "" ? turno == "DIA" ? turnosDia.Contains(sel.Turnos) : turno == "NOCHE" ? turnosNoche.Contains(sel.Turnos) : true : true) &&
                                (envioZeus != "" ? envioZeus != "Todo" ? envioZeus == sel.EnvioZeus : true : true) &&
                                (Convert.ToInt32(sel.Ot.Trim()) == cl.Item) &&
                                (sel.Item >= FirstRollDaySealed(fechaInicio).FirstOrDefault()) &&
                                (RollsNightSealed(fechaFin.AddDays(1)).Any() ? sel.Item <= (RollsNightSealed(fechaFin.AddDays(1)).FirstOrDefault()) : sel.FechaEntrada <= fechaFin)
                                && sel.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe" &&
                                (maquina != "" ? Convert.ToString(sel.Maquina) == maquina : true) &&
                                (operario != "" ? (Convert.ToString(sel.Operario) == operario || Convert.ToString(sel.Operario2) == operario || Convert.ToString(sel.Operario3) == operario || Convert.ToString(sel.Operario4) == operario) : true)
                                
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
                              PesoTeorico = Convert.ToDecimal(sel.Pesot),
                              Desviacion =  (((Convert.ToDecimal(sel.Peso) / Convert.ToDecimal(sel.Pesot)) * 100) - 100),
                              Turno = Convert.ToString(sel.Turnos),
                              Fecha =sel.FechaEntrada,
                              Hora = Convert.ToString(sel.Hora),
                              Maquina = Convert.ToInt32(sel.Maquina),
                              EnvioZeus = Convert.ToString(sel.EnvioZeus),
                              Proceso = sel.NomStatus,
                              CantPedida = sel.Unidad == "KLS" ? cl.DatosotKg : cl.DatoscantBolsa,
                              Observacion = sel.Observaciones,
                              Operario = sel.Operario, 
                           }).ToList();

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
            int count = 0;
            foreach(var rollo in rollos)
            {
                var datos = (from pro in _context.Set<ProcExtrusion>()
                             join ot in _context.Set<ClientesOt>() on Convert.ToString(pro.Ot) equals Convert.ToString(ot.Item)
                             where pro.Item == rollo &&
                                   pro.EnvioZeus.Trim() == "0" &&
                                   pro.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                             select new
                             {
                                 Orden = pro.Ot,
                                 Item = pro.ClienteItem,
                                 Presentacion = ot.PtPresentacionNom,
                                 Rollo = pro.Item,
                                 Cantidad = pro.Extnetokg,
                                 Costo = ot.DatoscantKg
                             }).FirstOrDefault();


                if (datos != null)
                {
                    //await EnviarAjuste(datos.Orden, datos.Item, datos.Presentacion, datos.Rollo, datos.Cantidad, Convert.ToDecimal(datos.Costo));
                    await PutEnvioZeus(datos.Rollo);
                    count++;
                    if (count == rollos.Count) return Ok();
                }
                else continue;
            }

            return Ok();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpGet("EnviarAjuste/{rollo}")]
        public async Task<ActionResult> EnviarAjuste(int rollo)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
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
                             Cantidad = Convert.ToString(pro.Extnetokg),
                             Costo = Convert.ToString(ot.DatoscantKg)

                         }).FirstOrDefault();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            if (datos == null) return Ok();

            string today = DateTime.Today.ToString("yyyy-MM-dd");
            SoapRequestAction request = new SoapRequestAction();
            request.User = "wsZeusInvProd";
            request.Password = "wsZeusInvProd";
            request.Body = $"<Ajuste>" +
                                $"<Op>I</Op>" +
                                $"<Cabecera>" +
                                    $"<Detalle>{Convert.ToString(datos.Orden)}</Detalle>" +
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
                                    $"<CodigoArticulo>{datos.Item}</CodigoArticulo>" +
                                    $"<Presentacion>{datos.Presentacion}</Presentacion>" +
                                    "<CodigoLote>0</CodigoLote>" +
                                    "<CodigoBodega>003</CodigoBodega>" +
                                    "<CodigoUbicacion></CodigoUbicacion>" +
                                    "<CodigoClasificacion>0</CodigoClasificacion>" +
                                    "<CodigoReferencia></CodigoReferencia>" +
                                    "<Serial>0</Serial>" +
                                    $"<Detalle>{datos.Rollo}</Detalle>" +
                                    $"<Cantidad>{datos.Cantidad}</Cantidad>" +
                                    $"<PrecioUnidad>{datos.Costo}</PrecioUnidad>" +
                                    $"<PrecioUnidad2>{datos.Costo}</PrecioUnidad2>" +
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
                                      pe.NomStatus == proceso &&
                                      pe.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                                orderby pe.Item descending
                                select pe;
            return rollosPesados.Any() ? Ok(rollosPesados) : BadRequest();
        }

        //Función que retornará los rollos de Bagpro por ot y proceso sin tener en cuenta los rollos que vienen por parametro.
        [HttpPost("getAvailablesRollsOT/{orden}/{proceso}")]
        public ActionResult getAvailablesRollsOT(string orden, string proceso, [FromBody] List<int> rollos)
        {
            var rollosPesados = from pe in _context.Set<ProcExtrusion>()
                                where pe.Ot == orden &&
                                      pe.NomStatus == proceso &&
                                      !rollos.Contains(pe.Item) &&
                                      pe.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                                orderby pe.Item descending
                                select pe;

            if(rollosPesados.Any()) return Ok(rollosPesados);
            else return Ok(from ps in _context.Set<ProcExtrusion>()
                           where ps.Ot == orden &&
                                 ps.NomStatus == proceso &&
                                 !rollos.Contains(ps.Item) &&
                                 ps.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                           orderby ps.Item descending
                           select ps);
        }

        [HttpGet("getInformactionProductionForTag/{production}")]
        public ActionResult GetInformactionProductionForTag(int production)
        {
            var data = from pe in _context.Set<ProcExtrusion>()
                       where pe.Observaciones == $"Rollo #{production} en PBDD.dbo.Produccion_Procesos"
                       select pe;
            return data.Any() ? Ok(data.Take(1)) : BadRequest();
        }

        [HttpGet("getProductionByNumber/{production}/{searchIn}")]
        public ActionResult GetProductionByNumber(int production, string searchIn)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var datos = from ps in _context.Set<ProcSellado>() where ps.Item == production && ps.EnvioZeus.Trim() == "0" && ps.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe" && ps.NomStatus.Trim() == "SELLADO" select ps;
            if (datos.Any() && (searchIn == "TODO" || searchIn == "SELLADO")) return Ok(datos);
            else 
            {
                var datos2 = from pe in _context.Set<ProcExtrusion>() where pe.Item == production && pe.Observacion != "Etiqueta eliminada desde App Plasticaribe" && (pe.NomStatus.Trim() == "EMPAQUE" || pe.NomStatus.Trim() == "EXTRUSION") && pe.EnvioZeus.Trim() == "0" select pe;
                if (datos2.Any() && (searchIn == "TODO" || searchIn == "EXTRUSION")) return Ok(datos2);
                else return NotFound();
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpGet("getProductionForExitByNumber/{production}")]
        public ActionResult GetProductionForExitByNumber(int production)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var datos = from pe in _context.Set<ProcExtrusion>()
                        where pe.Item == production &&
                              pe.NomStatus == "EMPAQUE" &&
                              pe.Observaciones.StartsWith("Rollo #") &&
                              pe.EnvioZeus.Trim() == "1" &&
                              pe.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                        select pe;
            if (datos.Count() > 0) return Ok(datos);
            else
            {
                var datos2 = from ps in _context.Set<ProcSellado>()
                             where ps.Item == production &&
                                   ps.Observaciones.StartsWith("Rollo #") &&
                                   ps.EnvioZeus.Trim() == "1" &&
                                   ps.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe"
                             select ps;
                return Ok(datos2);
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpGet("getNumberReelByNumberAndProcess/{number}/{process}")]
        public ActionResult GetNumberReelByNumberAndProcess(int number, string process)
        {
            if (process == "WIKETIADO") process = "Wiketiado";

            var itemPE = from pe in _context.Set<ProcExtrusion>() where pe.Observaciones == $"Rollo #{number} en PBDD.dbo.Produccion_Procesos" && pe.NomStatus == process && pe.Observacion != "Etiqueta eliminada desde App Plasticaribe" select pe.Item;

            if (itemPE.Any()) return Ok(itemPE);
            else {
                var itemPS = from pe in _context.Set<ProcSellado>() where pe.Observaciones == $"Rollo #{number} en PBDD.dbo.Produccion_Procesos" && pe.NomStatus == process && pe.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe" select pe.Item;
                return Ok(itemPS);
            }
        }

        // Consulta que devolverá la información del rollo
        [HttpGet("getProductionByProduction/{production}")]
        public ActionResult GetProductionByProduction(int production)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var datos = from ps in _context.Set<ProcSellado>()
                        where ps.Observaciones == $"Rollo #{production} en PBDD.dbo.Produccion_Procesos" &&
                              ps.NomStatus == "SELLADO" &&
                              ps.EnvioZeus.Trim() == "0" &&
                              ps.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe"
                        select ps;
            if (datos.Any()) return Ok(datos);
            else
            {
                var datos2 = from pe in _context.Set<ProcExtrusion>()
                        where pe.Observaciones == $"Rollo #{production} en PBDD.dbo.Produccion_Procesos" &&
                              pe.EnvioZeus.Trim() == "0" && 
                              pe.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                             /*pe.NomStatus == "EMPAQUE"*/
                             select pe;
                return Ok(datos2);
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //Consulta que devolverá el numero del bulto desde cualquier proceso
        [HttpGet("getRollProduction/{roll}")]
        public ActionResult getRollProduction(int roll, string? process = "")
        {
            var data = (from pe in _context.Set<ProcExtrusion>()
                        join ot in _context.Set<ClientesOt>() on pe.Ot.Trim() equals Convert.ToString(ot.Item)
                        where pe.Item == roll && 
                        pe.NomStatus.Contains(process) &&
                        pe.Ot.Trim() == Convert.ToString(ot.Item)
                        select new
                        {
                            Rollo = pe.Item,
                            OT = ot.Item,
                            Cliente = ot.ClienteNom.Trim(),
                            Item = ot.ClienteItems,
                            Referencia = ot.ClienteItemsNom.Trim(),
                            Peso = pe.Extnetokg,
                            Unidad = Convert.ToString("Kg"),
                            Proceso = pe.NomStatus.Trim(),
                            Id_Material = Convert.ToInt32(ot.ExtMaterial.Trim()),
                            Material = ot.ExtMaterialNom.Trim(),
                            Id_Pigmento_Extrusion = Convert.ToInt32(ot.ExtPigmento.Trim()),
                            Pigmento_Extrusion = ot.ExtPigmentoNom.Trim(),
                            Fecha = pe.Fecha.Value.ToString("yyyy-MM-dd"),
                            Maquina = pe.Maquina,
                            Operario = pe.Operador.Trim(),
                            Hora = pe.Hora, 
                            Turno = pe.Turno,
                            Cantidad = pe.Extnetokg,
                            Presentacion = ot.PtPresentacionNom.Trim() == "Kilo" ? "Kg" : ot.PtPresentacionNom.Trim() == "Unidad" ? "Und" : "Paquete"
                        }).FirstOrDefault();

            if (data == null) return Ok((from ps in _context.Set<ProcSellado>() 
                                         join ot in _context.Set<ClientesOt>() on ps.Ot.Trim() equals Convert.ToString(ot.Item)
                                         where ps.Item == roll && 
                                         ps.NomStatus.Contains(process) &&
                                         ps.Ot.Trim() == Convert.ToString(ot.Item)
                                         select new 
                                         { 
                                             Rollo = ps.Item,
                                             OT = ot.Item,
                                             Cliente = ot.ClienteNom.Trim(),
                                             Item = ot.ClienteItems,
                                             Referencia = ot.ClienteItemsNom.Trim(),
                                             Peso = ps.Peso,
                                             Unidad = Convert.ToString("Kg"),
                                             Proceso = ps.NomStatus.Trim(),
                                             Id_Material = Convert.ToInt32(ot.ExtMaterial.Trim()),
                                             Material = ot.ExtMaterialNom.Trim(),
                                             Id_Pigmento_Extrusion = Convert.ToInt32(ot.ExtPigmento.Trim()),
                                             Pigmento_Extrusion = ot.ExtPigmentoNom.Trim(),
                                             Fecha = ps.FechaEntrada.ToString("yyyy-MM-dd"),
                                             Maquina = ps.Maquina,
                                             Operario = ps.Operario.Trim(),
                                             Hora = ps.Hora,
                                             Turno = ps.Turnos,
                                             Cantidad = ps.Qty, 
                                             Presentacion = ot.PtPresentacionNom.Trim() == "Kilo" ? "Kg" : ot.PtPresentacionNom.Trim() == "Unidad" ? "Und" : "Paquete"
                                         }).FirstOrDefault());
            else return Ok(data);
        }

        //Consulta que devolverá el numero del bulto desde cualquier proceso
        [HttpGet("getPayRollCourt/{date1}/{date2}")]
        public ActionResult getPayRollCourt(DateTime date1, DateTime date2)
        {
            var payRoll = from p in _context.Set<ProcExtrusion>()
                          join cl in _context.Set<ClientesOtItem>() on p.ClienteItem.Trim() equals cl.ClienteItems.ToString().Trim()
                          where p.Fecha >= date1 && //02
                          p.Fecha <= date2.AddDays(1) && //03+1 = 04
                          p.NomStatus == "EMPAQUE" &&
                          p.Item >= FirstRollDayExtrusion(date1).FirstOrDefault() &&
                          (RollsNightExtrusion(date2.AddDays(1)).Any() ? p.Item <= (RollsNightExtrusion(date2.AddDays(1)).FirstOrDefault()) : p.Fecha <= date2) &&
                          p.Observacion == null
                          select new
                          {
                              Roll = p.Item,
                              Item = p.ClienteItem.Trim(),
                              Reference = p.ClienteItemNombre.Trim(),
                              OT = p.Ot.Trim(),
                              Weight = p.Extnetokg,
                              Operator = p.Operador,
                              Date = p.Fecha,
                              Hour = p.Hora.Trim(),
                              Turn = p.Turno.Trim(),
                              Value_Production = p.Estado.Trim() == "0" ? Convert.ToDateTime(p.Fecha).DayOfWeek == DayOfWeek.Sunday ? cl.DiaC.Value == Convert.ToDecimal(86.46) ? Convert.ToDecimal(125.01) :
                                                                                                                                      cl.DiaC.Value == Convert.ToDecimal(132.08) ? Convert.ToDecimal(175.47) :
                                                                                                                                      cl.DiaC.Value == Convert.ToDecimal(120.88) ? Convert.ToDecimal(170.67) : 0 : p.Turno == "DIA" ? cl.DiaC.Value : p.Turno == "NOCHE" ? cl.NocheC.Value : 0 : Convert.ToDateTime(p.Fecha).DayOfWeek == DayOfWeek.Sunday ? Convert.ToDecimal(250.02) : p.Turno == "DIA" ? Convert.ToDecimal(173.17) : p.Turno == "NOCHE" ? Convert.ToDecimal(207.73) : 0,
                              Value_Pay = p.Estado.Trim() == "0" ? Convert.ToDateTime(p.Fecha).DayOfWeek == DayOfWeek.Sunday ? cl.DiaC.Value == Convert.ToDecimal(86.46) ? Convert.ToDecimal(125.01) * p.Extnetokg :
                                                                                                                               cl.DiaC.Value == Convert.ToDecimal(132.08) ? Convert.ToDecimal(175.47) * p.Extnetokg :
                                                                                                                               cl.DiaC.Value == Convert.ToDecimal(120.88) ? Convert.ToDecimal(170.67) * p.Extnetokg : 0 : p.Turno == "DIA" ? Convert.ToDecimal(cl.DiaC.Value * p.Extnetokg) : p.Turno == "NOCHE" ? Convert.ToDecimal(cl.NocheC.Value * p.Extnetokg) : 0 : Convert.ToDateTime(p.Fecha).DayOfWeek == DayOfWeek.Sunday ? Convert.ToDecimal(250.02) * p.Extnetokg : p.Turno == "DIA" ? Convert.ToDecimal(173.17) * p.Extnetokg : p.Turno == "NOCHE" ? Convert.ToDecimal(207.73) * p.Extnetokg : 0,

                              Value_Day = Convert.ToDecimal(cl.DiaC.Value),
                              Value_Night = Convert.ToDecimal(cl.NocheC.Value),
                              Value_Sunday = cl.DiaC.Value == Convert.ToDecimal(86.46) ? Convert.ToDecimal(125.01) :
                                             cl.DiaC.Value == Convert.ToDecimal(132.08) ? Convert.ToDecimal(175.47) :
                                             cl.DiaC.Value == Convert.ToDecimal(120.88) ? Convert.ToDecimal(170.67) : 0,
                              Value_Rewinding = p.Estado.Trim() == "1" ? Convert.ToDateTime(p.Fecha).DayOfWeek == DayOfWeek.Sunday ? Convert.ToDecimal(250.02) : p.Turno == "DIA" ? Convert.ToDecimal(173.17) : p.Turno == "NOCHE" ? Convert.ToDecimal(207.73) : 0 : 0,
                              Position_Job = "Operario Corte",
                              Machine = p.Maquina,
                              Send_Zeus = p.EnvioZeus.Trim(),
                              Sunday = Convert.ToDateTime(p.Fecha).DayOfWeek == DayOfWeek.Sunday,
                              Material = p.Material.Trim(),
                              Printed = cl.ImpTinta1 != "1" ? "SI" : "NO",
                              Laminate = cl.LamCapa2Nom.Trim(),
                              Rewinding = p.Estado == "1" ? "SI" : "NO",
                              Concept = "PRODUCCION"
                              //p.Turno == "DIA" && Convert.ToDateTime(p.Fecha).DayOfWeek == DayOfWeek.Sunday ? cl.DiaC.Value :
                              //Value_Production = Convert.ToDateTime(p.Fecha).DayOfWeek == DayOfWeek.Sunday ? (cl.DiaC.Value * Convert.ToDecimal(0.35)) + cl.DiaC.Value : p.Turno == "DIA" ? cl.DiaC.Value : p.Turno == "NOCHE" ? cl.NocheC.Value : 0,
                              /* Value_Pay = p.Turno == "DIA" ? Convert.ToDecimal(cl.DiaC.Value * p.Extnetokg) : p.Turno == "NOCHE" ? Convert.ToDecimal(cl.NocheC.Value * p.Extnetokg) : 0,
                              Value_Day = Convert.ToDecimal(cl.DiaC.Value),
                              Value_Night = Convert.ToDecimal(cl.NocheC.Value),
                              Value_Sunday = Convert.ToDateTime(p.Fecha).DayOfWeek == DayOfWeek.Sunday ? (cl.DiaC.Value * Convert.ToDecimal(0.35)) + cl.DiaC.Value : (cl.DiaC.Value * Convert.ToDecimal(0.35)) + cl.DiaC.Value,
                              Position_Job = "Operario Corte",
                              Machine = p.Maquina,
                              Send_Zeus = p.EnvioZeus,
                              Sunday = Convert.ToDateTime(p.Fecha).DayOfWeek == DayOfWeek.Sunday*/

                          };

            if (date1 >= Convert.ToDateTime("2025-03-03")) return Ok(payRoll);
            else return Ok(payRoll);
        }

         [HttpPost("getAvaibleProduction/{item}")]
        public IActionResult getAvaibleProduction(string item, [FromBody] List<long> notAvaibleProduction)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var sellado = from p in _context.Set<ProcSellado>()
                          join coi in _context.Set<ClientesOtItem>() on Convert.ToString(p.Referencia) equals Convert.ToString(coi.ClienteItems)
                          where p.Referencia == item &&
                                !notAvaibleProduction.Contains(p.Item) &&
                                p.EnvioZeus.Trim() == "1" &&
                                p.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe"
                          select new
                          {
                              Item = coi.ClienteItems,
                              Reference = coi.ClienteItemsNom,
                              NumberProduction = p.Item,
                              Quantity = p.Qty,
                              Presentation = coi.PtPresentacionNom,
                          };

            if (sellado.Any()) return Ok(sellado);
            else
            {
                var extrusion = from p in _context.Set<ProcExtrusion>()
                                join coi in _context.Set<ClientesOtItem>() on Convert.ToString(p.ClienteItem) equals Convert.ToString(coi.ClienteItems)
                                where p.ClienteItem == item &&
                                      !notAvaibleProduction.Contains(p.Item) &&
                                      p.EnvioZeus.Trim() == "1" &&
                                      p.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                                select new
                                {
                                    Item = coi.ClienteItems,
                                    Reference = coi.ClienteItemsNom,
                                    NumberProduction = p.Item,
                                    Quantity = coi.PtPresentacionNom == "Kilo" ? p.Extnetokg : 1,
                                    Presentation = coi.PtPresentacionNom,
                                };

                return extrusion.Any() ? Ok(extrusion) : NotFound();
            }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //Primer rollo del dia extrusion
        private IQueryable<int> FirstRollDayExtrusion(DateTime date)
        {
            string[] process = { "EXTRUSION", "IMPRESION", "ROTOGRABADO", "EMPAQUE", "DOBLADO", "LAMINADO" };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return from PS in _context.Set<ProcExtrusion>()
                       where PS.Fecha == date &&
                            (!PS.Hora.StartsWith("00") &&
                            !PS.Hora.StartsWith("01") &&
                            !PS.Hora.StartsWith("02") &&
                            !PS.Hora.StartsWith("03") &&
                            !PS.Hora.StartsWith("04") &&
                            !PS.Hora.StartsWith("05") &&
                            !PS.Hora.StartsWith("06") /*&&
                            !PS.Hora.StartsWith("07:0") &&
                            !PS.Hora.StartsWith("07:1") &&
                            !PS.Hora.StartsWith("07:2")*/) &&
                            process.Contains(PS.NomStatus) &&
                            PS.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                   orderby PS.Item ascending
                   select PS.Item;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //Primer rollo del dia extrusion
        [HttpGet("FirstRollDayExtrusion2/{date}")]
        public ActionResult FirstRollDayExtrusion2(DateTime date)
        {
            string[] process = { "EXTRUSION", "IMPRESION", "ROTOGRABADO", "EMPAQUE", "DOBLADO", "LAMINADO" };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return Ok(from PS in _context.Set<ProcExtrusion>()
                      where PS.Fecha == date &&
                           (!PS.Hora.StartsWith("00") &&
                           !PS.Hora.StartsWith("01") &&
                           !PS.Hora.StartsWith("02") &&
                           !PS.Hora.StartsWith("03") &&
                           !PS.Hora.StartsWith("04") &&
                           !PS.Hora.StartsWith("05") &&
                           !PS.Hora.StartsWith("06") &&
                           !PS.Hora.StartsWith("07:0") &&
                           !PS.Hora.StartsWith("07:1") &&
                           !PS.Hora.StartsWith("07:2")) &&
                           process.Contains(PS.NomStatus) &&
                           PS.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                      orderby PS.Item ascending
                      select PS.Item);
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //Primer rollo del dia sellado
        private IQueryable<int> FirstRollDaySealed(DateTime date)
        {
            //string[] process = { "SELLADO", "Wiketiado" };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return from PS in _context.Set<ProcSellado>()
                        where PS.FechaEntrada == date &&
                             (!PS.Hora.StartsWith("00") &&
                             !PS.Hora.StartsWith("01") &&
                             !PS.Hora.StartsWith("02") &&
                             !PS.Hora.StartsWith("03") &&
                             !PS.Hora.StartsWith("04") &&
                             !PS.Hora.StartsWith("05") &&
                             !PS.Hora.StartsWith("06") &&
                             !PS.Hora.StartsWith("07:0") &&
                             !PS.Hora.StartsWith("07:1") &&
                             !PS.Hora.StartsWith("07:2")) &&
                             //process.Contains(PS.NomStatus) &&
                              PS.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe"
                   orderby PS.Item ascending
                        select PS.Item;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //Rollos de la noche en extrusión
        private IQueryable<int> RollsNightExtrusion(DateTime date)
        {
            string[] process = { "EXTRUSION", "IMPRESION", "ROTOGRABADO", "EMPAQUE", "DOBLADO", "LAMINADO" };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return from PS in _context.Set<ProcExtrusion>()
                   where PS.Fecha == date &&
                        (PS.Hora.StartsWith("00") ||
                        PS.Hora.StartsWith("01") ||
                        PS.Hora.StartsWith("02") ||
                        PS.Hora.StartsWith("03") ||
                        PS.Hora.StartsWith("04") ||
                        PS.Hora.StartsWith("05") ||
                        PS.Hora.StartsWith("06") /*||
                        PS.Hora.StartsWith("07:0") ||
                        PS.Hora.StartsWith("07:1") ||
                        PS.Hora.StartsWith("07:2")*/) &&
                        process.Contains(PS.NomStatus) &&
                        PS.Observacion != "Etiqueta eliminada desde App Plasticaribe"
                   orderby PS.Item descending
                   select PS.Item;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        //Rollos de la noche en sellado
        private IQueryable<int> RollsNightSealed(DateTime date)
        {
            //string[] process = { "SELLADO", "Wiketiado" };
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return from PS in _context.Set<ProcSellado>()
                   where PS.FechaEntrada == date &&
                        (PS.Hora.StartsWith("00") ||
                        PS.Hora.StartsWith("01") ||
                        PS.Hora.StartsWith("02") ||
                        PS.Hora.StartsWith("03") ||
                        PS.Hora.StartsWith("04") ||
                        PS.Hora.StartsWith("05") ||
                        PS.Hora.StartsWith("06") ||
                        PS.Hora.StartsWith("07:0") ||
                        PS.Hora.StartsWith("07:1") ||
                        PS.Hora.StartsWith("07:2")) &&
                        //process.Contains(PS.NomStatus) &&
                        PS.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe"
                   orderby PS.Item descending
                   select PS.Item;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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

        [HttpPut("putEnvioZeus/{rollo}")]
        public async Task<IActionResult> PutEnvioZeus(int rollo)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var procExtrusion = (from pro in _context.Set<ProcExtrusion>() where pro.Item == rollo && pro.Observacion != "Etiqueta eliminada desde App Plasticaribe" select pro).FirstOrDefault();
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


        //Función que recibirá los rollos a los que se les revertirá (actualizará) el Envio Zeus a 0
        [HttpPost("putReversionEnvioZeus_ProcExtrusion")]
        async public Task<IActionResult> putReversionEnvioZeus_ProcExtrusion([FromBody] List<int> rolls)
        {
            int count = 0;
            foreach (var roll in rolls)
            {
                var procExtrusion = (from pro in _context.Set<ProcExtrusion>() where pro.Item == roll && pro.Observacion != "Etiqueta eliminada desde App Plasticaribe" select pro).FirstOrDefault();
                procExtrusion.EnvioZeus = "0";
                _context.Entry(procExtrusion).State = EntityState.Modified;
                _context.SaveChanges();
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProcExtrusionExists(roll)) return NotFound();
                    else throw;
                }
                count++;
                if (count == rolls.Count()) return NoContent();
            }
            return NoContent();
        }

        //Función que agregará una observacion de los rollos eliminados.
        [HttpPost("putObservationDeletedRolls")]
        async public Task<IActionResult> putObservationDeletedRolls([FromBody] List<rollsToDelete> rollsToDelete)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            string[] processProduction = { "EMP", "EXT", "IMP", "ROT", "LAM", "DBLD", "PERF" };
            
            foreach(var rolls in rollsToDelete) 
            { 
                if (processProduction.Contains(rolls.process))
                {
                    var roll = (from pe in _context.Set<ProcExtrusion>() where pe.Item == rolls.roll && processProduction.Contains(pe.NomStatus.Substring(0, 3)) select pe).FirstOrDefault();
                    roll.Observacion = "Etiqueta eliminada desde App Plasticaribe";
                    _context.Entry(roll).State = EntityState.Modified;
                    _context.SaveChanges();
                    try
                    {
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        return NotFound();
                    }
                } 
                else if (rolls.process == "SELLA") 
                {
                    var roll = (from ps in _context.Set<ProcSellado>() where ps.Item == rolls.roll && ps.NomStatus == rolls.process.Replace("SELLA", "SELLADO") select ps).FirstOrDefault();
                    if (roll != null)
                    {
                        var update = _context.Database.ExecuteSql($"UPDATE ProcSellado SET RemplazoItem = 'Etiqueta eliminada desde App Plasticaribe' WHERE Item = {roll.Item}");
                    }
                    else 
                    {
                        return NotFound();
                    }
                    
                }
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
                     where y.Item == rollo && y.Observacion != "Etiqueta eliminada desde App Plasticaribe"
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

public class rollsToDelete
{
    //[RegularExpression("^[1-9]\\d*$", ErrorMessage = "Invalid fieldName.")]
    public long roll { get; set; }

    //[RegularExpression("^[1-9]\\d*$", ErrorMessage = "Invalid fieldName.")]
    public string process { get; set; } = null!;

}
