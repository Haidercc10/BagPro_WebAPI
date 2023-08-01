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
    public class OperariosProcesosController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public OperariosProcesosController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/OperariosProcesos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OperariosProceso>>> GetOperariosProcesos()
        {
            return await _context.OperariosProcesos.ToListAsync();
        }

        // GET: api/OperariosProcesos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OperariosProceso>> GetOperariosProceso(int id)
        {
            var operariosProceso = await _context.OperariosProcesos.FindAsync(id);

            if (operariosProceso == null)
            {
                return NotFound();
            }

            return operariosProceso;
        }

        /** Cargar nombre de Operarios */
        [HttpGet("NombreOperarios")]
        public ActionResult GetNombreOperariosProceso()
        {
            var Proceso = "EXTRUSION";
            var operariosProceso = _context.OperariosProcesos.Where(P => P.Planta == Proceso)
                                                              .Select(Ope => new
                                                              {
                                                                Ope.Nombre
                                                              })
                                                              .ToList();
                                                              

            if (operariosProceso == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(operariosProceso);
            }             
        }

        // PUT: api/OperariosProcesos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperariosProceso(int id, OperariosProceso operariosProceso)
        {
            if (id != operariosProceso.Id)
            {
                return BadRequest();
            }

            _context.Entry(operariosProceso).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperariosProcesoExists(id))
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

        // POST: api/OperariosProcesos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<OperariosProceso>> PostOperariosProceso(OperariosProceso operariosProceso)
        {
            _context.OperariosProcesos.Add(operariosProceso);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOperariosProceso", new { id = operariosProceso.Id }, operariosProceso);
        }

        // DELETE: api/OperariosProcesos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperariosProceso(int id)
        {
            var operariosProceso = await _context.OperariosProcesos.FindAsync(id);
            if (operariosProceso == null)
            {
                return NotFound();
            }

            _context.OperariosProcesos.Remove(operariosProceso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OperariosProcesoExists(int id)
        {
            return _context.OperariosProcesos.Any(e => e.Id == id);
        }
    }
}
