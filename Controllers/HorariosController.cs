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
    public class HorariosController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public HorariosController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/Horarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Horario>>> GetHorarios()
        {
          if (_context.Horarios == null)
          {
              return NotFound();
          }
            return await _context.Horarios.ToListAsync();
        }

        // GET: api/Horarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Horario>> GetHorario(int id)
        {
          if (_context.Horarios == null)
          {
              return NotFound();
          }
            var horario = await _context.Horarios.FindAsync(id);

            if (horario == null)
            {
                return NotFound();
            }

            return horario;
        }

        [HttpGet("getHorarioProceso/{proceso}")]
        public ActionResult GetHorarioProceso(string proceso)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS0219 // Variable is assigned but its value is never used
            var horario = (from h in _context.Set<Horario>()
                           where h.Procesoplanta == proceso
                           select h).FirstOrDefault();
            
            string horaActual = DateTime.Now.ToString("HH:mm");
            string horaInicio = $"{horario.Hora}:{horario.Minuto}";
            string horaFin = $"{horario.Hora2}:{horario.Minuto2}";

            string turno = "";

            if (Convert.ToDateTime(horaActual) >= Convert.ToDateTime(horaInicio) && Convert.ToDateTime(horaActual) <= Convert.ToDateTime(horaFin))
            {
                if (proceso == "EXTRUSION") turno = "RD";
                else turno = "DIA";
            }
            else
            {
                if (proceso == "EXTRUSION") turno = "RN";
                else turno = "NOCHE";
            }

            return Ok(turno.Split(" "));
#pragma warning restore CS0219 // Variable is assigned but its value is never used
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // PUT: api/Horarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHorario(int id, Horario horario)
        {
            if (id != horario.Item)
            {
                return BadRequest();
            }

            _context.Entry(horario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HorarioExists(id))
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

        // POST: api/Horarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Horario>> PostHorario(Horario horario)
        {
          if (_context.Horarios == null)
          {
              return Problem("Entity set 'plasticaribeContext.Horarios'  is null.");
          }
            _context.Horarios.Add(horario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HorarioExists(horario.Item))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHorario", new { id = horario.Item }, horario);
        }

        // DELETE: api/Horarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHorario(int id)
        {
            if (_context.Horarios == null)
            {
                return NotFound();
            }
            var horario = await _context.Horarios.FindAsync(id);
            if (horario == null)
            {
                return NotFound();
            }

            _context.Horarios.Remove(horario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HorarioExists(int id)
        {
            return (_context.Horarios?.Any(e => e.Item == id)).GetValueOrDefault();
        }
    }
}
