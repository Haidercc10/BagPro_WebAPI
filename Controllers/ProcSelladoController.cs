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
    public class ProcSelladoController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public ProcSelladoController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/ProcExtrusion
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcSellado>>> GetProcExtrusions()
        {
          if (_context.ProcExtrusions == null)
          {
              return NotFound();
          }
            return await _context.ProcSellados.ToListAsync();
        }

        [HttpGet("OT/{ot}")]
        public ActionResult<ProcSellado> GetOT(string ot)
        {
            var procExtrusion = _context.ProcSellados.Where(prExt => prExt.Ot == ot).ToList();

            if (procExtrusion == null)
            {
                return NotFound();
            }

            return Ok(procExtrusion);
        }

        [HttpGet("FechaFinOT/{ot}")]
        public ActionResult<ProcSellado> GetFechaFinOTSellado(string ot)
        {
            /*** Consulta para obtener la ultima fecha en que se realizó el proceso de sellado en una OT */
            var nomStatus = "SELLADO";
            var procSellado = _context.ProcSellados.Where(prSella => prSella.Ot == ot &&
                                                          prSella.NomStatus == nomStatus)
                                                       .OrderByDescending(x => x.FechaEntrada)
                                                       .Select(ProSellado => new
                                                       {
                                                          ProSellado.Ot,
                                                          ProSellado.NomStatus,
                                                          ProSellado.FechaEntrada
                                                       })
                                                       .First();

            if (procSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(procSellado);
            }


        }
    }
}
