using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using BagproWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BagproWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesOtController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public ClientesOtController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/ClientesOt
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesOt>>> GetClientesOts()
        {
          if (_context.ClientesOts == null)
          {
              return NotFound();
          }
            return await _context.ClientesOts.ToListAsync();
        }

        // GET: api/ClientesOt/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesOt>> GetClientesOt(int id)
        {
          if (_context.ClientesOts == null)
          {
              return NotFound();
          }
            var clientesOt = await _context.ClientesOts.FindAsync(id);

            if (clientesOt == null)
            {
                return NotFound();
            }

            return clientesOt;
        }



        [HttpGet("CostosOT/{Item}")]
        public IActionResult GetCostostOt(int Item)
        {
            if (_context.ClientesOts == null)
            {
                return NotFound();
            }
            var OT =  _context.ClientesOts.Where(OrdTrab => OrdTrab.Item == Item)
                                          .Select(COT => new { 
                                          COT.ClienteNom,
                                          COT.ClienteItems,
                                          COT.ClienteItemsNom,
                                          COT.PtPresentacionNom,
                                          COT.DatoscantKg,
                                          COT.DatosmargenKg,
                                          COT.DatosotKg,
                                          COT.DatoscantBolsa,
                                          COT.DatosvalorBolsa,
                                          COT.DatosValorKg, 
                                          COT.DatosvalorOt,
                                          COT.FechaCrea
                                          }).ToList();
            if (OT == null)
            {
                return BadRequest();
            } 
            else
            {
                return Ok(OT);
            }
        }

        // GET: api/ClientesOt/5

        [HttpGet("OT/{item}")]
        public ActionResult<ClientesOt> GetOt(int item)
        {
            var clientesOt = _context.ClientesOts.Where(cOt => cOt.Item == item).ToList();  

            if (clientesOt == null)
            {
                return NotFound();
            }

            return Ok(clientesOt);
        }

        // PUT: api/ClientesOt/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientesOt(int id, ClientesOt clientesOt)
        {
            if (id != clientesOt.Item)
            {
                return BadRequest();
            }

            _context.Entry(clientesOt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesOtExists(id))
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

        /** Actualizar Estado de Ordenes */
        [HttpPut("CambioEstadoOT/{Item}")]
        public IActionResult PutEstadoClientesOt(int Item, ClientesOt clientesOt, string Estado)
        {
            if (Item != clientesOt.Item)
            {
                return BadRequest();
            }

            try
            {
                var Actualizado = _context.ClientesOts.First<ClientesOt>();
                Actualizado.Estado = Estado;

                _context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesOtExists(Item))
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


        // POST: api/ClientesOt
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientesOt>> PostClientesOt(ClientesOt clientesOt)
        {
          if (_context.ClientesOts == null)
          {
              return Problem("Entity set 'plasticaribeContext.ClientesOts'  is null.");
          }
            _context.ClientesOts.Add(clientesOt);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientesOtExists(clientesOt.Item))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClientesOt", new { id = clientesOt.Item }, clientesOt);
        }

        // DELETE: api/ClientesOt/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientesOt(int id)
        {
            if (_context.ClientesOts == null)
            {
                return NotFound();
            }
            var clientesOt = await _context.ClientesOts.FindAsync(id);
            if (clientesOt == null)
            {
                return NotFound();
            }

            _context.ClientesOts.Remove(clientesOt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientesOtExists(int id)
        {
            return (_context.ClientesOts?.Any(e => e.Item == id)).GetValueOrDefault();
        }
    }
}
