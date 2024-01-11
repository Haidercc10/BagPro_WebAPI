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

        [HttpGet("getOtProcesoDesperdicio/{ot}")]
        public ActionResult GetOtProcesoDesperdicio(string ot, string? proceso = "")
        {
            if (proceso == "CORTE") proceso = "CORTADORES";
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning disable CS8629 // Nullable value type may be null.
            var procDesperdicio = from d in _context.Set<Procdesperdicio>()
                                  where d.Ot == ot &&
                                  d.NomStatus.Contains("DESP_" + proceso)
                                  select new
                                  {
                                      OT = Convert.ToString(d.Ot),
                                      Rollo = Convert.ToInt32(d.Item),
                                      Maquina = Convert.ToInt32(d.Maquina),
                                      Material = Convert.ToString(d.Material),
                                      Operario = Convert.ToString(d.Operador) == Convert.ToString("0") ? Convert.ToString("NO APLICA") : Convert.ToString(d.Operador),
                                      NoConformidad = Convert.ToString("NO APLICA"),
                                      Peso = Convert.ToDecimal(d.Extnetokg),
                                      Impreso = Convert.ToString("NO APLICA"),
                                      Fecha = d.Fecha.Value,
                                      Hora = Convert.ToString(d.Hora),
                                      Proceso = Convert.ToString(d.NomStatus),
                                      Turno = Convert.ToString(d.TurnoD),
                                      Observacion = Convert.ToString(""),
                                  };

            var procExtrusion = from p in _context.Set<ProcExtrusion>()
                                  where p.Ot == ot &&
                                  p.NomStatus.Contains("DESP_" + proceso)
                                  select new
                                  {
                                      OT = Convert.ToString(p.Ot),
                                      Rollo = Convert.ToInt32(p.Item),
                                      Maquina = Convert.ToInt32(p.Maquina),
                                      Material = Convert.ToString(p.Material),
                                      Operario = Convert.ToString(p.Operador) == Convert.ToString("0") ? Convert.ToString("NO APLICA") : Convert.ToString(p.Operador),
                                      NoConformidad = Convert.ToString(p.TipoDesperdicio) == Convert.ToString("") ? Convert.ToString("NO APLICA") : Convert.ToString(p.TipoDesperdicio),
                                      Peso = Convert.ToDecimal(p.Extnetokg),
                                      Impreso = Convert.ToString(p.TipoDesperdicio) == Convert.ToString("IMPRESO") || Convert.ToString(p.TipoDesperdicio) == Convert.ToString("TIRILLA IMPRESO") ? Convert.ToString("SI") : Convert.ToString("NO"),
                                      Fecha = p.Fecha.Value,
                                      Hora = Convert.ToString(p.Hora),
                                      Proceso = Convert.ToString(p.NomStatus),
                                      Turno = Convert.ToString(p.Turno),
                                      Observacion = Convert.ToString(""),
                                  };

#pragma warning disable CS8604 // Possible null reference argument.
            if (procDesperdicio == null && procExtrusion == null) return NotFound();
            else return Ok(procDesperdicio.Concat(procExtrusion));
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
#pragma warning restore CS8629 // Nullable value type may be null.
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
