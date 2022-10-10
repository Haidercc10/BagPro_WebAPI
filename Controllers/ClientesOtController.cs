using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using BagproWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BagproWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesOtController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public ClientesOtController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/ClientesOt
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesOt>>> GetClientesOts()
        {
          if (_context.ClientesOts == null)
          {
              return NotFound();
          }
            return await _context.ClientesOts.ToListAsync();
        }

        // GET: api/ClientesOt/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesOt>> GetClientesOt(int id)
        {
          if (_context.ClientesOts == null)
          {
              return NotFound();
          }
            var clientesOt = await _context.ClientesOts.FindAsync(id);

            if (clientesOt == null)
            {
                return NotFound();
            }

            return clientesOt;
        }


        /** Obtener costos de clientes OT*/
        [HttpGet("CostosOT/{Item}")]
        public IActionResult GetCostostOt(int Item)
        {
            if (_context.ClientesOts == null)
            {
                return NotFound();
            }
            var OT =  _context.ClientesOts.Where(OrdTrab => OrdTrab.Item == Item)
                                          .Select(COT => new { 
                                          COT.ClienteNom,
                                          COT.ClienteItems,
                                          COT.ClienteItemsNom,
                                          COT.PtPresentacionNom,
                                          COT.DatoscantKg,
                                          COT.DatosmargenKg,
                                          COT.DatosotKg,
                                          COT.DatoscantBolsa,
                                          COT.DatosvalorBolsa,
                                          COT.DatosValorKg, 
                                          COT.DatosvalorOt,
                                          COT.FechaCrea,
                                          COT.UsrCrea,
                                          COT.Estado
                                          }).ToList();
            if (OT == null)
            {
                return BadRequest();
            } 
            else
            {
                return Ok(OT);
            }
        }


        [HttpGet("BetweenFechas/")]
        public IActionResult GetEntreFechas(DateTime FechaInicial, DateTime FechaFinal)
        {
            if (_context.ClientesOts == null)
            {
                return NotFound();
            }
            var OT = _context.ClientesOts.Where(CO => CO.FechaCrea >= FechaInicial && CO.FechaCrea <= FechaFinal)
                                          .Select(COT => new {
                                              COT.Item,
                                              COT.ClienteNom,
                                              COT.ClienteItems,
                                              COT.ClienteItemsNom,
                                              COT.PtPresentacionNom,
                                              COT.DatoscantKg,
                                              COT.DatosmargenKg,
                                              COT.DatosotKg,
                                              COT.DatoscantBolsa,
                                              COT.DatosvalorBolsa,
                                              COT.DatosValorKg,
                                              COT.DatosvalorOt,
                                              COT.FechaCrea,
                                              COT.UsrCrea,
                                              COT.Estado
                                          }).ToList();
            if (OT == null)
            {
                return BadRequest();
            }
            else
            {
                return Ok(OT);
            }
        }

        // GET: api/ClientesOt/5

        [HttpGet("OT/{item}")]
        public ActionResult<ClientesOt> GetOt(int item)
        {
            var clientesOt = _context.ClientesOts.Where(cOt => cOt.Item == item).ToList();  

            if (clientesOt == null)
            {
                return NotFound();
            }

            return Ok(clientesOt);
        }

        /** Obtener OT's por Usuario Vendedor. */
        [HttpGet("BuscarOTxVendedores/{usuarioVendedor}")]
        public ActionResult<ClientesOt> GetOtXUsuarioVendedor(string usuarioVendedor)
        {
            var clientesOt = _context.ClientesOts.Where(cOt => cOt.UsrModifica == usuarioVendedor)
                                                 .Select(coti => new
                                                 {
                                                    coti.Item, 
                                                    coti.Cliente,
                                                    coti.ClienteNom,
                                                    coti.ClienteItems,
                                                    coti.ClienteItemsNom,
                                                    coti.UsrModifica
                                                 })
                                                 .ToList();
            return Ok(clientesOt);
        }

        [HttpGet("OT_Cliente_Item_Presentacion/{Cliente}/{ClienteItems}/{PtPresentacionNom}")]
        public ActionResult<ClientesOt> GetOt(string Cliente, int ClienteItems, string PtPresentacionNom)
        {
            var clientesOt = _context.ClientesOts
                .Where(cOt => cOt.ClienteNom == Cliente && cOt.ClienteItems == ClienteItems && cOt.PtPresentacionNom == PtPresentacionNom)
                .OrderBy(cOt => cOt.Item)
                .Last();

            if (clientesOt == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(clientesOt);
            }
        }

        [HttpGet("UltimaOT/")]
        public ActionResult<ClientesOt> GetUltimaOt()
        {
            var clientesOt = _context.ClientesOts.OrderBy(cOt => cOt.Item).Last();

            if (clientesOt == null)
            {
                return NotFound();
            }

            return Ok(clientesOt);
        }

        [HttpGet("consultarItem/{fecha1}/{fecha2}/{item}/{precio}")]
        public ActionResult get(DateTime fecha1, DateTime fecha2, int item, decimal precio)
        {
            var con = _context.ClientesOts
                .Where(x => x.FechaCrea >= fecha1
                       && x.FechaCrea <= fecha2
                       && x.ClienteItems == item
                       && precio == x.DatosValorKg)
                .OrderByDescending(x => x.Id)
                .ToList();
            return Ok(con);
        }

        // PUT: api/ClientesOt/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientesOt(int id, ClientesOt clientesOt)
        {
            if (id != clientesOt.Item)
            {
                return BadRequest();
            }

            _context.Entry(clientesOt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesOtExists(id))
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

        /** Actualizar Estado de Ordenes */
        [HttpPut("CambioEstadoOT/{Item}")]
        public ActionResult PutEstadoClientesOt(int Item, ClientesOt clientesOt, string Estado)
        {
            
            if (Item != clientesOt.Item)
            {
                return BadRequest();
            }

            var Actualizado = _context.ClientesOts.Where(x => x.Item == Item)
                                                 .First<ClientesOt>();

            try
            {
                  
                Actualizado.Estado = Estado;
                _context.SaveChanges();
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesOtExists(Item))
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




        // POST: api/ClientesOt
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientesOt>> PostClientesOt(ClientesOt clientesOt)
        {
          if (_context.ClientesOts == null)
          {
              return Problem("Entity set 'plasticaribeContext.ClientesOts'  is null.");
          }
            _context.ClientesOts.Add(clientesOt);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ClientesOtExists(clientesOt.Item))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetClientesOt", new { id = clientesOt.Item }, clientesOt);
        }

        // DELETE: api/ClientesOt/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientesOt(int id)
        {
            if (_context.ClientesOts == null)
            {
                return NotFound();
            }
            var clientesOt = await _context.ClientesOts.FindAsync(id);
            if (clientesOt == null)
            {
                return NotFound();
            }

            _context.ClientesOts.Remove(clientesOt);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientesOtExists(int id)
        {
            return (_context.ClientesOts?.Any(e => e.Item == id)).GetValueOrDefault();
        }
    }
}
