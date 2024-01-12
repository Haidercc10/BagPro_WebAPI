using BagproWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ServiceReference1;
using System.Data;
using System.ServiceModel;

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
                            //&& (PS.NomStatus == "SELLADO" ? PS.EnvioZeus == "1" : true)
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
                          PS.DivBulto,
                          PS.Ot,
                          PS.Referencia,
                          PS.NomReferencia,
                          PS.Unidad,
                          PS.Turnos,
                          CL.Dia,
                          CL.Noche,
                          PS.EnvioZeus,
                          PS.NomStatus,
                          PS.Maquina,
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
                          Cantidad = (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto),
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
                            //PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * Convert.ToDecimal(PS.Key.Dia) :
                            //PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * Convert.ToDecimal(PS.Key.Noche) :
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.NomStatus == "SELLADO" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * Convert.ToDecimal(PS.Key.Dia) :
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.NomStatus == "SELLADO" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * Convert.ToDecimal(PS.Key.Noche) :

                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                  (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                   (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :

                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                  (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                   (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :

                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :

                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : 0
                          ),
                          Registros = PS.Count(),
                          PesadoEntre = PS.Key.DivBulto,
                          PS.Key.EnvioZeus,
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
                    $"'EnvioZeus': '{item.EnvioZeus.Trim()}', " +
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

        // Consulta la nomina de los operarios de Sellado, esta consulta será acumulada por Items
        [HttpGet("getNominaSelladoDespachoAcumuladaItem/{fechaInicio}/{fechaFin}")]
        public ActionResult GetNominaSelladoDespachoAcumuladaItem(DateTime fechaInicio, DateTime fechaFin)
        {
#pragma warning disable CS8602 // Desreferencia de una referencia posiblemente NULL.

            var con = from PS in _context.Set<ProcSellado>()
                      join CL in _context.Set<ClientesOtItem>() on PS.Referencia.Trim() equals CL.ClienteItems.ToString().Trim()
                      where PS.FechaEntrada >= fechaInicio
                            && PS.FechaEntrada <= fechaFin.AddDays(1)
                            && PS.Item >= PrimerRolloPesado(fechaInicio).FirstOrDefault()
                            && (PS.NomStatus == "SELLADO" ? PS.EnvioZeus == "1" : true)
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
                          PS.DivBulto,
                          PS.Ot,
                          PS.Referencia,
                          PS.NomReferencia,
                          PS.Unidad,
                          PS.Turnos,
                          CL.Dia,
                          CL.Noche,
                          PS.EnvioZeus,
                          PS.NomStatus,
                          PS.Maquina,
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
                          Cantidad = (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto),
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
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Turnos == "DIA" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * Convert.ToDecimal(PS.Key.Dia) :
                            PS.Key.Maquina != "50" && PS.Key.Maquina != "9" && PS.Key.EnvioZeus == "1" && PS.Key.NomStatus == "SELLADO" && PS.Key.Turnos == "NOCHE" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * Convert.ToDecimal(PS.Key.Noche) :
                            
                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                  (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() != null ?
                                                                                                                                   (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() : Convert.ToDecimal(PS.Key.Dia)) :

                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                  (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :
                            PS.Key.EnvioZeus == "1" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() != null ?
                                                                                                                                   (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : Convert.ToDecimal(PS.Key.Noche)) :

                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "DIA" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Dia).First() :

                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "9" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() :
                            PS.Key.EnvioZeus == "0" && PS.Key.Turnos == "NOCHE" && PS.Key.Maquina == "50" ? (PS.Sum(x => x.PS.Qty) / PS.Key.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Key.Referencia select wik.Noche).First() : 0
                          ),
                          Registros = PS.Count(),
                          PesadoEntre = PS.Key.DivBulto,
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
                   where PS.FechaEntrada >= fecha && 
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
                   where PS2.FechaEntrada <= fecha
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
                            && PS.FechaEntrada <= fechaFin.AddDays(1)
                            && PS.Referencia == producto
                            && (PS.Cedula == persona || PS.Cedula2 == persona || PS.Cedula3 == persona || PS.Cedula4 == persona)
                            && PS.Item >= PrimerRolloPesado(fechaInicio).FirstOrDefault()
                            //&& (PS.NomStatus == "SELLADO" ? PS.EnvioZeus == "1" : true)
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
                          Cantidad = (PS.Qty / PS.DivBulto),
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
                            //PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? (PS.Qty / PS.DivBulto) * Convert.ToDecimal(CL.Dia) :
                            //PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? (PS.Qty / PS.DivBulto) * Convert.ToDecimal(CL.Noche) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? (PS.Qty / PS.DivBulto) * Convert.ToDecimal(CL.Dia) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? (PS.Qty / PS.DivBulto) * Convert.ToDecimal(CL.Noche) :

                            PS.EnvioZeus == "1" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / PS.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                        (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / PS.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                         (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :

                            PS.EnvioZeus == "1" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / PS.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                          (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / PS.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                           (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :

                            PS.EnvioZeus == "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / PS.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / PS.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :

                            PS.EnvioZeus == "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / PS.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / PS.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : 0
                          ),
                          PesadoEntre = PS.DivBulto,
                          PS.EnvioZeus,
                      };
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
                    $"'EnvioZeus': '{item.EnvioZeus.Trim()}', " +
                    $"'Proceso': '{item.NomStatus.Trim()}', " +
                    $"'Precio': '{item.Precio}'," +
                    $"'Valor_Total': '{item.Total}'," +
                    $"'Pesado_Entre': '{item.PesadoEntre}' ";

                if (item.Cedula.Trim() == persona) result.Add($"'Cedula': '{item.Cedula.Trim()}', 'Operario': '{item.Operario.Trim()}', {data}");
                if (item.Cedula2.Trim() == persona) result.Add($"'Cedula': '{item.Cedula2.Trim()}', 'Operario': '{item.Operario2.Trim()}', {data}");
                if (item.Cedula3.Trim() == persona) result.Add($"'Cedula': '{item.Cedula3.Trim()}', 'Operario': '{item.Operario3.Trim()}', {data}");
                if (item.Cedula4.Trim() == persona) result.Add($"'Cedula': '{item.Cedula4.Trim()}', 'Operario': '{item.Operario4.Trim()}', {data}");
            }
            return result.Count() > 0 ? Ok(result) : NotFound();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
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
                            //&& (PS.NomStatus == "SELLADO" ? PS.EnvioZeus == "1" : true)
                            && (RollosPesadosMadrugada(fechaFin.AddDays(1)).Any() ? PS.Item < RollosPesadosMadrugada(fechaFin.AddDays(1)).FirstOrDefault() : PS.Item <= UltimosRolloPesado(fechaFin).FirstOrDefault())
                      orderby PS.Cedula, PS.Referencia, PS.FechaEntrada, PS.Item
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
                          Cantidad = (PS.Qty / PS.DivBulto),
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
                            //PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? (PS.Qty / PS.DivBulto) * Convert.ToDecimal(CL.Dia) :
                            //PS.Maquina != "50" && PS.Maquina != "9" && PS.EnvioZeus == "1" && PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? (PS.Qty / PS.DivBulto) * Convert.ToDecimal(CL.Noche) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.NomStatus == "SELLADO" && PS.Turnos == "DIA" ? (PS.Qty / PS.DivBulto) * Convert.ToDecimal(CL.Dia) :
                            PS.Maquina != "50" && PS.Maquina != "9" && PS.NomStatus == "SELLADO" && PS.Turnos == "NOCHE" ? (PS.Qty / PS.DivBulto) * Convert.ToDecimal(CL.Noche) :

                            PS.EnvioZeus == "1" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / PS.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                        (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / PS.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() != null ?
                                                                                                                         (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() : Convert.ToDecimal(CL.Dia)) :

                            PS.EnvioZeus == "1" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / PS.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                          (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :
                            PS.EnvioZeus == "1" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / PS.DivBulto) * ((from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() != null ?
                                                                                                                           (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : Convert.ToDecimal(CL.Noche)) :

                            PS.EnvioZeus == "0" && PS.Turnos == "DIA" && PS.Maquina == "9" ? (PS.Qty / PS.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Dia).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "DIA" && PS.Maquina == "50" ? (PS.Qty / PS.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Dia).First() :

                            PS.EnvioZeus == "0" && PS.Turnos == "NOCHE" && PS.Maquina == "9" ? (PS.Qty / PS.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 9 && wik.Codigo == PS.Referencia select wik.Noche).First() :
                            PS.EnvioZeus == "0" && PS.Turnos == "NOCHE" && PS.Maquina == "50" ? (PS.Qty / PS.DivBulto) * (from wik in _context.Set<Wiketiando>() where wik.Mq == 50 && wik.Codigo == PS.Referencia select wik.Noche).First() : 0
                          ),
                          PesadoEntre = PS.DivBulto,
                          PS.EnvioZeus,
                      };
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
                    $"'EnvioZeus': '{item.EnvioZeus.Trim()}', " +
                    $"'Proceso': '{item.NomStatus.Trim()}', " +
                    $"'Precio': '{item.Precio}'," +
                    $"'Valor_Total': '{item.Total}'," +
                    $"'Pesado_Entre': '{item.PesadoEntre}' ";

                result.Add($"'Cedula': '{item.Cedula.Trim()}', 'Operario': '{item.Operario.Trim()}', {data}");
                if (item.Cedula2.Trim() != "0") result.Add($"'Cedula': '{item.Cedula2.Trim()}', 'Operario': '{item.Operario2.Trim()}', {data}");
                if (item.Cedula3.Trim() != "0") result.Add($"'Cedula': '{item.Cedula3.Trim()}', 'Operario': '{item.Operario3.Trim()}', {data}");
                if (item.Cedula4.Trim() != "0") result.Add($"'Cedula': '{item.Cedula4.Trim()}', 'Operario': '{item.Operario4.Trim()}', {data}");
            }

            return result.Count() > 0 ? Ok(result) : NotFound();
#pragma warning restore CS8602 // Desreferencia de una referencia posiblemente NULL.
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

        //
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
                if (datos != null)
                {
                    await EnviarAjuste(datos.Orden, datos.Item, datos.Presentacion, datos.Rollo, datos.Cantidad, Convert.ToDecimal(datos.Costo));
                    //CreatedAtAction("EnviarAjuste", new { ordenTrabajo = datos.Orden, articulo = datos.Item, presentacion = datos.Presentacion, rollo = datos.Rollo, cantidad = datos.Cantidad, costo = Convert.ToDecimal(datos.Costo) });
                    //await PutEnvioZeus(datos.Rollo);
                    CreatedAtAction("PutEnvioZeus", new { rollo = datos.Rollo }, datos.Rollo);
                    count++;
                    if (count == rollos.Count) return Ok();
                }
                else continue;
            }
            return Ok();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpGet("EnviarAjuste/{rollo}")]
        public async Task<ActionResult> EnviarAjuste(int rollo)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
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
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (datos == null) return Ok();

            string today = DateTime.Today.ToString("yyyy-MM-dd");
            SoapRequestAction request = new SoapRequestAction();
            request.User = "wsZeusInvProd";
            request.Password = "wsZeusInvProd";
            request.Body = $"<Ajuste>" +
                                $"<Op>I</Op>" +
                                $"<Cabecera>" +
                                    $"<Detalle>{datos.Orden}</Detalle>" +
                                    "<Concepto>001</Concepto>" +
                                    "<Consecutivo>0</Consecutivo>" +
                                    $"<Fecha>{today}</Fecha>" +
                                    "<Estado></Estado>" +
                                    "<Solicitante>7200000</Solicitante>" +
                                    "<Aprueba></Aprueba>" +
                                    "<Fuente>MA</Fuente>" +
                                    "<Serie>00</Serie>" +
                                    "<Usuario>zeussystem</Usuario>" +
                                    "<Documento></Documento>" +
                                    "<Documentorevertido></Documentorevertido>" +
                                    "<Bodega>003</Bodega>" +
                                    "<Grupo></Grupo>" +
                                    "<Origen>I</Origen>" +
                                    "<ConsecutivoRecosteo>0</ConsecutivoRecosteo>" +
                                    "<TipoDocumentoExterno></TipoDocumentoExterno>" +
                                    "<ConsecutivoExterno></ConsecutivoExterno>" +
                                    "<EsAjustePorDistribucion></EsAjustePorDistribucion>" +
                                    "<ItemsBodegaVirtual></ItemsBodegaVirtual>" +
                                    "<Clasificaciones></Clasificaciones>" +
                                    "<Propiedad1></Propiedad1>" +
                                    "<Propiedad2></Propiedad2>" +
                                    "<Propiedad3></Propiedad3>" +
                                    "<Propiedad4></Propiedad4>" +
                                    "<Propiedad5></Propiedad5>" +
                                    "<EsInicioNIIF></EsInicioNIIF>" +
                                    "<UtilizarZmlSpId></UtilizarZmlSpId>" +
                                    "<DatoExterno1></DatoExterno1>" +
                                    "<DatoExterno2></DatoExterno2>" +
                                    "<DatoExterno3></DatoExterno3>" +
                                    "<Moneda></Moneda>" +
                                    "<TasaCambio>1</TasaCambio>" +
                                    "<BU>Local</BU>" +
                                "</Cabecera>" +
                                "<Productos>" +
                                    $"<CodigoArticulo>{datos.Item}</CodigoArticulo>" +
                                    $"<Presentacion>{datos.Presentacion}</Presentacion>" +
                                    "<CodigoLote>0</CodigoLote>" +
                                    "<CodigoBodega>003</CodigoBodega>" +
                                    "<CodigoUbicacion></CodigoUbicacion>" +
                                    "<CodigoClasificacion>0</CodigoClasificacion>" +
                                    "<CodigoReferencia></CodigoReferencia>" +
                                    "<Serial>0</Serial>" +
                                    $"<Detalle>{rollo}</Detalle>" +
                                    $"<Cantidad>{datos.Cantidad}</Cantidad>" +
                                    $"<PrecioUnidad>{Convert.ToString(datos.Costo)}</PrecioUnidad>" +
                                    $"<PrecioUnidad2>{Convert.ToString(datos.Costo)}</PrecioUnidad2>" +
                                    "<Concepto_Codigo></Concepto_Codigo>" +
                                    "<TemporalItems_ValorAjuste></TemporalItems_ValorAjuste>" +
                                    "<Servicios>" +
                                    "<CodigoServicios>001</CodigoServicios>" +
                                    "<Referencia></Referencia>" +
                                    "<Detalle></Detalle>" +
                                    "<AuxiliarAbierto></AuxiliarAbierto>" +
                                    "<CentroCosto>0202</CentroCosto>" +
                                    "<Tercero>800188732</Tercero>" +
                                    "<Proveedor></Proveedor>" +
                                    "<TipoDocumentoCartera></TipoDocumentoCartera>" +
                                    "<DocumentoCartera></DocumentoCartera>" +
                                    "<Vencimiento></Vencimiento>" +
                                    "<Cliente></Cliente>" +
                                    "<Vendedor></Vendedor>" +
                                    "<ItemsContable></ItemsContable>" +
                                    "<Propiedad1></Propiedad1>" +
                                    "<Propiedad2></Propiedad2>" +
                                    "<Propiedad3></Propiedad3>" +
                                    "<Propiedad4></Propiedad4>" +
                                    "<Propiedad5></Propiedad5>" +
                                    "<CuentaMovimiento></CuentaMovimiento>" +
                                    "<Moneda></Moneda>" +
                                    "<Moneda></Moneda>" +
                                    "</Servicios>" +
                                "</Productos>" +
                            "</Ajuste>";
            request.DynamicProperty = "4";
            request.Action = "Inventario";
            request.TypeSQL = "true";

            var binding = new BasicHttpBinding()
            {
                Name = "BasicHttpBinding_IFakeService",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };

            var endpoint = new EndpointAddress("http://192.168.0.85/wsGenericoZeus/ServiceWS.asmx");
            WebservicesGenericoZeusSoapClient client = new WebservicesGenericoZeusSoapClient(binding, endpoint);
            SoapResponse response = await client.ExecuteActionSOAPAsync(request);
            await PutEnvioZeus(datos.Rollo);
            return Convert.ToString(response.Status) == "SUCCESS" ? Ok(response) : BadRequest(response);
        }

        [HttpGet("EnviarAjuste")]
        public async Task<ActionResult> EnviarAjuste(string ordenTrabajo, string articulo, string presentacion, int rollo, decimal cantidad, decimal costo)
        {
            string today = DateTime.Today.ToString("yyyy-MM-dd");
            SoapRequestAction request = new SoapRequestAction();
            request.User = "wsZeusInvProd";
            request.Password = "wsZeusInvProd";
            request.Body = $"<Ajuste>" +
                                $"<Op>I</Op>" +
                                $"<Cabecera>" +
                                    $"<Detalle>{ordenTrabajo}</Detalle>" +
                                    "<Concepto>001</Concepto>" +
                                    "<Consecutivo>0</Consecutivo>" +
                                    $"<Fecha>{today}</Fecha>" +
                                    "<Estado></Estado>" +
                                    "<Solicitante>7200000</Solicitante>" +
                                    "<Aprueba></Aprueba>" +
                                    "<Fuente>MA</Fuente>" +
                                    "<Serie>00</Serie>" +
                                    "<Usuario>zeussystem</Usuario>" +
                                    "<Documento></Documento>" +
                                    "<Documentorevertido></Documentorevertido>" +
                                    "<Bodega>003</Bodega>" +
                                    "<Grupo></Grupo>" +
                                    "<Origen>I</Origen>" +
                                    "<ConsecutivoRecosteo>0</ConsecutivoRecosteo>" +
                                    "<TipoDocumentoExterno></TipoDocumentoExterno>" +
                                    "<ConsecutivoExterno></ConsecutivoExterno>" +
                                    "<EsAjustePorDistribucion></EsAjustePorDistribucion>" +
                                    "<ItemsBodegaVirtual></ItemsBodegaVirtual>" +
                                    "<Clasificaciones></Clasificaciones>" +
                                    "<Propiedad1></Propiedad1>" +
                                    "<Propiedad2></Propiedad2>" +
                                    "<Propiedad3></Propiedad3>" +
                                    "<Propiedad4></Propiedad4>" +
                                    "<Propiedad5></Propiedad5>" +
                                    "<EsInicioNIIF></EsInicioNIIF>" +
                                    "<UtilizarZmlSpId></UtilizarZmlSpId>" +
                                    "<DatoExterno1></DatoExterno1>" +
                                    "<DatoExterno2></DatoExterno2>" +
                                    "<DatoExterno3></DatoExterno3>" +
                                    "<Moneda></Moneda>" +
                                    "<TasaCambio>1</TasaCambio>" +
                                    "<BU>Local</BU>" +
                                "</Cabecera>" +
                                "<Productos>" +
                                    $"<CodigoArticulo>{articulo}</CodigoArticulo>" +
                                    $"<Presentacion>{presentacion}</Presentacion>" +
                                    "<CodigoLote>0</CodigoLote>" +
                                    "<CodigoBodega>003</CodigoBodega>" +
                                    "<CodigoUbicacion></CodigoUbicacion>" +
                                    "<CodigoClasificacion>0</CodigoClasificacion>" +
                                    "<CodigoReferencia></CodigoReferencia>" +
                                    "<Serial>0</Serial>" +
                                    $"<Detalle>{rollo}</Detalle>" +
                                    $"<Cantidad>{cantidad}</Cantidad>" +
                                    $"<PrecioUnidad>{costo}</PrecioUnidad>" +
                                    $"<PrecioUnidad2>{costo}</PrecioUnidad2>" +
                                    "<Concepto_Codigo></Concepto_Codigo>" +
                                    "<TemporalItems_ValorAjuste></TemporalItems_ValorAjuste>" +
                                    "<Servicios>" +
                                    "<CodigoServicios>001</CodigoServicios>" +
                                    "<Referencia></Referencia>" +
                                    "<Detalle></Detalle>" +
                                    "<AuxiliarAbierto></AuxiliarAbierto>" +
                                    "<CentroCosto>0202</CentroCosto>" +
                                    "<Tercero>800188732</Tercero>" +
                                    "<Proveedor></Proveedor>" +
                                    "<TipoDocumentoCartera></TipoDocumentoCartera>" +
                                    "<DocumentoCartera></DocumentoCartera>" +
                                    "<Vencimiento></Vencimiento>" +
                                    "<Cliente></Cliente>" +
                                    "<Vendedor></Vendedor>" +
                                    "<ItemsContable></ItemsContable>" +
                                    "<Propiedad1></Propiedad1>" +
                                    "<Propiedad2></Propiedad2>" +
                                    "<Propiedad3></Propiedad3>" +
                                    "<Propiedad4></Propiedad4>" +
                                    "<Propiedad5></Propiedad5>" +
                                    "<CuentaMovimiento></CuentaMovimiento>" +
                                    "<Moneda></Moneda>" +
                                    "<Moneda></Moneda>" +
                                    "</Servicios>" +
                                "</Productos>" +
                            "</Ajuste>";
            request.DynamicProperty = "4";
            request.Action = "Inventario";
            request.TypeSQL = "true";

            var binding = new BasicHttpBinding()
            {
                Name = "BasicHttpBinding_IFakeService",
                MaxBufferSize = 2147483647,
                MaxReceivedMessageSize = 2147483647
            };

            var endpoint = new EndpointAddress("http://192.168.0.85/wsGenericoZeus/ServiceWS.asmx");
            WebservicesGenericoZeusSoapClient client = new WebservicesGenericoZeusSoapClient(binding, endpoint);
            SoapResponse response = await client.ExecuteActionSOAPAsync(request);
            return Convert.ToString(response.Status) == "SUCCESS" ? Ok(response) : BadRequest(response);
        }

        [HttpPut("putEnvioZeus{rollo}")]
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

        [HttpGet("getProduccionSellado/{ot}")]
        public ActionResult<ProcSellado> GetProduccionSellado(string ot)
        {
            var con = from ps in _context.Set<ProcSellado>()
                      where ps.Ot == ot
                      select new 
                      { 
                        Bulto = ps.Item, 
                        Cedula1 = ps.Cedula,
                        Operario1 = ps.Operario,
                        Cedula2 = ps.Cedula2,
                        Operario2 = ps.Operario2,
                        Cedula3 = ps.Cedula3,
                        Operario3 = ps.Operario3,
                        Cedula4 = ps.Cedula4,
                        Operario4 = ps.Operario4, 
                        CantidadUnd = ps.Qty,
                        Presentacion1 = ps.Unidad == "UND" ? "Und" : ps.Unidad == "KLS" ? "Kg" : ps.Unidad == "PAQ" ? "Paquete" : ps.Unidad,
                        Peso = ps.Peso,
                        Presentacion2 = "Kg",
                        Fecha = ps.FechaEntrada.ToString("dd/MM/yyyy"),
                        Hora = ps.Hora,
                      };

            if (con == null) return BadRequest("No existe producción en sellado para esta Orden de Trabajo.");
            else return Ok(con);
        }

        [HttpGet("getEtiquetaBagpro/{rollo}/{reimpresion}")]
        public ActionResult<ProcSellado> GetEtiquetaBagpro(int rollo, int reimpresion)
        {
            var con1 = from ps in _context.Set<ProcSellado>()
                      where ps.Observaciones == "Rollo #" + Convert.ToString(rollo) + " en PBDD.dbo.Produccion_Procesos"
                      select new
                      {
                          Bulto = ps.Item,
                      };

            var con2 = from ps in _context.Set<ProcSellado>()
                       where ps.Item == rollo
                       select new
                       {
                           Bulto = ps.Item,
                       };

            if (reimpresion == 0) return Ok(con1);
            else if (reimpresion == 1) return Ok(con2);
            else return BadRequest("No existe el bulto.");
        }

    }
}