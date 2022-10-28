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
    public class ProcImpresionRollosBoppController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public ProcImpresionRollosBoppController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/ProcImpresionRollosBopp
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcImpresionRollosBopp>>> GetProcImpresionRollosBopps()
        {
            return await _context.ProcImpresionRollosBopps.ToListAsync();
        }

        // GET: api/ProcImpresionRollosBopp/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcImpresionRollosBopp>> GetProcImpresionRollosBopp(int id)
        {
            var procImpresionRollosBopp = await _context.ProcImpresionRollosBopps.FindAsync(id);

            if (procImpresionRollosBopp == null)
            {
                return NotFound();
            }

            return procImpresionRollosBopp;
        }

        //Consulta por numero de orden de trabajo de Impresion
        [HttpGet("consultaOtImpresion/{ot}")]
        public ActionResult Get(string ot)
        {
            var con = _context.ProcImpresionRollosBopps.Where(imp => imp.OtImp == ot).Select(imp => new {
                imp.Ot
            });
            return Ok(con);
        }

        // PUT: api/ProcImpresionRollosBopp/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcImpresionRollosBopp(int id, ProcImpresionRollosBopp procImpresionRollosBopp)
        {
            if (id != procImpresionRollosBopp.Id)
            {
                return BadRequest();
            }

            _context.Entry(procImpresionRollosBopp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcImpresionRollosBoppExists(id))
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

        // POST: api/ProcImpresionRollosBopp
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProcImpresionRollosBopp>> PostProcImpresionRollosBopp(ProcImpresionRollosBopp procImpresionRollosBopp)
        {
            _context.ProcImpresionRollosBopps.Add(procImpresionRollosBopp);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcImpresionRollosBopp", new { id = procImpresionRollosBopp.Id }, procImpresionRollosBopp);
        }

        // DELETE: api/ProcImpresionRollosBopp/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProcImpresionRollosBopp(int id)
        {
            var procImpresionRollosBopp = await _context.ProcImpresionRollosBopps.FindAsync(id);
            if (procImpresionRollosBopp == null)
            {
                return NotFound();
            }

            _context.ProcImpresionRollosBopps.Remove(procImpresionRollosBopp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProcImpresionRollosBoppExists(int id)
        {
            return _context.ProcImpresionRollosBopps.Any(e => e.Id == id);
        }
    }
}
