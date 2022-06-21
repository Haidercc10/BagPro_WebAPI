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
    public class ClientesOtItemsController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public ClientesOtItemsController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/ClientesOtItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesOtItem>>> GetClientesOtItems()
        {
          if (_context.ClientesOtItems == null)
          {
              return NotFound();
          }
            return await _context.ClientesOtItems.ToListAsync();
        }

        // GET: api/ClientesOtItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesOtItem>> GetClientesOtItem(int id)
        {
          if (_context.ClientesOtItems == null)
          {
              return NotFound();
          }
            var clientesOtItem = await _context.ClientesOtItems.FindAsync(id);

            if (clientesOtItem == null)
            {
                return NotFound();
            }

            return clientesOtItem;
        }

        // PUT: api/ClientesOtItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientesOtItem(int id, ClientesOtItem clientesOtItem)
        {
            if (id != clientesOtItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientesOtItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesOtItemExists(id))
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

        // POST: api/ClientesOtItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientesOtItem>> PostClientesOtItem(ClientesOtItem clientesOtItem)
        {
          if (_context.ClientesOtItems == null)
          {
              return Problem("Entity set 'plasticaribeContext.ClientesOtItems'  is null.");
          }
            _context.ClientesOtItems.Add(clientesOtItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientesOtItem", new { id = clientesOtItem.Id }, clientesOtItem);
        }

        // DELETE: api/ClientesOtItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientesOtItem(int id)
        {
            if (_context.ClientesOtItems == null)
            {
                return NotFound();
            }
            var clientesOtItem = await _context.ClientesOtItems.FindAsync(id);
            if (clientesOtItem == null)
            {
                return NotFound();
            }

            _context.ClientesOtItems.Remove(clientesOtItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientesOtItemExists(int id)
        {
            return (_context.ClientesOtItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
