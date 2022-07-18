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
