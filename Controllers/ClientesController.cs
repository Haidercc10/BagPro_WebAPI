using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BagproWebAPI.Models;
using Microsoft.AspNetCore.Authorization;

namespace BagproWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ClientesController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public ClientesController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
          if (_context.Clientes == null)
          {
              return NotFound();
          }
            return await _context.Clientes.ToListAsync();
        }

        //El campo IdentNro es la llave primaria de la tabla clientes.
        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(string id)
        {
          if (_context.Clientes == null)
          {
              return NotFound();
          }
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return cliente;
        }

        /** Obtener Nombre Clientes con OT de 2 años hasta la fecha */
        [HttpGet("UltimosClientes/{fecha}")]
        public ActionResult GetClienteConOT(DateTime fecha)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }

            var ClientesOT = _context.ClientesOts.Where(clot => clot.FechaCrea >= fecha).Select(Id => Id.Cliente.ToString()).ToList();

#pragma warning disable CS8604 // Posible argumento de referencia nulo
            var cliente = _context.Clientes.Where(P => ClientesOT.Contains(P.CodBagpro)).Select(c => new { c.NombreComercial }).ToList();
#pragma warning restore CS8604 // Posible argumento de referencia nulo


            if (cliente == null)
            {
                return NotFound();
            } else
            {
                return Ok(cliente);
            }

            
        }

        [HttpGet("getClientesNombre/{nombre}")]
        public ActionResult GetClientesNombre(string nombre)
        {
            var clientes = from c in _context.Clientes
                           where c.NombreComercial.Contains(nombre)
                           select c;
            return Ok(clientes);
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(string id, Cliente cliente)
        {
            if (id != cliente.IdentNro)
            {
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
          if (_context.Clientes == null)
          {
              return Problem("Entity set 'plasticaribeContext.Clientes'  is null.");
          }
            _context.Clientes.Add(cliente);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClienteExists(cliente.IdentNro))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCliente", new { id = cliente.IdentNro }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(string id)
        {
            if (_context.Clientes == null)
            {
                return NotFound();
            }
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClienteExists(string id)
        {
            return (_context.Clientes?.Any(e => e.IdentNro == id)).GetValueOrDefault();
        }
    }
}
