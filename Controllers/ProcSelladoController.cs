using BagproWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        // Consulta para traer las unidades, paquetes o kilos pesados en una OT.
        [HttpGet("OT/{ot}")]
        public ActionResult<ProcSellado> GetOT(string ot)
        {
            var con = from ps in _context.Set<ProcSellado>()
                      where ps.Ot == ot
                      group ps by new
                      {
                          ps.NomStatus,
                      } into ps
                      select new
                      {
                          Proceso = ps.Key.NomStatus,
                          TotalPeso = ps.Sum(x => x.Peso),
                          TotalUnd = ps.Sum(x => x.Qty)
                      };

            return Ok(con);
        }

        /*** Consulta para obtener la ultima fecha en que se realizó el proceso de sellado en una OT */
        [HttpGet("FechaFinOT/{ot}")]
        public ActionResult<ProcSellado> GetFechaFinOTSellado(string ot)
        {
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

        // Consulta la nomina de los operarios de Sellado, esta consulta será acumulada por Items
        [HttpGet("getNominaSelladoAcumuladaItem/{fechaInicio}/{fechaFin}")]
        public ActionResult GetNominaSelladoAcumuladaItem(DateTime fechaInicio, DateTime fechaFin)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.

            var con = from PS in _context.Set<ProcSellado>()
                      join CL in _context.Set<ClientesOtItem>() on PS.Referencia.Trim() equals CL.ClienteItems.ToString().Trim()
                      where PS.FechaEntrada >= fechaInicio
                            && PS.FechaEntrada <= fechaFin.AddDays(1)
                            && PS.Item >= PrimerRolloPesado(fechaInicio).FirstOrDefault()
                            && (RollosPesadosMadrugada(fechaFin.AddDays(1)).Any() ? PS.Item < RollosPesadosMadrugada(fechaFin.AddDays(1)).FirstOrDefault() : PS.Item <= UltimosRolloPesado(fechaFin).FirstOrDefault())
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
                          PS.NomStatus,
                          PS.Maquina
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
                            PS.Key.Maquina == "9" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                      (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :
                            PS.Key.Maquina == "50" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                      (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :
                            PS.Key.NomStatus == "SELLADO" ? Convert.ToDecimal(PS.Key.Dia) :
                            PS.Key.NomStatus == "Wiketiado" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : 0
                          ),
                          PrecioNoche = (
                            PS.Key.Maquina == "9" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                      (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :
                            PS.Key.Maquina == "50" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                      (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :
                            PS.Key.NomStatus == "SELLADO" ? Convert.ToDecimal(PS.Key.Noche) :
                            PS.Key.NomStatus == "Wiketiado" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : 0
                          ),
                          Total = (
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 4) * Convert.ToDecimal(PS.Key.Dia) :
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 4) * Convert.ToDecimal(PS.Key.Noche) :
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 3) * Convert.ToDecimal(PS.Key.Dia) :
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 3) * Convert.ToDecimal(PS.Key.Noche) :
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / 2) * Convert.ToDecimal(PS.Key.Dia) :
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / 2) * Convert.ToDecimal(PS.Key.Noche) :
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Turnos == "DIA" ? PS.Sum(x => x.PS.Qty) * Convert.ToDecimal(PS.Key.Dia) :
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Turnos == "NOCHE" ? PS.Sum(x => x.PS.Qty) * Convert.ToDecimal(PS.Key.Noche) :

                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? PS.Sum(x => x.PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                  (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? PS.Sum(x => x.PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                   (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :

                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                                                 (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? PS.Sum(x => x.PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                  (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? PS.Sum(x => x.PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                   (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :

                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? PS.Sum(x => x.PS.Qty) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? PS.Sum(x => x.PS.Qty) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :

                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula4 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula3 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Cedula2 != "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? PS.Sum(x => x.PS.Qty) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? PS.Sum(x => x.PS.Qty) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : 0
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
                if (item.Cedula2.Trim() != "0") result.Add($"'Cedula': '{item.Cedula2.Trim()}', 'Operario': '{item.Operario2.Trim()}', {data}");
                if (item.Cedula3.Trim() != "0") result.Add($"'Cedula': '{item.Cedula3.Trim()}', 'Operario': '{item.Operario3.Trim()}', {data}");
                if (item.Cedula4.Trim() != "0") result.Add($"'Cedula': '{item.Cedula4.Trim()}', 'Operario': '{item.Operario4.Trim()}', {data}");
            }

            return result.Count() > 0 ? Ok(result) : NotFound();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
        }

        //funcion que devolverá el primer rollo pesado
        private IQueryable<int> PrimerRolloPesado(DateTime fecha)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return from PS in _context.Set<ProcSellado>()
                   where PS.FechaEntrada == fecha && 
                         !PS.Hora.StartsWith("00") &&
                         !PS.Hora.StartsWith("01") &&
                         !PS.Hora.StartsWith("02") &&
                         !PS.Hora.StartsWith("03") &&
                         !PS.Hora.StartsWith("04") &&
                         !PS.Hora.StartsWith("05") &&
                         !PS.Hora.StartsWith("06")
                   orderby PS.Item ascending
                   select PS.Item;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // Funcion que devolverá todos los rollos pesados en el día que se le pase y los ordenará de manera descendente
        private IQueryable<int> UltimosRolloPesado(DateTime fecha)
        {
            return from PS2 in _context.Set<ProcSellado>()
                   where PS2.FechaEntrada == fecha
                   orderby PS2.Item descending
                   select PS2.Item;
        }

        // Funcion que devolverá todos los rollos que hayan sido pesados en la fecha que se le pase y que sean solamente de las horas 7, 8 y 9
        private IQueryable<int> RollosPesadosMadrugada(DateTime fecha)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            return from PS2 in _context.Set<ProcSellado>()
                   where (PS2.Hora.StartsWith("07") ||
                         PS2.Hora.StartsWith("08") ||
                         PS2.Hora.StartsWith("09") ||
                         !PS2.Hora.StartsWith("0")) &&
                         PS2.FechaEntrada == fecha
                   orderby PS2.Item ascending
                   select PS2.Item;
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        // Consulta la nomina de los operarios de Sellado, esta consulta será detallada por Items y Personas
        [HttpGet("getNominaSelladoDetalladaItemPersona/{fechaInicio}/{fechaFin}/{producto}/{persona}")]
        public ActionResult GetNominaSelladoDetalladaItemPersona(DateTime fechaInicio, DateTime fechaFin, string producto, string persona)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from PS in _context.Set<ProcSellado>()
                      join CL in _context.Set<ClientesOtItem>() on PS.Referencia.Trim() equals CL.ClienteItems.ToString().Trim()
                      where PS.FechaEntrada >= fechaInicio
                            && PS.Referencia == producto
                            && (PS.Cedula == persona || PS.Cedula2 == persona || PS.Cedula3 == persona || PS.Cedula4 == persona)
                            && PS.FechaEntrada <= fechaFin.AddDays(1)
                            && PS.Item >= PrimerRolloPesado(fechaInicio).FirstOrDefault()
                            && (RollosPesadosMadrugada(fechaFin.AddDays(1)).Any() ? PS.Item < RollosPesadosMadrugada(fechaFin.AddDays(1)).FirstOrDefault() : PS.Item <= UltimosRolloPesado(fechaFin).FirstOrDefault())
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
                            PS.Maquina == "9" && PS.Turnos == "DIA" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                        (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.Maquina == "50" && PS.Turnos == "DIA" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                        (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.Maquina == "9" && PS.Turnos == "NOCHE" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                        (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.Maquina == "50" && PS.Turnos == "NOCHE" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                         (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? Convert.ToDecimal(CL.Dia) :
                            PS.NomStatus == "Wiketiado" && PS.Turnos == "DIA" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? Convert.ToDecimal(CL.Noche) :
                            PS.NomStatus == "Wiketiado" && PS.Turnos == "NOCHE" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : 0
                          ),
                          Total = (
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula4 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 4) * Convert.ToDecimal(CL.Dia) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 4) * Convert.ToDecimal(CL.Noche) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula3 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 3) * Convert.ToDecimal(CL.Dia) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 3) * Convert.ToDecimal(CL.Noche) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula2 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 2) * Convert.ToDecimal(CL.Dia) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 2) * Convert.ToDecimal(CL.Noche) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? PS.Qty * Convert.ToDecimal(CL.Dia) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? PS.Qty * Convert.ToDecimal(CL.Noche) :

                            PS.EnvioZeus == "1" && PS.Cedula4 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Cedula4 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Cedula3 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Cedula3 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Cedula2 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Cedula2 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                             (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                          (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                   (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :

                            PS.EnvioZeus == "1" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                             (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                          (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                   (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :

                            PS.EnvioZeus == "0" && PS.Cedula4 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Cedula4 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Cedula3 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Cedula3 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Cedula2 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Cedula2 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :

                            PS.EnvioZeus == "0" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : 0
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

                if (item.Cedula.Trim() == persona) result.Add($"'Cedula': '{item.Cedula.Trim()}', 'Operario': '{item.Operario.Trim()}', {data}");
                if (item.Cedula2.Trim() == persona) result.Add($"'Cedula': '{item.Cedula2.Trim()}', 'Operario': '{item.Operario2.Trim()}', {data}");
                if (item.Cedula3.Trim() == persona) result.Add($"'Cedula': '{item.Cedula3.Trim()}', 'Operario': '{item.Operario3.Trim()}', {data}");
                if (item.Cedula4.Trim() == persona) result.Add($"'Cedula': '{item.Cedula4.Trim()}', 'Operario': '{item.Operario4.Trim()}', {data}");
            }

            return result.Count() > 0 ? Ok(result) : NotFound();
        }

        // Consulta la nomina de los operarios de Sellado, esta consulta será detallada por bulto
        [HttpGet("getNominaSelladoDetalladaxBulto/{fechaInicio}/{fechaFin}")]
        public ActionResult GetNominaSelladoDetalladaxBulto(DateTime fechaInicio, DateTime fechaFin)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.
            var con = from PS in _context.Set<ProcSellado>()
                      join CL in _context.Set<ClientesOtItem>() on PS.Referencia.Trim() equals CL.ClienteItems.ToString().Trim()
                      where PS.FechaEntrada >= fechaInicio
                            && PS.FechaEntrada <= fechaFin.AddDays(1)
                            && PS.Item >= PrimerRolloPesado(fechaInicio).FirstOrDefault()
                            && (RollosPesadosMadrugada(fechaFin.AddDays(1)).Any() ? PS.Item < RollosPesadosMadrugada(fechaFin.AddDays(1)).FirstOrDefault() : PS.Item <= UltimosRolloPesado(fechaFin).FirstOrDefault())
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
                            PS.Maquina == "9" && PS.Turnos == "DIA" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                        (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.Maquina == "50" && PS.Turnos == "DIA" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                        (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.Maquina == "9" && PS.Turnos == "NOCHE" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                        (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.Maquina == "50" && PS.Turnos == "NOCHE" ? ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                         (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? Convert.ToDecimal(CL.Dia) :
                            PS.NomStatus == "Wiketiado" && PS.Turnos == "DIA" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? Convert.ToDecimal(CL.Noche) :
                            PS.NomStatus == "Wiketiado" && PS.Turnos == "NOCHE" ? (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : 0
                          ),
                          Total = (
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula4 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 4) * Convert.ToDecimal(CL.Dia) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 4) * Convert.ToDecimal(CL.Noche) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula3 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 3) * Convert.ToDecimal(CL.Dia) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 3) * Convert.ToDecimal(CL.Noche) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula2 != "0" && PS.Turnos == "DIA" ? (PS.Qty / 2) * Convert.ToDecimal(CL.Dia) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" ? (PS.Qty / 2) * Convert.ToDecimal(CL.Noche) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? PS.Qty * Convert.ToDecimal(CL.Dia) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? PS.Qty * Convert.ToDecimal(CL.Noche) :

                            PS.EnvioZeus == "1" && PS.Cedula4 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Cedula4 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Cedula3 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Cedula3 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Cedula2 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Cedula2 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                                             (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                          (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                                   (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :

                            PS.EnvioZeus == "1" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 4) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 3) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                     (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 2) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                                             (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                          (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                                   (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :

                            PS.EnvioZeus == "0" && PS.Cedula4 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Cedula4 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Cedula3 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Cedula3 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Cedula2 != "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Cedula2 != "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :

                            PS.EnvioZeus == "0" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Cedula4 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 4) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Cedula3 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 3) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Cedula2 != "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / 2) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? PS.Qty * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : 0
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

                result.Add($"'Cedula': '{item.Cedula.Trim()}', 'Operario': '{item.Operario.Trim()}', {data}");
                if (item.Cedula2.Trim() != "0") result.Add($"'Cedula': '{item.Cedula2.Trim()}', 'Operario': '{item.Operario2.Trim()}', {data}");
                if (item.Cedula3.Trim() != "0") result.Add($"'Cedula': '{item.Cedula3.Trim()}', 'Operario': '{item.Operario3.Trim()}', {data}");
                if (item.Cedula4.Trim() != "0") result.Add($"'Cedula': '{item.Cedula4.Trim()}', 'Operario': '{item.Operario4.Trim()}', {data}");
            }

            return result.Count() > 0 ? Ok(result) : NotFound();
        }

        // Consulta que devolverá la información de una orden de trabajo en un proceso en especifico
        [HttpGet("getInformacionOrden_Proceso/{orden}/{proceso}")]
        public ActionResult GetInformacionOrden_Proceso(string orden, string proceso)
        {
#pragma warning disable CS8604 // Possible null reference argument
            List<string> procesos = new List<string>();
            foreach (var item in proceso.Split("|"))
            {
                procesos.Add(item);
            }
            var sellado = from sel in _context.Set<ProcSellado>()
                          where sel.Ot == orden &&
                                procesos.Contains(sel.NomStatus)
                          select sel;
#pragma warning restore CS8604 // Possible null reference argument.
            return sellado.Any() ? Ok(sellado) : BadRequest();
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

        [HttpPost("ajusteExistencia")]
        public async Task<ActionResult> AjusteExistencia([FromBody] List<int> rollos)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            int count = 0;
            foreach (var rollo in rollos)
            {
                var datos = (from pro in _context.Set<ProcSellado>()
                             join ot in _context.Set<ClientesOt>() on Convert.ToString(pro.Ot) equals Convert.ToString(ot.Item)
                             where pro.Item == rollo &&
                                   pro.EnvioZeus.Trim() == "0"
                             select new
                             {
                                 Orden = pro.Ot,
                                 Item = pro.Referencia,
                                 Presentacion = ot.PtPresentacionNom,
                                 Rollo = pro.Item,
                                 Cantidad = pro.Qty,
                                 Costo = ot.DatoscantKg
                             }).FirstOrDefault();

                ProcExtrusionController procExtrusionController = new ProcExtrusionController(_context);
                await procExtrusionController.EnviarAjuste(datos.Orden, datos.Item, datos.Presentacion, datos.Rollo, datos.Cantidad, Convert.ToDecimal(datos.Costo));
                await PutEnvioZeus(datos.Rollo);
                count++;
                if (count == rollos.Count) return Ok();
            }
            return Ok();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        public async Task<IActionResult> PutEnvioZeus(int rollo)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var procSellado = (from pro in _context.Set<ProcSellado>() where pro.Item == rollo select pro).FirstOrDefault();
            procSellado.EnvioZeus = "1";
            _context.Entry(procSellado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return NoContent();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }
    }
}