using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BagproWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Drawing;
using System.Linq;

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
                                                     Hora  = ProExtru.Hora.Trim(),
                                                     Proceso =  ProExtru.NomStatus,
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
