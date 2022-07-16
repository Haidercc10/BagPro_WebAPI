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
    public class ProcdesperdicioController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public ProcdesperdicioController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/Procdesperdicio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Procdesperdicio>>> GetProcdesperdicios()
        {
          if (_context.Procdesperdicios == null)
          {
              return NotFound();
          }
            return await _context.Procdesperdicios.ToListAsync();
        }

        // GET: api/Procdesperdicio/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Procdesperdicio>> GetProcdesperdicio(int id)
        {
          if (_context.Procdesperdicios == null)
          {
              return NotFound();
          }
            var procdesperdicio = await _context.Procdesperdicios.FindAsync(id);

            if (procdesperdicio == null)
            {
                return NotFound();
            }

            return procdesperdicio;
        }

        [HttpGet("OT/{ot}")]
        public ActionResult<Procdesperdicio> GetOT(string ot)
        {
            var procdesperdicio = _context.Procdesperdicios.Where(prdesp => prdesp.Ot == ot).ToList();

            if (procdesperdicio == null)
            {
                return NotFound();
            }

            return Ok(procdesperdicio);
        }

        // PUT: api/Procdesperdicio/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcdesperdicio(int id, Procdesperdicio procdesperdicio)
        {
            if (id != procdesperdicio.Item)
            {
                return BadRequest();
            }

            _context.Entry(procdesperdicio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcdesperdicioExists(id))
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

        // POST: api/Procdesperdicio
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Procdesperdicio>> PostProcdesperdicio(Procdesperdicio procdesperdicio)
        {
          if (_context.Procdesperdicios == null)
          {
              return Problem("Entity set 'plasticaribeContext.Procdesperdicios'  is null.");
          }
            _context.Procdesperdicios.Add(procdesperdicio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcdesperdicio", new { id = procdesperdicio.Item }, procdesperdicio);
        }

        // DELETE: api/Procdesperdicio/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcdesperdicio(int id)
        {
            if (_context.Procdesperdicios == null)
            {
                return NotFound();
            }
            var procdesperdicio = await _context.Procdesperdicios.FindAsync(id);
            if (procdesperdicio == null)
            {
                return NotFound();
            }

            _context.Procdesperdicios.Remove(procdesperdicio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcdesperdicioExists(int id)
        {
            return (_context.Procdesperdicios?.Any(e => e.Item == id)).GetValueOrDefault();
        }
    }
}
