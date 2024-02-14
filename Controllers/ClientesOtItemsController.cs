using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BagproWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace BagproWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
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

        //Consultar por Id de Cliente Item
        [HttpGet("OtItem/{ClienteItems}")]
        public  IActionResult GetIdClientesOtItem(int ClienteItems)
        {
            var idClientesItem = from cliOT in _context.Set<ClientesOtItem>()
                                 from ven in _context.Set<Vendedore>()
                                 where cliOT.ClienteItems == ClienteItems
                                 && cliOT.UsrModifica == ven.Id
                                 select new
                                 {
                                     cliOT.ClienteItems,
                                     cliOT.ClienteItemsNom,
                                     cliOT.Cliente,
                                     cliOT.ClienteNom,
                                     cliOT.PtPresentacionNom,
                                     cliOT.DatosValorKg,
                                     ven.NombreCompleto
                                 };

            if (idClientesItem == null)
            {
                return NotFound();

            } else             
            {
                return Ok(idClientesItem.Take(1)); 
            }
        }

        //Consultar através de un LIKE 
        [HttpGet("likeReferencia/{referencia}")]
        public IActionResult LikeReferencia(string referencia)
        {
            var nombreItem = (from c in _context.Set<ClientesOtItem>()
                              where c.ClienteItemsNom.Contains(referencia)
                              select new { Item = c.ClienteItems, Referencia = c.ClienteItemsNom, PrecioKg = c.DatosValorKg, }).Take(50);

            if (nombreItem == null) return NotFound();
            else return Ok(nombreItem);
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
