using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BagproWebAPI.Models;

namespace BagproWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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

        [HttpGet("OT/{ot}")]
        public ActionResult<ProcExtrusion> GetOT(string ot)
        {
            var procExtrusion = _context.ProcExtrusions.Where(prExt => prExt.Ot == ot).ToList();

            if (procExtrusion == null)
            {
                return NotFound();
            }

            return Ok(procExtrusion);
        }

        [HttpGet("Fechas/{fechaini}/{fechafin}")]
        public ActionResult<ProcExtrusion> GetOT(DateTime fechaini, DateTime fechafin)
        {
            var procExtrusion = _context.ProcExtrusions.Where(x => x.Fecha >= fechaini && x.Fecha <= fechafin).ToList();

            if (procExtrusion == null)
            {
                return NotFound();
            }

            return Ok(procExtrusion);
        }

        [HttpGet("Rollos/{Rollo}")]
        public ActionResult Get(int Rollo)
        {
            var con = from ext in _context.Set<ProcExtrusion>()
                      from cli in _context.Set<Cliente>()
                      where ext.Item == Rollo /*&& ext.NomStatus == "EMPAQUE" && ext.NomStatus == "EXTRUSION"*/
                            && (cli.CodBagpro == ext.Cliente || cli.NombreComercial == ext.ClienteNombre)
                      select new
                      {
                          ext.Ot,
                          ext.Item,
                          cli.IdentNro,
                          cli.NombreComercial,
                          ext.ClienteItem,
                          ext.ClienteItemNombre,
                          ext.Extnetokg,
                          unidad = "Kg",
                          ext.NomStatus,
                          ext.Fecha
                      };
            return Ok(con);
        }

        [HttpGet("FechasOT/{fechaini}/{fechafin}/{ot}")]
        public ActionResult<ProcExtrusion> GetOT(DateTime fechaini, DateTime fechafin, string ot)
        {
            var procExtrusion = _context.ProcExtrusions.Where(x => x.Fecha >= fechaini && x.Fecha <= fechafin && x.Ot == ot).ToList();

            if (procExtrusion == null)
            {
                return NotFound();
            }

            return Ok(procExtrusion);
        }

        [HttpGet("FechaFinOT/{ot}")]
        public ActionResult<ProcExtrusion> GetFechaFinOT(string ot)
        {
            var ProcesoEmpaque = "EMPAQUE";
            var procExtrusion = _context.ProcExtrusions.Where(prExt => prExt.Ot == ot &&
                                                              prExt.NomStatus == ProcesoEmpaque)
                                                       .OrderByDescending(x => x.Fecha)
                                                       .Select(ProcExt => new
                                                       {
                                                           ProcExt.Item,
                                                           ProcExt.Ot,
                                                           ProcExt.Fecha,
                                                           ProcExt.NomStatus

                                                       })
                                                       .First();
                                                       

            if (procExtrusion == null)
            {
                return NotFound();
            } else
            {
                return Ok(procExtrusion);
            }


        }

        [HttpGet("OtConEmpaque/{ot}")]
        public ActionResult<ProcExtrusion> GetOTEmpaque(string ot)
        {
            /** Consulta para obtener la suma realizada en KG en el proceso de empaque en una OT */            
            var ProcesoEmpaque = "EMPAQUE";
            var procSellado = _context.ProcExtrusions.Where(prExt => prExt.Ot == ot &&
                                                          prExt.NomStatus == ProcesoEmpaque)
                                                       .GroupBy(agr => new { agr.Ot })
                                                       .Select(ProEmpaque => new
                                                       {
                                                           ProEmpaque.Key.Ot,
                                                           SumaPeso = ProEmpaque.Sum(sma => sma.Extnetokg)
                                                       });


            if (procSellado == null)
            {   
                return NotFound();
            }
            else
            {
                return Ok(procSellado);
            }
        }

        [HttpGet("ContarOtEnEmpaque/{ot}")]
        public ActionResult<ProcExtrusion> GetConteoOTEmpaque(string ot)
        {
            /** Consulta para obtener la suma realizada en KG en el proceso de empaque en una OT */
            var ProcesoEmpaque = "EMPAQUE";
            var prEmpaque = _context.ProcExtrusions.Where(prExt => prExt.Ot == ot &&
                                                          prExt.NomStatus == ProcesoEmpaque)
                                                       .GroupBy(agr => new { agr.Ot, agr.NomStatus })
                                                       .Select(ProEmpaque => new
                                                       {
                                                           ProEmpaque.Key.Ot,
                                                           ProEmpaque.Key.NomStatus,
                                                           conteoFilas = ProEmpaque.Count()
                                                       });

            if (prEmpaque == null)
            {
                return NotFound();
                
            }
            else
            {
                return Ok(prEmpaque);
            }
        }

        [HttpGet("MostrarRollos/{Rollo}/{item}")]
        public ActionResult GetRollosOT(int Rollo, string item)
        {


            

            var procExtrusion = from sc2 in _context.Set<ProcExtrusion>()
                                 where sc2.Item == Rollo && sc2.ClienteItem == item
                                 select new
                                 {
                                    Rollo = Convert.ToString(sc2.Item),
                                    Cod_BagPro = Convert.ToString(sc2.Cliente),
                                    NombreCliente = Convert.ToString(sc2.ClienteNombre),
                                    Item = Convert.ToString(sc2.ClienteItem),
                                    NombreItem = Convert.ToString(sc2.ClienteItemNombre)
                                 };


   

            var procesoSellado = from sc3 in _context.Set<ProcSellado>()
                                 where sc3.Item == Rollo && sc3.Referencia == item
                                 select new
                                 {
                                     Rollo = Convert.ToString(sc3.Item),
                                     Cod_BagPro = Convert.ToString(sc3.CodCliente),
                                     NombreCliente = Convert.ToString(sc3.Cliente),
                                     Item =Convert.ToString( sc3.Referencia),
                                     NombreItem = Convert.ToString(sc3.NomReferencia)

                                 };

                return Ok(procExtrusion.Concat(procesoSellado));
            
        }

        [HttpGet("RollosOT/{ot}")]
        public ActionResult<ProcExtrusion> Get(string ot)
        {
            var con = from ext in _context.Set<ProcExtrusion>()
                      from cli in _context.Set<Cliente>()
                      where ext.Ot == ot
                            && (cli.CodBagpro == ext.Cliente || cli.NombreComercial == ext.ClienteNombre)
                      /*&& ext.NomStatus == "EMPAQUE" 
                      && ext.NomStatus == "EXTRUSION"*/
                      select new
                      {
                          ext.Ot,
                          ext.Item,
                          cli.IdentNro,
                          cli.NombreComercial,
                          ext.ClienteItem,
                          ext.ClienteItemNombre,
                          ext.Extnetokg,
                          unidad = "Kg",
                          ext.NomStatus,
                          ext.Fecha
                      };
            return Ok(con);
        }

        [HttpGet("FechasRollos/{fechaini}/{fechafin}")]
        public ActionResult<ProcExtrusion> GetFechas(DateTime fechaini, DateTime fechafin)
        {
            var con = from ext in _context.Set<ProcExtrusion>()
                      from cli in _context.Set<Cliente>()                     
                      where ext.Fecha >= fechaini
                            && ext.Fecha <= fechafin
                            && (cli.CodBagpro == ext.Cliente || cli.NombreComercial == ext.ClienteNombre)
                      orderby (ext.Ot)
                      /*&& ext.NomStatus == "EMPAQUE"
                      && ext.NomStatus == "EXTRUSION"*/
                      select new
                      {
                          ext.Ot,
                          ext.Item,
                          cli.IdentNro,
                          cli.NombreComercial,
                          ext.ClienteItem,
                          ext.ClienteItemNombre,
                          ext.Extnetokg,
                          unidad = "Kg",
                          ext.NomStatus,
                          ext.Fecha
                      };
            return Ok(con);
        }

        [HttpGet("FechasOTRollos/{fechaini}/{fechafin}/{ot}")]
        public ActionResult<ProcExtrusion> GetFechasOT(DateTime fechaini, DateTime fechafin, string ot)
        {
            var con = from ext in _context.Set<ProcExtrusion>()
                      from cli in _context.Set<Cliente>()
                      where ext.Ot == ot
                            && ext.Fecha >= fechaini
                            && ext.Fecha <= fechafin
                            && (cli.CodBagpro == ext.Cliente || cli.NombreComercial == ext.ClienteNombre)
                      /*&& ext.NomStatus == "EMPAQUE"
                      && ext.NomStatus == "EXTRUSION"*/
                      select new
                      {
                          ext.Ot,
                          ext.Item,
                          cli.IdentNro,
                          cli.NombreComercial,
                          ext.ClienteItem,
                          ext.ClienteItemNombre,
                          ext.Extnetokg,
                          unidad = "Kg",
                          ext.NomStatus,
                          ext.Fecha
                      };
            return Ok(con);
        }

        /** Consultas INFO por NomStatus y OT*/

        /** Extrusión */
        [HttpGet("ObtenerDatosOTxExtrusion/{OT}")]
        public ActionResult<ProcExtrusion> GetObtenerStatusExtrusion(string OT)
        {
            /** Consulta para obtener la suma realizada en KG en el proceso de empaque en una OT */
            var Estatus = "EXTRUSION";
            var prSellado = _context.ProcExtrusions.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Estatus)
                                                 .OrderByDescending(proExt => proExt.Item)
                                                 .Select(ProExtru => new
                                                 {
                                                     ProExtru.Item,
                                                     ProExtru.Ot,
                                                     ProExtru.Cliente,
                                                     ProExtru.ClienteNombre,
                                                     ProExtru.ClienteItem,
                                                     ProExtru.ClienteItemNombre,                                                     
                                                     ProExtru.Extnetokg,
                                                     ProExtru.Operador,                                                    
                                                     ProExtru.Maquina,
                                                     ProExtru.Fecha,
                                                     ProExtru.NomStatus,
                                                     ProExtru.Turno,
                                                     ProExtru.Hora,
                                                     ProExtru.EnvioZeus
                                                 }).ToList();

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }


        /** Impresion */
        [HttpGet("ObtenerDatosOTxImpresion/{OT}")]
        public ActionResult<ProcExtrusion> GetObtenerStatusImpresion(string OT)
        {
            /** Consulta para obtener la suma realizada en KG en el proceso de empaque en una OT */
            var Estatus = "IMPRESION";
            var prSellado = _context.ProcExtrusions.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Estatus)
                                                 .OrderByDescending(proExt => proExt.Item)
                                                 .Select(ProExtru => new
                                                 {
                                                     ProExtru.Item,
                                                     ProExtru.Ot,
                                                     ProExtru.Cliente,
                                                     ProExtru.ClienteNombre,
                                                     ProExtru.ClienteItem,
                                                     ProExtru.ClienteItemNombre,
                                                     ProExtru.Extnetokg,
                                                     ProExtru.Operador,
                                                     ProExtru.Maquina,
                                                     ProExtru.Fecha,
                                                     ProExtru.NomStatus,
                                                     ProExtru.Turno,
                                                     ProExtru.Hora,
                                                     ProExtru.EnvioZeus
                                                 }).ToList();

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }


        /** Rotog. */
        [HttpGet("ObtenerDatosOTxRotograbado/{OT}")]
        public ActionResult<ProcExtrusion> GetObtenerStatusRotograbado(string OT)
        {
            /** Consulta para obtener la suma realizada en KG en el proceso de empaque en una OT */
            var Estatus = "ROTOGRABADO";
            var prSellado = _context.ProcExtrusions.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Estatus)
                                                 .OrderByDescending(proExt => proExt.Item)
                                                 .Select(ProExtru => new
                                                 {
                                                     ProExtru.Item,
                                                     ProExtru.Ot,
                                                     ProExtru.Cliente,
                                                     ProExtru.ClienteNombre,
                                                     ProExtru.ClienteItem,
                                                     ProExtru.ClienteItemNombre,
                                                     ProExtru.Extnetokg,
                                                     ProExtru.Operador,
                                                     ProExtru.Maquina,
                                                     ProExtru.Fecha,
                                                     ProExtru.NomStatus,
                                                     ProExtru.Turno,
                                                     ProExtru.Hora,
                                                     ProExtru.EnvioZeus
                                                 }).ToList();

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }


        /** Doblado */
        [HttpGet("ObtenerDatosOTxDoblado/{OT}")]
        public ActionResult<ProcExtrusion> GetObtenerStatusDoblado(string OT)
        {
            /** Consulta para obtener la suma realizada en KG en el proceso de empaque en una OT */
            var Estatus = "DOBLADO";
            var prSellado = _context.ProcExtrusions.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Estatus)
                                                 .OrderByDescending(proExt => proExt.Item)
                                                 .Select(ProExtru => new
                                                 {
                                                     ProExtru.Item,
                                                     ProExtru.Ot,
                                                     ProExtru.Cliente,
                                                     ProExtru.ClienteNombre,
                                                     ProExtru.ClienteItem,
                                                     ProExtru.ClienteItemNombre,
                                                     ProExtru.Extnetokg,
                                                     ProExtru.Operador,
                                                     ProExtru.Maquina,
                                                     ProExtru.Fecha,
                                                     ProExtru.NomStatus,
                                                     ProExtru.Turno,
                                                     ProExtru.Hora,
                                                     ProExtru.EnvioZeus
                                                 }).ToList();

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }


        /** Laminado */
        [HttpGet("ObtenerDatosOTxLaminado/{OT}")]
        public ActionResult<ProcExtrusion> GetObtenerStatusLaminado(string OT)
        {          
            var Estatus = "LAMINADO";
            var prSellado = _context.ProcExtrusions.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Estatus)
                                                 .OrderByDescending(proExt => proExt.Item)
                                                 .Select(ProExtru => new
                                                 {
                                                     ProExtru.Item,
                                                     ProExtru.Ot,
                                                     ProExtru.Cliente,
                                                     ProExtru.ClienteNombre,
                                                     ProExtru.ClienteItem,
                                                     ProExtru.ClienteItemNombre,
                                                     ProExtru.Extnetokg,
                                                     ProExtru.Operador,
                                                     ProExtru.Maquina,
                                                     ProExtru.Fecha,
                                                     ProExtru.NomStatus,
                                                     ProExtru.Turno,
                                                     ProExtru.Hora,
                                                     ProExtru.EnvioZeus
                                                 }).ToList();

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }


        /** Corte */
        [HttpGet("ObtenerDatosOTxCorte/{OT}")]
        public ActionResult<ProcExtrusion> GetObtenerStatusCorte(string OT)
        {
            var Estatus = "CORTE";
            var prSellado = _context.ProcExtrusions.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Estatus)
                                                 .OrderByDescending(proExt => proExt.Item)
                                                 .Select(ProExtru => new
                                                 {
                                                     ProExtru.Item,
                                                     ProExtru.Ot,
                                                     ProExtru.Cliente,
                                                     ProExtru.ClienteNombre,
                                                     ProExtru.ClienteItem,
                                                     ProExtru.ClienteItemNombre,
                                                     ProExtru.Extnetokg,
                                                     ProExtru.Operador,
                                                     ProExtru.Maquina,
                                                     ProExtru.Fecha,
                                                     ProExtru.NomStatus,
                                                     ProExtru.Turno,
                                                     ProExtru.Hora,
                                                     ProExtru.EnvioZeus
                                                 }).ToList();

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }


        /** Empaque */
        [HttpGet("ObtenerDatosOTxEmpaque/{OT}")]
        public ActionResult<ProcExtrusion> GetObtenerStatusEmpaque(string OT)
        {
            var Estatus = "EMPAQUE";
            var prSellado = _context.ProcExtrusions.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Estatus)
                                                 .OrderByDescending(proExt => proExt.Item)
                                                 .Select(ProExtru => new
                                                 {
                                                     ProExtru.Item,
                                                     ProExtru.Ot,
                                                     ProExtru.Cliente,
                                                     ProExtru.ClienteNombre,
                                                     ProExtru.ClienteItem,
                                                     ProExtru.ClienteItemNombre,
                                                     ProExtru.Extnetokg,
                                                     ProExtru.Operador,
                                                     ProExtru.Maquina,
                                                     ProExtru.Fecha,
                                                     ProExtru.NomStatus,
                                                     ProExtru.Turno,
                                                     ProExtru.Hora,
                                                     ProExtru.EnvioZeus
                                                 }).ToList();

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }


        /** Mostrar Datos Consolidados de ProcExtrusión Para modal en Vista Estados Procesos OT*/

        [HttpGet("MostrarDatosConsolidados_ProcExtrusion/{OT}/{Proceso}")]
        public ActionResult<ProcExtrusion> GetObtenerInfoConsolidadaProcExtrusion(string OT, string Proceso)
        {
            var prSellado = _context.ProcExtrusions.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Proceso)
                                                 .GroupBy(agr => new { agr.Fecha, agr.Operador, agr.ClienteItemNombre, agr.Ot, agr.NomStatus })
                                                 .Select(ProEx => new
                                                 {
                                                     SumaPesoKg = ProEx.Sum(ProSel => ProSel.Extnetokg),
                                                     ProEx.Key.Ot,
                                                     ProEx.Key.ClienteItemNombre,
                                                     ProEx.Key.Operador,
                                                     ProEx.Key.Fecha,
                                                     ProEx.Key.NomStatus
                                                 }).ToList();

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }

        
        

        // PUT: api/ProcExtrusion/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

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
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        private bool ProcExtrusionExists(int id)
        {
            return (_context.ProcExtrusions?.Any(e => e.Item == id)).GetValueOrDefault();
        }
    }
}
