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

        [HttpGet("MostrarRollos/{Rollo}")]
        public ActionResult GetRollosOT(int Rollo)
        {
            /**var procExtrusion = _context.ProcExtrusions.Where(prExt => prExt.Ot == OT
                                                      && prExt.NomStatus == "EMPAQUE")
                                                     .OrderBy(prExt => prExt.Ot)
                                                     .Select(agr => new
                                                     {
                                                         agr.Item,
                                                         agr.Cliente,
                                                         agr.ClienteNombre,
                                                         agr.ClienteItem,
                                                         agr.ClienteItemNombre,
                                                         agr.Extnetokg,
                                                         agr.NomStatus,
                                                         agr.EnvioZeus
                                                     })
                                                     .ToList();*/

            var procExtrusion = (
                                 from sc2 in _context.Set<ProcExtrusion>()
                                 join cl in _context.Set<Cliente>()
                                 on sc2.Cliente equals cl.CodBagpro
                                 where sc2.Item == Rollo
                                 select new
                                 {
                                    Rollo = Convert.ToString(sc2.Item),
                                    Cod_BagPro = Convert.ToString(sc2.Cliente),
                                    IDCliente = Convert.ToString(cl.IdentNro), 
                                    NombreCliente = Convert.ToString(sc2.ClienteNombre),
                                    Item = Convert.ToString(sc2.ClienteItem),
                                    NombreItem = Convert.ToString(sc2.ClienteItemNombre),
                                    Peso_Unidad = Convert.ToString(sc2.Extnetokg),
                                    Presentacion = Convert.ToString("Kg"),
                                    Proceso =Convert.ToString( sc2.NomStatus),                                   
                                    Envio = Convert.ToString(sc2.EnvioZeus)
                                 });

            /*var procesoSellado = 
                                 from sc3 in _context.Set<ProcSellado>()
                                 join cl2 in _context.Set<Cliente>()
                                 on sc3.Cliente equals cl2.CodBagpro
                                 where sc3.Item == Rollo
                                 select new
                                 {
                                     Rollo = Convert.ToString(sc3.Item),
                                     Cod_BagPro = Convert.ToString(sc3.CodCliente),
                                     IDCliente = Convert.ToString(cl2.IdentNro),
                                     NombreCliente = Convert.ToString(sc3.Cliente),
                                     Item =Convert.ToString( sc3.Referencia),
                                     NombreItem =Convert.ToString(sc3.NomReferencia),
                                     Peso_Unidad = Convert.ToString(sc3.Qty),
                                     Presentacion = Convert.ToString(sc3.Unidad),
                                     Proceso = Convert.ToString(sc3.NomStatus),
                                     Envio = Convert.ToString(sc3.EnvioZeus)
                                 };*/

                return Ok(procExtrusion/*.Concat(procesoSellado)*/);
            
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
