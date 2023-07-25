using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BagproWebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using Microsoft.OpenApi.Any;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace BagproWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
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

        /** Obtener Info por OT y NomStatus Sellado */
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

        // Consulta la nomina de los operarios de Sellado, esta consulta será acumulada por Items
        [HttpGet("getNominaSelladoAcumuladaItem/{fechaInicio}/{fechaFin}")]
        public ActionResult GetNominaSelladoAcumuladaItem(DateTime fechaInicio, DateTime fechaFin)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.

            var con = from PS in _context.Set<ProcSellado>()
                      join CL in _context.Set<ClientesOtItem>() on PS.Referencia.Trim() equals CL.ClienteItems.ToString().Trim()
                      where PS.FechaEntrada >= fechaInicio
                            && PS.FechaEntrada <= fechaFin
                            && PS.Item < (from PS2 in _context.Set<ProcSellado>()
                                          where (PS2.Hora.StartsWith("07") ||
                                                PS2.Hora.StartsWith("08") ||
                                                PS2.Hora.StartsWith("09") ||
                                                !PS2.Hora.StartsWith("0")) &&
                                                PS2.FechaEntrada == fechaFin
                                          orderby PS2.Item ascending
                                          select PS2.Item).FirstOrDefault()
                      group new { PS, CL } by new
                      {
                          PS.Cedula,
                          PS.Cedula2,
                          PS.Cedula3,
                          PS.Cedula4,
                          PS.Operario,
                          PS.Operario2,
                          PS.Operario3,
                          PS.Operario4,
                          PS.Ot,
                          PS.Referencia,
                          PS.NomReferencia,
                          PS.Unidad,
                          PS.Turnos,
                          CL.Dia,
                          CL.Noche,
                          PS.EnvioZeus,
                          PS.NomStatus
                      } into PS
                      select new
                      {
                          PS.Key.Cedula,
                          PS.Key.Cedula2,
                          PS.Key.Cedula3,
                          PS.Key.Cedula4,
                          PS.Key.Operario,
                          PS.Key.Operario2,
                          PS.Key.Operario3,
                          PS.Key.Operario4,
                          PS.Key.Ot,
                          PS.Key.Referencia,
                          PS.Key.NomReferencia,
                          Cantidad = (
                            PS.Key.Cedula4 != "0" ? PS.Sum(x => x.PS.Qty) / 4 : 
                            PS.Key.Cedula3 != "0" ? PS.Sum(x => x.PS.Qty) / 3 :
                            PS.Key.Cedula2 != "0" ? PS.Sum(x => x.PS.Qty) / 2 :
                            PS.Sum(x => x.PS.Qty)
                          ),
                          CantidadTotal = PS.Sum(x => x.PS.Qty),
                          PS.Key.Unidad,
                          PS.Key.Turnos,
                          PS.Key.NomStatus,
                          PrecioDia = (
                            PS.Key.NomStatus == "SELLADO" ? Convert.ToDecimal(PS.Key.Dia) : 
                            PS.Key.NomStatus == "Wiketiado" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : 0
                          ),
                          PrecioNoche = (
                            PS.Key.NomStatus == "SELLADO" ? Convert.ToDecimal(PS.Key.Noche) :
                            PS.Key.NomStatus == "Wiketiado" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : 0
                          ),
                          Total = (
                            PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 4) * Convert.ToDecimal(PS.Key.Dia) :
                            PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 4) * Convert.ToDecimal(PS.Key.Noche) :
                            PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 3) * Convert.ToDecimal(PS.Key.Dia) :
                            PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 3) * Convert.ToDecimal(PS.Key.Noche) :
                            PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 2) * Convert.ToDecimal(PS.Key.Dia) :
                            PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 2) * Convert.ToDecimal(PS.Key.Noche) :
                            PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Turnos == "DIA" ? PS.Sum(x => x.PS.Qty) * Convert.ToDecimal(PS.Key.Dia) :
                            PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Turnos == "NOCHE" ? PS.Sum(x => x.PS.Qty) * Convert.ToDecimal(PS.Key.Noche) :

                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Turnos == "DIA" ? PS.Sum(x => x.PS.Qty) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Turnos == "DIA" ? PS.Sum(x => x.PS.Qty) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :

                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Turnos == "NOCHE" ? PS.Sum(x => x.PS.Qty) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.NomStatus == "Wiketiado" && PS.Key.Turnos == "NOCHE" ? PS.Sum(x => x.PS.Qty) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : 0
                          ),
                          Registros = PS.Count(),
                          PesadoEntre = (
                            PS.Key.Cedula4 != "0" ? 4 :
                            PS.Key.Cedula3 != "0" ? 3 :
                            PS.Key.Cedula2 != "0" ? 2 : 1
                          )
                      };
            var result = new List<object>();
            foreach (var item in con)
            {
                string data = $"'Referencia': '{item.Referencia.Trim()}', " +
                    $"'Nombre_Referencia': '{item.NomReferencia.Replace("'", "`").Replace('"', '`')}'," +
                    $"'Ot': '{item.Ot}', " +
                    $"'Registros': '{item.Registros}', " +
                    $"'Pesado_Entre': '{Convert.ToDecimal(item.PesadoEntre)}', " +
                    $"'CantidadTotal': '{Convert.ToDecimal(item.CantidadTotal)}', " +
                    $"'Cantidad': '{Convert.ToDecimal(item.Cantidad)}', " +
                    $"'Presentacion': '{item.Unidad.Trim()}', " +
                    $"'Proceso': '{item.NomStatus.Trim()}', " +
                    $"'Turno': '{item.Turnos.Trim()}', " +
                    $"'PrecioDia': '{Convert.ToDecimal(item.PrecioDia)}', " +
                    $"'PrecioNoche': '{Convert.ToDecimal(item.PrecioNoche)}', " +
                    $"'PagoTotal': '{Convert.ToDecimal(item.Total)}'";

                result.Add($"'Cedula': '{item.Cedula.Trim()}', 'Operario': '{item.Operario.Trim()}', {data}");
                if (item.Cedula2 != "0") result.Add($"'Cedula': '{item.Cedula2}', 'Operario': '{item.Operario2}', {data}");
                if (item.Cedula3 != "0") result.Add($"'Cedula': '{item.Cedula3}', 'Operario': '{item.Operario3}', {data}");
                if (item.Cedula4 != "0") result.Add($"'Cedula': '{item.Cedula4}', 'Operario': '{item.Operario4}', {data}");
            }

            return result.Count() > 0 ? Ok(result) : NoContent();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
        }

        // Consulta la nomina de los operarios de Sellado, esta consulta será detallada por Items y Personas
        [HttpGet("getNominaSelladoDetalladaItemPersona/{fechaInicio}/{fechaFin}/{producto}/{persona}")]
        public ActionResult GetNominaSelladoDetalladaItemPersona(DateTime fechaInicio, DateTime fechaFin, string producto, string persona)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from PS in _context.Set<ProcSellado>()
                      join CL in _context.Set<ClientesOtItem>() on PS.Referencia.Trim() equals CL.ClienteItems.ToString().Trim()
                      where PS.FechaEntrada >= fechaInicio
                            && PS.FechaEntrada <= fechaFin
                            && PS.Referencia == producto
                            && (PS.Cedula == persona || PS.Cedula2 == persona || PS.Cedula3 == persona || PS.Cedula4 == persona)
                            && PS.Item < (from PS2 in _context.Set<ProcSellado>()
                                          where (PS2.Hora.StartsWith("07") ||
                                                PS2.Hora.StartsWith("08") ||
                                                PS2.Hora.StartsWith("09") ||
                                                !PS2.Hora.StartsWith("0")) &&
                                                PS2.FechaEntrada == fechaFin
                                          orderby PS2.Item ascending
                                          select PS2.Item).FirstOrDefault()
                      select new
                      {
                          PS.Cedula,
                          PS.Cedula2,
                          PS.Cedula3,
                          PS.Cedula4,
                          PS.Operario,
                          PS.Operario2,
                          PS.Operario3,
                          PS.Operario4,
                          PS.Ot,
                          PS.FechaEntrada,
                          PS.Hora,
                          PS.Referencia,
                          PS.NomReferencia,
                          PS.Item,
                          Cantidad = (
                            PS.Cedula4 != "0" ? PS.Qty / 4 :
                            PS.Cedula3 != "0" ? PS.Qty / 3 :
                            PS.Cedula2 != "0" ? PS.Qty / 2 :
                            PS.Qty
                          ),
                          CantidadTotal = PS.Qty,
                          PS.Unidad,
                          PS.Maquina,
                          PS.Peso,
                          PS.Turnos,
                          PS.NomStatus,
                          Precio = (
                            PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? Convert.ToDecimal(CL.Dia) :
                            PS.NomStatus == "Wiketiado" && PS.Turnos == "DIA" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? Convert.ToDecimal(CL.Noche) :
                            PS.NomStatus == "Wiketiado" && PS.Turnos == "NOCHE" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : 0
                          ),
                          Total = (
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula4 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 4) * Convert.ToDecimal(CL.Dia) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 4) * Convert.ToDecimal(CL.Noche) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula3 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 3) * Convert.ToDecimal(CL.Dia) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 3) * Convert.ToDecimal(CL.Noche) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula2 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 2) * Convert.ToDecimal(CL.Dia) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 2) * Convert.ToDecimal(CL.Noche) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? PS.Qty * Convert.ToDecimal(CL.Dia) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? PS.Qty * Convert.ToDecimal(CL.Noche) :

                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula4 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula4 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula3 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula3 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula2 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Turnos == "DIA" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Turnos == "DIA" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :

                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Turnos == "NOCHE" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Turnos == "NOCHE" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : 0
                          ),
                          PesadoEntre = (
                            PS.Cedula4 != "0" ? 4 :
                            PS.Cedula3 != "0" ? 3 :
                            PS.Cedula2 != "0" ? 2 : 1
                          )

                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            var result = new List<object>();
            foreach (var item in con)
            {
                string data = $"'Fecha': '{item.FechaEntrada} {item.Hora}'," +
                    $"'Ot': '{item.Ot}'," +
                    $"'Bulto': '{item.Item}'," +
                    $"'Referencia': '{item.Referencia.Trim()}'," +
                    $"'Nombre_Referencia': '{item.NomReferencia.Replace("'", "`").Replace('"', '`')}'," +
                    $"'Cantidad_Total': '{item.CantidadTotal}'," +
                    $"'Cantidad': '{item.Cantidad}'," +
                    $"'Presentacion': '{item.Unidad.Trim()}'," +
                    $"'Maquina': '{item.Maquina.Trim()}'," +
                    $"'Peso': '{item.Peso}'," +
                    $"'Turno': '{item.Turnos.Trim()}'," +
                    $"'Proceso': '{item.NomStatus.Trim()}'," +
                    $"'Precio': '{item.Precio}'," +
                    $"'Valor_Total': '{item.Total}'," +
                    $"'Pesado_Entre': '{item.PesadoEntre}' ";

                if (item.Cedula == persona) result.Add($"'Cedula': '{item.Cedula.Trim()}', 'Operario': '{item.Operario.Trim()}', {data}");
                if (item.Cedula2 == persona) result.Add($"'Cedula': '{item.Cedula2}', 'Operario': '{item.Operario2}', {data}");
                if (item.Cedula3 == persona) result.Add($"'Cedula': '{item.Cedula3}', 'Operario': '{item.Operario3}', {data}");
                if (item.Cedula4 == persona) result.Add($"'Cedula': '{item.Cedula4}', 'Operario': '{item.Operario4}', {data}");
            }

            return result.Count() > 0 ? Ok(result) : NoContent();
        }

        // Consulta la nomina de los operarios de Sellado, esta consulta será detallada por bulto
        [HttpGet("getNominaSelladoDetalladaxBulto/{fechaInicio}/{fechaFin}")]
        public ActionResult GetNominaSelladoDetalladaxBulto(DateTime fechaInicio, DateTime fechaFin)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from PS in _context.Set<ProcSellado>()
                      join CL in _context.Set<ClientesOtItem>() on PS.Referencia.Trim() equals CL.ClienteItems.ToString().Trim()
                      where PS.FechaEntrada >= fechaInicio
                            && PS.FechaEntrada <= fechaFin
                            && PS.Item < (from PS2 in _context.Set<ProcSellado>()
                                          where (PS2.Hora.StartsWith("07") ||
                                                PS2.Hora.StartsWith("08") ||
                                                PS2.Hora.StartsWith("09") ||
                                                !PS2.Hora.StartsWith("0")) &&
                                                PS2.FechaEntrada == fechaFin
                                          orderby PS2.Item ascending
                                          select PS2.Item).FirstOrDefault()
                      //orderby PS.Cedula, PS.Referencia, PS.FechaEntrada, PS.Item
                      select new
                      {
                          PS.Cedula,
                          PS.Cedula2,
                          PS.Cedula3,
                          PS.Cedula4,
                          PS.Operario,
                          PS.Operario2,
                          PS.Operario3,
                          PS.Operario4,
                          PS.Ot,
                          PS.FechaEntrada,
                          PS.Hora,
                          PS.Referencia,
                          PS.NomReferencia,
                          PS.Item,
                          Cantidad = (
                            PS.Cedula4 != "0" ? PS.Qty / 4 :
                            PS.Cedula3 != "0" ? PS.Qty / 3 :
                            PS.Cedula2 != "0" ? PS.Qty / 2 :
                            PS.Qty
                          ),
                          CantidadTotal = PS.Qty,
                          PS.Unidad,
                          PS.Maquina,
                          PS.Peso,
                          PS.Turnos,
                          PS.NomStatus,
                          Precio = (
                            PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? Convert.ToDecimal(CL.Dia) :
                            PS.NomStatus == "Wiketiado" && PS.Turnos == "DIA" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? Convert.ToDecimal(CL.Noche) :
                            PS.NomStatus == "Wiketiado" && PS.Turnos == "NOCHE" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : 0
                          ),
                          Total = (
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula4 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 4) * Convert.ToDecimal(CL.Dia) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 4) * Convert.ToDecimal(CL.Noche) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula3 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 3) * Convert.ToDecimal(CL.Dia) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 3) * Convert.ToDecimal(CL.Noche) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula2 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 2) * Convert.ToDecimal(CL.Dia) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 2) * Convert.ToDecimal(CL.Noche) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? PS.Qty * Convert.ToDecimal(CL.Dia) :
                            PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? PS.Qty * Convert.ToDecimal(CL.Noche) :

                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula4 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula4 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula3 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula3 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula2 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Turnos == "DIA" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Turnos == "DIA" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :

                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Turnos == "NOCHE" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.NomStatus == "Wiketiado" && PS.Turnos == "NOCHE" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : 0
                          ),
                          PesadoEntre = (
                            PS.Cedula4 != "0" ? 4 :
                            PS.Cedula3 != "0" ? 3 :
                            PS.Cedula2 != "0" ? 2 : 1
                          )

                      };
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
            var result = new List<object>();
            foreach (var item in con)
            {
                string data = $"'Fecha': '{item.FechaEntrada} {item.Hora}'," +
                    $"'Bulto': '{item.Item}'," +
                    $"'Ot': '{item.Ot}'," +
                    $"'Referencia': '{item.Referencia.Trim()}'," +
                    $"'Nombre_Referencia': '{item.NomReferencia.Replace("'", "`").Replace('"', '`') }'," +
                    $"'Cantidad_Total': '{item.CantidadTotal}'," +
                    $"'Cantidad': '{item.Cantidad}'," +
                    $"'Presentacion': '{item.Unidad.Trim()}'," +
                    $"'Maquina': '{item.Maquina.Trim()}'," +
                    $"'Peso': '{item.Peso}'," +
                    $"'Turno': '{item.Turnos.Trim()}'," +
                    $"'Proceso': '{item.NomStatus.Trim()}'," +
                    $"'Precio': '{item.Precio}'," +
                    $"'Valor_Total': '{item.Total}'," +
                    $"'Pesado_Entre': '{item.PesadoEntre}' ";

                result.Add($"'Cedula': '{item.Cedula.Trim()}', 'Operario': '{item.Operario.Trim()}', {data}");
                if (item.Cedula2 != "0") result.Add($"'Cedula': '{item.Cedula2}', 'Operario': '{item.Operario2}', {data}");
                if (item.Cedula3 != "0") result.Add($"'Cedula': '{item.Cedula3}', 'Operario': '{item.Operario3}', {data}");
                if (item.Cedula4 != "0") result.Add($"'Cedula': '{item.Cedula4}', 'Operario': '{item.Operario4}', {data}");
            }

            return result.Count() > 0 ? Ok(result) : NoContent();
        }

        [HttpGet("getPrueba/{fechaFin}")]
        public ActionResult getPrueba(DateTime fechaFin)
        {
            DateTime Fecha = fechaFin.AddDays(1);

            return Ok(Fecha);
        }

        /** Eliminar bultos de procsellado*/
        [HttpDelete("EliminarRollosSellado_Wiketiado/{id}")]
        public ActionResult<ProcSellado> DeleteRollos_Sellado(int id)
        {
            if (_context.ProcSellados == null)
            {
                return NotFound();
            }
            var procSellado = _context.Database.ExecuteSql($"DELETE FROM ProcSellado WHERE Item = {id}");

#pragma warning disable CS0472 // El resultado de la expresión siempre es el mismo ya que un valor de este tipo siempre es igual a "null"
            if (procSellado == null)
            {
                return NotFound();
            }
#pragma warning restore CS0472 // El resultado de la expresión siempre es el mismo ya que un valor de este tipo siempre es igual a "null"

            return Ok(procSellado);
        }
    }
}