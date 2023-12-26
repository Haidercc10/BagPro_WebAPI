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
    public class WiketiandoController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public WiketiandoController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/Wiketiando
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Wiketiando>>> GetWiketiandos()
        {
          if (_context.Wiketiandos == null)
          {
              return NotFound();
          }
            return await _context.Wiketiandos.ToListAsync();
        }

        // GET: api/Wiketiando/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Wiketiando>> GetWiketiando(string id)
        {
          if (_context.Wiketiandos == null)
          {
              return NotFound();
          }
            var wiketiando = await _context.Wiketiandos.FindAsync(id);

            if (wiketiando == null)
            {
                return NotFound();
            }

            return wiketiando;
        }

        // PUT: api/Wiketiando/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWiketiando(string id, Wiketiando wiketiando)
        {
            if (id != wiketiando.Id)
            {
                return BadRequest();
            }

            _context.Entry(wiketiando).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WiketiandoExists(id))
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

        // POST: api/Wiketiando
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Wiketiando>> PostWiketiando(Wiketiando wiketiando)
        {
          if (_context.Wiketiandos == null)
          {
              return Problem("Entity set 'plasticaribeContext.Wiketiandos'  is null.");
          }
            _context.Wiketiandos.Add(wiketiando);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WiketiandoExists(wiketiando.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWiketiando", new { id = wiketiando.Id }, wiketiando);
        }

        // DELETE: api/Wiketiando/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWiketiando(string id)
        {
            if (_context.Wiketiandos == null)
            {
                return NotFound();
            }
            var wiketiando = await _context.Wiketiandos.FindAsync(id);
            if (wiketiando == null)
            {
                return NotFound();
            }

            _context.Wiketiandos.Remove(wiketiando);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WiketiandoExists(string id)
        {
            return (_context.Wiketiandos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
