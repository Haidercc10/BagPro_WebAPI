﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BagproWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations.Schema;

namespace BagproWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class ClientesOtItemsController : ControllerBase
    {
        private readonly plasticaribeContext _context;

        public ClientesOtItemsController(plasticaribeContext context)
        {
            _context = context;
        }

        // GET: api/ClientesOtItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClientesOtItem>>> GetClientesOtItems()
        {
          if (_context.ClientesOtItems == null)
          {
              return NotFound();
          }
            return await _context.ClientesOtItems.ToListAsync();
        }

        // GET: api/ClientesOtItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientesOtItem>> GetClientesOtItem(int id)
        {
          if (_context.ClientesOtItems == null)
          {
              return NotFound();
          }
            var clientesOtItem = await _context.ClientesOtItems.FindAsync(id);

            if (clientesOtItem == null)
            {
                return NotFound();
            }

            return clientesOtItem;
        }


        //Consultar através de un LIKE 
        [HttpPost("CalcularKilosItem")]
        public ActionResult CalcularKilosItem([FromBody] List<Items> items)
        {
            List<Object> references = new List<Object>();
            int counter = 0;
            foreach (var item in items)
            {
                string unit = item.unit == "UND" ? "Unidad" : 
                              item.unit == "PAQ" ? "Paquete" : 
                              item.unit == "KLS" ? "Kilo" : "";

                var kilos = (from c in _context.Set<ClientesOtItem>()
                            where c.ClienteItems == item.item
                            && c.PtPresentacionNom == unit
                            select new
                            {
                                Item = c.ClienteItems, 
                                Reference = c.ClienteItemsNom, 
                                Weight = c.PtPresentacionNom == "Kilo" ? item.qty :
                                         c.PtPresentacionNom == "Unidad" ? Convert.ToDecimal((Convert.ToDecimal(item.qty) * Convert.ToDecimal(c.PtPesoMillar.Value)) / Convert.ToInt32(1000)) :
                                         c.PtPresentacionNom == "Paquete" && c.PtQtyBulto == 1 ? (c.PtQtyBulto * c.PtPesoMillar * item.qty) :
                                         c.PtPresentacionNom == "Paquete" && c.PtQtyBulto > 1 ? ((c.PtQtyBulto * c.PtPesoMillar * c.PtQtyPquete) / 1000) * (item.qty / c.PtQtyBulto) : 0,
                                Unit = c.PtPresentacionNom
                            }).FirstOrDefault();
                counter++;
                references.Add(kilos);
                if (items.Count() == counter) return Ok(references);
            }
            return Ok(references);
        }


        //Consultar por Id de Cliente Item
        [HttpPost("OtItem")]
        public  IActionResult GetIdClientesOtItem([FromBody] List<int> rolls)
        {
            List<Object> items = new List<Object>();

            int count = 0;
            foreach (var item in rolls)
            {
                var idClientesItem = from cliOT in _context.Set<ClientesOtItem>()
                                     from ven in _context.Set<Vendedore>()
                                     where cliOT.ClienteItems == item
                                     && cliOT.UsrModifica == ven.Id
                                     select new //ListItems
                                     {
                                         ClienteItems = cliOT.ClienteItems,
                                         ClienteItemsNom = cliOT.ClienteItemsNom,
                                         Cliente = cliOT.Cliente,
                                         ClienteNom = cliOT.ClienteNom,
                                         PtPresentacionNom = cliOT.PtPresentacionNom.Trim() == "Kilo" ? "Kg" : cliOT.PtPresentacionNom.Trim() == "Unidad" ? "Und" : "Paquete",
                                         DatosValorKg = cliOT.DatosValorKg,
                                         NombreCompleto = ven.NombreCompleto
                                     };

                count++;
                items.AddRange(idClientesItem.Take(1));
                if (rolls.Count() == count) return Ok(items);
                //if (idClientesItem == null) return NotFound();
                //else return Ok(idClientesItem.Take(1));
            }
            return Ok(items);
        }

        //Consultar através de un LIKE 
        [HttpGet("likeReferencia/{referencia}")]
        public IActionResult LikeReferencia(string referencia)
        {
            var nombreItem = (from c in _context.Set<ClientesOtItem>()
                              where c.ClienteItemsNom.Contains(referencia)
                              select new { Item = c.ClienteItems, Referencia = c.ClienteItemsNom, PrecioKg = c.DatosValorKg, Categoria = 0 }).Take(50);

            if (nombreItem == null) return NotFound();
            else return Ok(nombreItem);
        }

        // PUT: api/ClientesOtItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientesOtItem(int id, ClientesOtItem clientesOtItem)
        {
            if (id != clientesOtItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(clientesOtItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesOtItemExists(id))
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

        // POST: api/ClientesOtItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ClientesOtItem>> PostClientesOtItem(ClientesOtItem clientesOtItem)
        {
          if (_context.ClientesOtItems == null)
          {
              return Problem("Entity set 'plasticaribeContext.ClientesOtItems'  is null.");
          }
            _context.ClientesOtItems.Add(clientesOtItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClientesOtItem", new { id = clientesOtItem.Id }, clientesOtItem);
        }

        // DELETE: api/ClientesOtItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientesOtItem(int id)
        {
            if (_context.ClientesOtItems == null)
            {
                return NotFound();
            }
            var clientesOtItem = await _context.ClientesOtItems.FindAsync(id);
            if (clientesOtItem == null)
            {
                return NotFound();
            }

            _context.ClientesOtItems.Remove(clientesOtItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientesOtItemExists(int id)
        {
            return (_context.ClientesOtItems?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

public class Items
{ 
    public int item { get; set; } 

    public string unit { get; set; }

    [Precision(18,2)]
    public decimal qty { get; set; }
}

    class ListItems
{
    public int? ClienteItems { get; set; }

    public string ClienteItemsNom { get; set; } = null!;

    public int Cliente { get; set; }

    public string ClienteNom { get; set; } = null!;
    
    public string PtPresentacionNom { get; set; }

    public decimal? DatosValorKg { get; set; }

    public string NombreCompleto { get; set; } = null!;

}
