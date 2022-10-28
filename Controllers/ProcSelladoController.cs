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

        [HttpGet("RollosOT/{ot}")]
        public ActionResult Get(string ot)
        {
            var con = from sel in _context.Set<ProcSellado>()
                      from cli in _context.Set<Cliente>()
                      where sel.Ot == ot
                            && (cli.CodBagpro == sel.CodCliente || cli.NombreComercial == sel.Cliente)
                      select new
                      {
                          sel.Ot,
                          sel.Item,
                          cli.IdentNro,
                          cli.NombreComercial,
                          sel.Referencia,
                          sel.NomReferencia,
                          sel.Qty,
                          sel.Unidad,
                          sel.NomStatus,
                          sel.FechaEntrada
                      };
            return Ok(con);
        }

        [HttpGet("Fechas/{fechaini}/{fechafin}")]
        public ActionResult<ProcSellado> GetOT(DateTime fechaini, DateTime fechafin)
        {
            var con = from sel in _context.Set<ProcSellado>()
                      from cli in _context.Set<Cliente>()
                      where sel.FechaEntrada >= fechaini
                            && sel.FechaEntrada <= fechafin
                            && (cli.CodBagpro == sel.CodCliente || cli.NombreComercial == sel.Cliente)
                      select new
                      {
                          sel.Ot,
                          sel.Item,
                          cli.IdentNro,
                          cli.NombreComercial,
                          sel.Referencia,
                          sel.NomReferencia,
                          sel.Qty,
                          sel.Unidad,
                          sel.NomStatus,
                          sel.FechaEntrada
                      };
            return Ok(con);
        }

        [HttpGet("Rollos/{Rollo}")]
        public ActionResult Get(int Rollo)
        {
            var con = from sel in _context.Set<ProcSellado>()
                      from cli in _context.Set<Cliente>()
                      where sel.Item == Rollo
                            && (cli.CodBagpro == sel.CodCliente || cli.NombreComercial == sel.Cliente)
                      select new
                      {
                          sel.Ot,
                          sel.Item,
                          cli.IdentNro,
                          cli.NombreComercial,
                          sel.Referencia,
                          sel.NomReferencia,
                          sel.Qty,
                          sel.Unidad,
                          sel.NomStatus,
                          sel.FechaEntrada
                      };
            return Ok(con);
        }

        [HttpGet("FechasOT/{fechaini}/{fechafin}/{ot}")]
        public ActionResult<ProcSellado> GetOT(DateTime fechaini, DateTime fechafin, string ot)
        {
            var con = from sel in _context.Set<ProcSellado>()
                      from cli in _context.Set<Cliente>()
                      where sel.Ot == ot
                            && sel.FechaEntrada >= fechaini
                            && sel.FechaEntrada <= fechafin
                            && (cli.CodBagpro == sel.CodCliente || cli.NombreComercial == sel.Cliente)
                      select new
                      {
                          sel.Ot,
                          sel.Item,
                          cli.IdentNro,
                          cli.NombreComercial,
                          sel.Referencia,
                          sel.NomReferencia,
                          sel.Qty,
                          sel.Unidad,
                          sel.NomStatus,
                          sel.FechaEntrada
                      };
            return Ok(con);
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

        [HttpGet("OtConSellado/{ot}")]
        public ActionResult<ProcSellado> GetOTSellada(string ot)
        {
        /*** Consulta para obtener la ultima fecha en que se realizó el proceso de sellado en una OT */
        //var nomStatus = "SELLADO";
        //var nomStatusW = "Wiketiado";
        var procSellado = _context.ProcSellados.Where(prSella => prSella.Ot == ot)
                                                    .GroupBy(agr => new { agr.Ot })
                                                    .Select(ProSellado => new
                                                    {
                                                        ProSellado.Key.Ot,
                                                        SumaPeso = ProSellado.Sum(sma => sma.Peso)
                                                    });
                                                           

            if (procSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(procSellado);
            }
        }

        [HttpGet("OtConSelladoPesoUnidad/{ot}")]
        public ActionResult<ProcSellado> GetOTSelladaPesoUnidad(string ot)
        {
            /*** Consulta para obtener la ultima fecha en que se realizó el proceso de sellado en una OT */
            //var nomStatus = "SELLADO";
            //var nomStatusW = "Wiketiado";
            var procSellado = _context.ProcSellados.Where(prSella => prSella.Ot == ot)
                                                        .GroupBy(agr => new { agr.Ot })
                                                        .Select(ProSellado => new
                                                        {
                                                            ProSellado.Key.Ot,
                                                            SumaPeso = ProSellado.Sum(sma => sma.Peso),
                                                            SumaUnidades = ProSellado.Sum(sma => sma.Qty)
                                                        });


            if (procSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(procSellado);
            }
        }

        [HttpGet("ContarOtEnSellado/{ot}")]
        public ActionResult<ProcExtrusion> GetConteoOTSellado(string ot)
        {
            /** Consulta para obtener la suma realizada en KG en el proceso de empaque en una OT */
            
            var prSellado = _context.ProcSellados.Where(prExt => prExt.Ot == ot)
                                                       .GroupBy(agr => new { agr.Ot})
                                                       .Select(ProSella => new
                                                       {
                                                           ProSella.Key.Ot,                                                           
                                                           conteoFilas = ProSella.Count()
                                                       });          

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }

        [HttpGet("MostrarRollos/{Rollo}")]
        public ActionResult GetRollosOT(int Rollo)
        {
            /**var procesoSellado = _context.ProcSellados.Where(prExt => prExt.Ot == OT)                                                       
                                                     .OrderBy(prExt => prExt.Ot)
                                                     .Select(agr => new
                                                     {
                                                         agr.Item,
                                                         agr.CodCliente,
                                                         agr.Cliente,
                                                         agr.Referencia,
                                                         agr.NomReferencia,
                                                         agr.Qty,
                                                         agr.Peso,
                                                         agr.Unidad,
                                                         agr.EnvioZeus,
                                                         
                                                     })
                                                     .ToList();*/

            var procesoSellado = from sc in _context.Set<Cliente>()
                          from sc2 in _context.Set<ProcSellado>()
                          where sc.CodBagpro == sc2.Cliente
                          && sc2.Item == Rollo
                          select new
                          {
                              sc2.Item,
                              sc2.CodCliente,
                              sc.IdentNro,
                              sc2.Cliente,
                              sc2.Referencia,
                              sc2.NomReferencia,
                              sc2.Qty,
                              sc2.Peso,
                              sc2.Unidad,
                              sc2.EnvioZeus
                          };

            if (procesoSellado == null)
            {
                return NotFound();
            } else
            {
                return Ok(procesoSellado);
            }

            
        }

        /** Obtener Info por OT y NomStatus */

        /** Sellado */
        [HttpGet("ObtenerDatosOTxSellado/{OT}")]
        public ActionResult<ProcSellado> GetObtenerStatusSellado(string OT)
        {
            /** Consulta para obtener la suma realizada en KG en el proceso de empaque en una OT */
            var Estatus = "SELLADO";
            var prSellado = _context.ProcSellados.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Estatus)
                                                 .OrderByDescending(proExt => proExt.Item)
                                                 .Select(ProSella => new
                                                       {
                                                           ProSella.Item,
                                                           ProSella.CodCliente,
                                                           ProSella.Cliente,
                                                           ProSella.Referencia,
                                                           ProSella.NomReferencia,
                                                           ProSella.Cedula,
                                                           ProSella.Operario,
                                                           ProSella.Cedula2,
                                                           ProSella.Operario2,
                                                           ProSella.Cedula3,
                                                           ProSella.Operario3,
                                                           ProSella.Cedula4,
                                                           ProSella.Operario4,
                                                           ProSella.Maquina,
                                                           ProSella.Qty,
                                                           ProSella.Peso,
                                                           ProSella.FechaEntrada,
                                                           ProSella.Supervisor,                                                            
                                                           ProSella.FechaCambio,
                                                           ProSella.Unidad,
                                                           ProSella.Hora,
                                                           ProSella.EnvioZeus,
                                                           ProSella.Turnos,
                                                           ProSella.Pesot,
                                                           ProSella.PesoMillar,
                                                           ProSella.NomStatus
                                                 }).ToList();

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }


        /** Wiketiado */
        [HttpGet("ObtenerDatosOTxWiketiado/{OT}")]
        public ActionResult<ProcSellado> GetObtenerStatusWiketiado(string OT)
        {
            /** Consulta para obtener la suma realizada en KG en el proceso de empaque en una OT */
            var Estatus = "Wiketiado";
            var prSellado = _context.ProcSellados.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Estatus)
                                                 .OrderByDescending(proExt => proExt.Item)
                                                 .Select(ProSella => new
                                                 {
                                                     ProSella.Item,
                                                     ProSella.CodCliente,
                                                     ProSella.Cliente,
                                                     ProSella.Referencia,
                                                     ProSella.NomReferencia,
                                                     ProSella.Cedula,
                                                     ProSella.Operario,
                                                     ProSella.Cedula2,
                                                     ProSella.Operario2,
                                                     ProSella.Cedula3,
                                                     ProSella.Operario3,
                                                     ProSella.Cedula4,
                                                     ProSella.Operario4,
                                                     ProSella.Maquina,
                                                     ProSella.Qty,
                                                     ProSella.Peso,
                                                     ProSella.FechaEntrada,
                                                     ProSella.Supervisor,
                                                     ProSella.Estado,
                                                     ProSella.FechaCambio,
                                                     ProSella.Unidad,
                                                     ProSella.Hora,
                                                     ProSella.EnvioZeus,
                                                     ProSella.Turnos,
                                                     ProSella.Pesot,
                                                     ProSella.PesoMillar,
                                                     ProSella.NomStatus,
                                                 }).ToList();

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }

        /** Consultas para consolidación de Info para modal en Estados Procesos OT */
        [HttpGet("MostrarDatosConsolidados_ProcSellado/{OT}/{Proceso}")]
        public ActionResult<ProcSellado> GetObtenerInfoConsolidadaProcSellado(string OT, string Proceso)
        {
            /** Consulta para obtener la suma realizada en KG en el proceso de empaque en una OT */
            var prSellado = _context.ProcSellados.Where(prExt => prExt.Ot == OT
                                                       && prExt.NomStatus == Proceso)
                                                 .GroupBy(agr => new { agr.FechaEntrada, agr.Operario, agr.NomReferencia, agr.Ot, agr.NomStatus } )
                                                 .Select(ProSella => new
                                                 {
                                                     SumaCantidad = ProSella.Sum(ProSel => ProSel.Qty),
                                                     SumaPeso = ProSella.Sum(ProSel => ProSel.Peso),
                                                     ProSella.Key.Ot,
                                                     ProSella.Key.NomReferencia,
                                                     ProSella.Key.Operario,
                                                     ProSella.Key.FechaEntrada,
                                                     ProSella.Key.NomStatus,
                                                     Count = ProSella.Count(),
                                                 }).ToList();

            if (prSellado == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(prSellado);
            }
        }
  
    }
}
