//using Microsoft.EntityFrameworkCore;

using Aspose.Pdf.Facades;
using BagproWebAPI.Models;
using Intercom.Core;
using Intercom.Data;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Drawing;
using static System.Net.Mime.MediaTypeNames;

namespace BagproWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
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

        //Consulta para obtener los clientes por OT. 
        [HttpGet("getClientsForOT/{ot}")]
        public ActionResult getClientsForOT(int ot) 
        {
            var client = from c in _context.Set<ClientesOt>()
                         where c.Item == ot
                         select new
                         {
                             c.ClienteNom,
                             c.ExtMaterialNom,
                             c.PtAnchopt,
                             c.ExtAcho1,
                             c.ExtAcho2,
                             c.ExtAcho3,
                             c.ExtUnidadesNom, 
                             c.ExtCalibre,
                             c.ExtPigmentoNom,
                             c.DatosValorKg,
                             c.UsrModifica
                             //Fabrication_Day = (from pe in _context.Set<ProcExtrusion>() where Convert.ToInt32(pe.Ot.Trim()) == ot && pe.Item == roll && pe.NomStatus == "EXTRUSION" select pe.Fecha).FirstOrDefault(),
                         }; 
            return Ok(client);
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
            var OT = _context.ClientesOts.Where(OrdTrab => OrdTrab.Item == Item)
                                          .Select(COT => new
                                          {
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
                                              COT.Estado,
                                              NitCliente = (from cl in _context.Clientes where cl.CodBagpro == Convert.ToString(COT.Cliente) select cl.IdentNro).FirstOrDefault(),
                                              COT.Item,
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

        [HttpGet("getOrdenTrabajo/{orden}")]
        public ActionResult GetOrdenTrabajo(int orden)
        {
            var vendedor = (from ot in _context.Set<ClientesOt>()
                            from v in _context.Set<Vendedore>()
                            where ot.Item == orden &&
                                 ot.UsrModifica == v.Id
                            select v.NombreCompleto).FirstOrDefault();

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var con = from ot in _context.Set<ClientesOt>()
                      where ot.Item == orden
                      select new
                      {
                          // Información de la OT
                          Numero_Orden = ot.Item,
                          Id_SedeCliente = ot.Cliente,
                          Id_Cliente = ot.Cliente,
                          Cliente = ot.ClienteNom,
                          Ciudad = "",
                          Direccion = "",
                          Id_Producto = ot.ClienteItems,
                          Producto = ot.ClienteItemsNom,
                          Id_Presentacion = ot.PtPresentacionNom,
                          Presentacion = ot.PtPresentacionNom,
                          Margen = ot.PtMargen,
                          Fecha_Creacion = ot.FechaCrea,
                          Fecha_Entrega = ot.FechaCrea,
                          Estado_Orden = ot.Estado,
                          Estado = ot.Estado,
                          Id_Pedido = 1,
                          Id_Vendedor = ot.UsrModifica,
                          Vendedor = vendedor,
                          Observacion = ot.Observacion,
                          Cyrel = ot.Cyrel,
                          Extrusion = ot.Extrusion,
                          Impresion = ot.Impresion,
                          Rotograbado = ot.Laminado2,
                          Laminado = ot.Lamiando,
                          Corte = ot.Corte,
                          Sellado = ot.Pterminado,
                          Cantidad_Pedida = ot.DatoscantBolsa,
                          Peso_Neto = ot.DatosotKg,
                          Precio_Producto = ot.PtPresentacionNom == "Kilo" ? ot.DatosValorKg : ot.DatosvalorBolsa,

                          // Información de Extrusión
                          Id_Material = ot.ExtMaterial,
                          Material = ot.ExtMaterialNom,
                          Id_Pigmento_Extrusion = ot.ExtPigmento,
                          Pigmento_Extrusion = ot.ExtPigmentoNom,
                          Id_Formato_Extrusion = ot.ExtFormato,
                          Formato_Extrusin = ot.ExtFormatoNom,
                          Id_Tratado = ot.ExtTratado,
                          Tratado = ot.ExtTratadoNom,
                          Calibre_Extrusion = ot.ExtCalibre,
                          Und_Extrusion = ot.ExtUnidadesNom,
                          Ancho1_Extrusion = ot.ExtAcho1,
                          Ancho2_Extrusion = ot.ExtAcho2,
                          Ancho3_Extrusion = ot.ExtAcho3,
                          Peso_Extrusion = ot.ExtPeso,

                          // Información de Impresión
                          Id_Tipo_Imptesion = ot.ImpFlexoNom,
                          Tipo_Impresion = ot.ImpFlexoNom,
                          Rodillo = ot.ImpRodillo,
                          Pista = ot.ImpPista,
                          Id_Tinta1 = ot.ImpTinta1,
                          Tinta1 = ot.ImpTinta1Nom,
                          Id_Tinta2 = ot.ImpTinta2,
                          Tinta2 = ot.ImpTinta2Nom,
                          Id_Tinta3 = ot.ImpTinta3,
                          Tinta3 = ot.ImpTinta3Nom,
                          Id_Tinta4 = ot.ImpTinta4,
                          Tinta4 = ot.ImpTinta4Nom,
                          Id_Tinta5 = ot.ImpTinta5,
                          Tinta5 = ot.ImpTinta5Nom,
                          Id_Tinta6 = ot.ImpTinta6,
                          Tinta6 = ot.ImpTinta6Nom,
                          Id_Tinta7 = ot.ImpTinta7,
                          Tinta7 = ot.ImpTinta7Nom,
                          Id_Tinta8 = ot.ImpTinta8,
                          Tinta8 = ot.ImpTinta8Nom,

                          // Información de Laminado
                          Id_Capa1 = ot.LamCapa1,
                          Laminado_Capa1 = ot.LamCapa1Nom,
                          Calibre_Laminado_Capa1 = ot.LamCalibre1,
                          Cantidad_Laminado_Capa1 = ot.Cant1,
                          Id_Capa2 = ot.LamCapa2,
                          Laminado_Capa2 = ot.LamCapa2Nom,
                          Calibre_Laminado_Capa2 = ot.LamCalibre2,
                          Cantidad_Laminado_Capa2 = ot.Cant2,
                          Id_Capa3 = ot.LamCapa3,
                          Laminado_Capa3 = ot.LamCapa3Nom,
                          Calibre_Laminado_Capa3 = ot.LamCalibre3,
                          Cantidad_Laminado_Capa3 = ot.Cant3,

                          // Información de Sellado
                          Id_Formato_Producto = ot.PtFormatopt,
                          Formato_Producto = ot.PtFormatoptNom,
                          SelladoCorte_Ancho = ot.PtAnchopt,
                          SelladoCorte_Largo = ot.PtLargopt,
                          SelladoCorte_Fuelle = ot.PtFuelle,
                          TpSellado_Id = ot.PtSelladoPt,
                          TpSellados_Nombre = ot.PtSelladoPtNom,
                          SelladoCorte_PesoMillar = ot.PtPesoMillar,
                          SelladoCorte_PesoBulto = ot.PesoBulto,
                          SelladoCorte_CantBolsasBulto = ot.PtQtyBulto,
                          SelladoCorte_CantBolsasPaquete = ot.PtQtyPquete,
                          SelladoCorte_PrecioSelladoDia = (from c in _context.Set<ClientesOtItem>() where c.ClienteItems == ot.ClienteItems && c.PtPresentacionNom == ot.PtPresentacionNom select c.Dia).FirstOrDefault(),
                          SelladoCorte_PrecioSelladoNoche = (from c in _context.Set<ClientesOtItem>() where c.ClienteItems == ot.ClienteItems && c.PtPresentacionNom == ot.PtPresentacionNom select c.Noche).FirstOrDefault(),
                          SelladoCorte_PesoPaquete = ot.Pesopaquete,
                          SelladoCorte_PesoProducto = ot.PtPesopt,
                          SelladoCorte_Etiqueta_Ancho = ot.Etiqueta.Trim() == "" || ot.Etiqueta.Trim() == "0" ? Convert.ToString(ot.PtAnchopt) : ot.Etiqueta.Trim(),
                          SelladoCorte_Etiqueta_Largo = ot.EtiquetaLargo.Trim() == "" || ot.Etiqueta.Trim() == "0" ? Convert.ToString(ot.PtLargopt) : ot.EtiquetaLargo.Trim(),
                          SelladoCorte_Etiqueta_Fuelle = ot.EtiquetaFuelle.Trim() == "" || ot.Etiqueta.Trim() == "0" ? Convert.ToString(ot.PtFuelle) : ot.EtiquetaFuelle.Trim(),

                          // Información de Mezclas
                          Mezcla_Id = ot.MezModo,
                          Mezcla_Nombre = ot.MezModoNom,
                          Mezcla_NroCapas = ot.Ncapas,
                          // Informacion Capa 1
                          Mezcla_PorcentajeCapa1 = ot.Mezporc1,
                          Mezcla_PorcentajeMaterial1_Capa1 = ot.Porc1cap1,
                          MezMaterial_Id1xCapa1 = ot.Mezprod1cap1,
                          M1C1_nombre = ot.Mezprod1cap1Nom,

                          Mezcla_PorcentajeMaterial2_Capa1 = ot.Porc2cap1,
                          MezMaterial_Id2xCapa1 = ot.Mezprod2cap1,
                          M2C1_nombre = ot.Mezprod2cap1Nom,

                          Mezcla_PorcentajeMaterial3_Capa1 = ot.Porc3cap1,
                          MezMaterial_Id3xCapa1 = ot.Mezprod3cap1,
                          M3C1_nombre = ot.Mezprod3cap1Nom,

                          Mezcla_PorcentajeMaterial4_Capa1 = ot.Porc4cap1,
                          MezMaterial_Id4xCapa1 = ot.Mezprod4cap1,
                          M4C1_nombre = ot.Mezprod4cap1Nom,

                          Mezcla_PorcentajePigmto1_Capa1 = ot.Porcpig1cap1,
                          MezPigmto_Id1xCapa1 = ot.Mezpigm1cap1,
                          P1C1_Nombre = ot.Mezpigm1cap1Nom,

                          Mezcla_PorcentajePigmto2_Capa1 = ot.Porcpig2cap1,
                          MezPigmto_Id2xCapa1 = ot.Mezpigm2cap1,
                          P2C1_Nombre = ot.Mezpigm2cap1Nom,

                          //Informacion Capa 2
                          Mezcla_PorcentajeCapa2 = ot.Mezporc2,
                          Mezcla_PorcentajeMaterial1_Capa2 = ot.Porc1cap2,
                          MezMaterial_Id1xCapa2 = ot.Mezprod1cap2,
                          M1C2_nombre = ot.Mezprod1cap2Nom,

                          Mezcla_PorcentajeMaterial2_Capa2 = ot.Porc2cap2,
                          MezMaterial_Id2xCapa2 = ot.Mezprod2cap2,
                          M2C2_nombre = ot.Mezprod2cap2Nom,

                          Mezcla_PorcentajeMaterial3_Capa2 = ot.Porc3cap2,
                          MezMaterial_Id3xCapa2 = ot.Mezprod3cap2,
                          M3C2_nombre = ot.Mezprod3cap2Nom,

                          Mezcla_PorcentajeMaterial4_Capa2 = ot.Porc4cap2,
                          MezMaterial_Id4xCapa2 = ot.Mezprod4cap2,
                          M4C2_nombre = ot.Mezprod4cap2Nom,

                          Mezcla_PorcentajePigmto1_Capa2 = ot.Porcpig1cap2,
                          MezPigmto_Id1xCapa2 = ot.Mezpigm1cap2,
                          P1C2_Nombre = ot.Mezpigm1cap2Nom,

                          Mezcla_PorcentajePigmto2_Capa2 = ot.Porcpig2cap2,
                          MezPigmto_Id2xCapa2 = ot.Mezpigm2cap2,
                          P2C2_Nombre = ot.Mezpigm2cap2Nom,

                          //Informacion Capa 3
                          Mezcla_PorcentajeCapa3 = ot.Mezporc3,
                          Mezcla_PorcentajeMaterial1_Capa3 = ot.Porc1cap3,
                          MezMaterial_Id1xCapa3 = ot.Mezprod1cap3,
                          M1C3_nombre = ot.Mezprod1cap3Nom,

                          Mezcla_PorcentajeMaterial2_Capa3 = ot.Porc2cap3,
                          MezMaterial_Id2xCapa3 = ot.Mezprod2cap3,
                          M2C3_nombre = ot.Mezprod2cap3Nom,

                          Mezcla_PorcentajeMaterial3_Capa3 = ot.Porc3cap3,
                          MezMaterial_Id3xCapa3 = ot.Mezprod3cap3,
                          M3C3_nombre = ot.Mezprod3cap3Nom,

                          Mezcla_PorcentajeMaterial4_Capa3 = ot.Porc4cap3,
                          MezMaterial_Id4xCapa3 = ot.Mezprod4cap3,
                          M4C3_nombre = ot.Mezprod4cap3Nom,

                          Mezcla_PorcentajePigmto1_Capa3 = ot.Porcpig1cap3,
                          MezPigmto_Id1xCapa3 = ot.Mezpigm1cap3,
                          P1C3_Nombre = ot.Mezpigm1cap3Nom,

                          Mezcla_PorcentajePigmto2_Capa3 = ot.Porcpig2cap3,
                          MezPigmto_Id2xCapa3 = ot.Mezpigm2cap3,
                          P2C3_Nombre = ot.Mezpigm2cap3Nom,
                      };
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            return con.Any() ? Ok(con) : BadRequest($"¡No se encontró una Orden de Trabajo con el consecutivo {orden}!");
        }

        /**Consulta por item y presentación*/
        [HttpGet("OT_Cliente_Item_Presentacion/{ClienteItems}/{PtPresentacionNom}")]
        public ActionResult GetItemUltOT(int ClienteItems, string PtPresentacionNom)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var ordenTrabajo = from ot in _context.Set<ClientesOtItem>()
                               where ot.ClienteItems == ClienteItems
                                     && ot.PtPresentacionNom == PtPresentacionNom
                               select new
                               {
                                      Id_OT = ot.Id,
                                      Numero_Orden = ot.Item,
                                      Id_SedeCliente = "",
                                      Id_Cliente = ot.Cliente,
                                      Cliente = ot.ClienteNom.Trim(),
                                      Ciudad = "",
                                      Direccion = "",
                                      Id_Producto = ot.ClienteItems,
                                      Producto = ot.ClienteItemsNom.Trim(),
                                      Id_Presentacion = ot.PtPresentacion,
                                      Presentacion = ot.PtPresentacionNom.Trim(),
                                      Margen = ot.PtMargen,
                                      Fecha_Creacion = ot.FechaCrea,
                                      Fecha_Entrega = ot.FechaCrea,
                                      Estado_Orden = "",
                                      Estado = "",
                                      Id_Pedido = "",
                                      Id_Vendedor = "",
                                      Vendedor = "",
                                      Observacion = ot.Observacion.Trim(),
                                      Cyrel = ot.Cyrel.Trim() == "1" ? true : false,
                                      MotrarEmpresaEtiquetas = false,
                                      Extrusion = ot.Extrusion.Trim() == "1" ? true : false,
                                      Impresion = ot.Impresion.Trim() == "1" ? true : false,
                                      Rotograbado = ot.Lamiando.Trim() == "1" ? true : false,
                                      Laminado = ot.Laminado2.Trim() == "1" ? true : false,
                                      Corte = ot.Corte.Trim() == "1" ? true : false,
                                      Sellado = ot.Pterminado.Trim() == "1" ? true : false,
                                      Cantidad_Pedida = ot.PtPresentacionNom == "Kilo" ? ot.DatoscantKg : ot.DatoscantBolsa,
                                      Peso_Neto = ot.DatosotKg,
                                      ValorKg = ot.DatosValorKg,
                                      ValorUnidad = ot.DatosvalorBolsa,
                                      NitCliente = (from cl in _context.Clientes where cl.CodBagpro == Convert.ToString(ot.Cliente) select cl.IdentNro).FirstOrDefault(),

                                      // Información de Extrusión
                                      Id_Material = Convert.ToInt32(ot.ExtMaterial),
                                      Material = ot.ExtMaterialNom != null ? ot.ExtMaterialNom.Trim() : ot.ExtMaterialNom,
                                      Id_Pigmento_Extrusion = Convert.ToInt32(ot.ExtPigmento),
                                      Pigmento_Extrusion = ot.ExtPigmentoNom.Trim(),
                                      Id_Formato_Extrusion = Convert.ToInt32(ot.ExtFormato) == 5 ? 4 : Convert.ToInt32(ot.ExtFormato),
                                      Formato_Extrusin = ot.ExtFormatoNom.Trim(),
                                      Id_Tratado = Convert.ToInt32(ot.ExtTratado),
                                      Tratado = ot.ExtTratadoNom.Trim(),
                                      Calibre_Extrusion = ot.ExtCalibre,
                                      Und_Extrusion = ot.ExtUnidadesNom.Trim(),
                                      Ancho1_Extrusion = ot.ExtAcho1,
                                      Ancho2_Extrusion = ot.ExtAcho2,
                                      Ancho3_Extrusion = ot.ExtAcho3,
                                      Peso_Extrusion = ot.ExtPeso,

                                      // Información de Impresión
                                      Id_Tipo_Imptesion = ot.ImpFlexo.Trim(),
                                      Tipo_Impresion = ot.ImpFlexoNom.Trim(),
                                      Rodillo = ot.ImpRodillo,
                                      Pista = ot.ImpPista,
                                      Id_Tinta1 = ot.ImpTinta1.Trim(),
                                      Tinta1 = ot.ImpTinta1Nom.Trim() == "" ? "NO APLICA" : ot.ImpTinta1Nom.Trim(),
                                      Id_Tinta2 = ot.ImpTinta2.Trim(),
                                      Tinta2 = ot.ImpTinta2Nom.Trim() == "" ? "NO APLICA" : ot.ImpTinta2Nom.Trim(),
                                      Id_Tinta3 = ot.ImpTinta3.Trim(),
                                      Tinta3 = ot.ImpTinta3Nom.Trim() == "" ? "NO APLICA" : ot.ImpTinta3Nom.Trim(),
                                      Id_Tinta4 = ot.ImpTinta4.Trim(),
                                      Tinta4 = ot.ImpTinta4Nom.Trim() == "" ? "NO APLICA" : ot.ImpTinta4Nom.Trim(),
                                      Id_Tinta5 = ot.ImpTinta5.Trim(),
                                      Tinta5 = ot.ImpTinta5Nom.Trim() == "" ? "NO APLICA" : ot.ImpTinta5Nom.Trim(),
                                      Id_Tinta6 = ot.ImpTinta6.Trim(),
                                      Tinta6 = ot.ImpTinta6Nom.Trim() == "" ? "NO APLICA" : ot.ImpTinta6Nom.Trim(),
                                      Id_Tinta7 = ot.ImpTinta7.Trim(),
                                      Tinta7 = ot.ImpTinta7Nom.Trim() == "" ? "NO APLICA" : ot.ImpTinta7Nom.Trim(),
                                      Id_Tinta8 = ot.ImpTinta8.Trim(),
                                      Tinta8 = ot.ImpTinta8Nom.Trim() == "" ? "NO APLICA" : ot.ImpTinta8Nom.Trim(),

                                      // Información de Laminado
                                      Id_Capa1 = ot.LamCapa1.Trim(),
                                      Laminado_Capa1 = ot.LamCapa1Nom.Trim(),
                                      Calibre_Laminado_Capa1 = ot.LamCalibre1,
                                      Cantidad_Laminado_Capa1 = ot.Cant1,
                                      Id_Capa2 = ot.LamCapa2.Trim(),
                                      Laminado_Capa2 = ot.LamCapa2Nom.Trim(),
                                      Calibre_Laminado_Capa2 = ot.LamCalibre2,
                                      Cantidad_Laminado_Capa2 = ot.Cant2,
                                      Id_Capa3 = ot.LamCapa3.Trim(),
                                      Laminado_Capa3 = ot.LamCapa3Nom.Trim(),
                                      Calibre_Laminado_Capa3 = ot.LamCalibre3,
                                      Cantidad_Laminado_Capa3 = ot.Cant3,

                                      // Información de Sellado
                                      Id_Formato_Producto = ot.PtFormatopt.Trim(),
                                      Formato_Producto = ot.PtFormatoptNom.Trim(),
                                      SelladoCorte_Ancho = ot.PtAnchopt,
                                      SelladoCorte_Largo = ot.PtLargopt,
                                      SelladoCorte_Fuelle = ot.PtFuelle,
                                      SelladoCorte_Etiqueta_Ancho = ot.Etiqueta.Trim(),
                                      SelladoCorte_Etiqueta_Largo = ot.EtiquetaLargo.Trim(),
                                      SelladoCorte_Etiqueta_Fuelle = ot.EtiquetaFuelle.Trim(),
                                      TpSellado_Id = ot.PtSelladoPt.Trim(),
                                      TpSellados_Nombre = ot.PtSelladoPtNom.Trim(),
                                      SelladoCorte_PesoMillar = ot.PtPesoMillar,
                                      SelladoCorte_PesoBulto = ot.PesoBulto,
                                      SelladoCorte_CantBolsasBulto = ot.PtQtyBulto,
                                      SelladoCorte_CantBolsasPaquete = ot.Pesopaquete,
                                      SelladoCorte_PesoPaquete = ot.PtQtyPquete,
                                      SelladoCorte_PesoProducto = ot.PtPesopt,
                                      SelladoCorte_PrecioSelladoDia = ot.Dia,
                                      SelladoCorte_PrecioSelladoNoche = ot.Noche,
                                      SelladoCorte_PrecioDia_Wik_Mq50 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == ot.ClienteItems && wik.Mq == 50 select wik.Dia).First(),
                                      SelladoCorte_PrecioNoche_Wik_Mq50 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == ot.ClienteItems && wik.Mq == 50 select wik.Noche).First(),
                                      SelladoCorte_PrecioDia_Wik_Mq9 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == ot.ClienteItems && wik.Mq == 9 select wik.Dia).First(),
                                      SelladoCorte_PrecioNoche_Wik_Mq9 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == ot.ClienteItems && wik.Mq == 9 select wik.Noche).First(),

                                      // Información de Mezclas
                                      Mezcla_Id = ot.MezModo.Trim(),
                                      Mezcla_Nombre = ot.MezModoNom.Trim(),
                                      Mezcla_NroCapas = ot.Ncapas,
                                      // Informacion Capa 1
                                      Mezcla_PorcentajeCapa1 = ot.Mezporc1,
                                      Mezcla_PorcentajeMaterial1_Capa1 = ot.Porc1cap1,
                                      MezMaterial_Id1xCapa1 = ot.Mezprod1cap1.Trim(),
                                      M1C1_nombre = ot.Mezprod1cap1Nom.Trim(),

                                      Mezcla_PorcentajeMaterial2_Capa1 = ot.Porc2cap1,
                                      MezMaterial_Id2xCapa1 = ot.Mezprod2cap1.Trim(),
                                      M2C1_nombre = ot.Mezprod2cap1Nom.Trim(),

                                      Mezcla_PorcentajeMaterial3_Capa1 = ot.Porc3cap1,
                                      MezMaterial_Id3xCapa1 = ot.Mezprod3cap1.Trim(),
                                      M3C1_nombre = ot.Mezprod3cap1Nom.Trim(),

                                      Mezcla_PorcentajeMaterial4_Capa1 = ot.Porc4cap1,
                                      MezMaterial_Id4xCapa1 = ot.Mezprod4cap1.Trim(),
                                      M4C1_nombre = ot.Mezprod4cap1Nom.Trim(),

                                      Mezcla_PorcentajePigmto1_Capa1 = ot.Porcpig1cap1,
                                      MezPigmto_Id1xCapa1 = ot.Mezpigm1cap1.Trim(),
                                      P1C1_Nombre = ot.Mezpigm1cap1.Trim(),

                                      Mezcla_PorcentajePigmto2_Capa1 = ot.Porcpig2cap1,
                                      MezPigmto_Id2xCapa1 = ot.Mezpigm2cap1.Trim(),
                                      P2C1_Nombre = ot.Mezpigm2cap1.Trim(),
                                      //Informacion Capa 2
                                      Mezcla_PorcentajeCapa2 = ot.Mezporc2,
                                      Mezcla_PorcentajeMaterial1_Capa2 = ot.Porc1cap2,
                                      MezMaterial_Id1xCapa2 = ot.Mezprod1cap2.Trim(),
                                      M1C2_nombre = ot.Mezprod1cap2Nom.Trim(),

                                      Mezcla_PorcentajeMaterial2_Capa2 = ot.Porc2cap2,
                                      MezMaterial_Id2xCapa2 = ot.Mezprod2cap2.Trim(),
                                      M2C2_nombre = ot.Mezprod2cap2Nom.Trim(),

                                      Mezcla_PorcentajeMaterial3_Capa2 = ot.Porc3cap2,
                                      MezMaterial_Id3xCapa2 = ot.Mezprod3cap2.Trim(),
                                      M3C2_nombre = ot.Mezprod3cap2Nom.Trim(),

                                      Mezcla_PorcentajeMaterial4_Capa2 = ot.Porc4cap2,
                                      MezMaterial_Id4xCapa2 = ot.Mezprod4cap2.Trim(),
                                      M4C2_nombre = ot.Mezprod4cap2Nom.Trim(),

                                      Mezcla_PorcentajePigmto1_Capa2 = ot.Porcpig1cap2,
                                      MezPigmto_Id1xCapa2 = ot.Mezpigm1cap2.Trim(),
                                      P1C2_Nombre = ot.Mezpigm1cap2.Trim(),

                                      Mezcla_PorcentajePigmto2_Capa2 = ot.Porcpig2cap2,
                                      MezPigmto_Id2xCapa2 = ot.Mezpigm2cap2.Trim(),
                                      P2C2_Nombre = ot.Mezpigm2cap2.Trim(),
                                      //Informacion Capa 3
                                      Mezcla_PorcentajeCapa3 = ot.Mezporc3,
                                      Mezcla_PorcentajeMaterial1_Capa3 = ot.Porc1cap3,
                                      MezMaterial_Id1xCapa3 = ot.Mezprod1cap3.Trim(),
                                      M1C3_nombre = ot.Mezprod1cap3Nom.Trim(),

                                      Mezcla_PorcentajeMaterial2_Capa3 = ot.Porc2cap3,
                                      MezMaterial_Id2xCapa3 = ot.Mezprod2cap3.Trim(),
                                      M2C3_nombre = ot.Mezprod2cap3Nom.Trim(),

                                      Mezcla_PorcentajeMaterial3_Capa3 = ot.Porc3cap3,
                                      MezMaterial_Id3xCapa3 = ot.Mezprod3cap3.Trim(),
                                      M3C3_nombre = ot.Mezprod3cap3Nom.Trim(),

                                      Mezcla_PorcentajeMaterial4_Capa3 = ot.Porc4cap3,
                                      MezMaterial_Id4xCapa3 = ot.Mezprod4cap3.Trim(),
                                      M4C3_nombre = ot.Mezprod4cap3Nom.Trim(),

                                      Mezcla_PorcentajePigmto1_Capa3 = ot.Porcpig1cap3,
                                      MezPigmto_Id1xCapa3 = ot.Mezpigm1cap3.Trim(),
                                      P1C3_Nombre = ot.Mezpigm1cap3.Trim(),

                                      Mezcla_PorcentajePigmto2_Capa3 = ot.Porcpig2cap3,
                                      MezPigmto_Id2xCapa3 = ot.Mezpigm2cap3.Trim(),
                                      P2C3_Nombre = ot.Mezpigm2cap3.Trim(),
                               };

            return ordenTrabajo.Any() ? Ok(ordenTrabajo) : NotFound();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        /**Consulta por item y presentación*/
        [HttpGet("getOrdenDeTrabajo/{orden}")]
        public ActionResult GetOrdenDeTrabajo(int orden, string? process = "")
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
            var ordenTrabajo = from ot in _context.Set<ClientesOtItem>()
                               from clot in _context.Set<ClientesOt>()
                               where clot.Item == orden
                                     && clot.ClienteItems == ot.ClienteItems
                               select new
                               {
                                   Id_OT = clot.Id,
                                   Numero_Orden = clot.Item,
                                   Id_SedeCliente = "",
                                   Id_Cliente = clot.Cliente,
                                   Cliente = clot.ClienteNom.Trim(),
                                   Ciudad = "",
                                   Direccion = "",
                                   Id_Producto = clot.ClienteItems,
                                   Producto = clot.ClienteItemsNom.Trim(),
                                   Id_Presentacion = clot.PtPresentacion,
                                   Presentacion = clot.PtPresentacionNom.Trim(),
                                   Margen = clot.PtMargen,
                                   Fecha_Creacion = clot.FechaCrea,
                                   Fecha_Entrega = clot.FechaCrea,
                                   Estado_Orden = "",
                                   Estado = "",
                                   Id_Pedido = "",
                                   Id_Vendedor = "",
                                   Vendedor = "",
                                   Observacion = clot.Observacion.Trim(),
                                   Cyrel = clot.Cyrel.Trim() == "1" ? true : false,
                                   MotrarEmpresaEtiquetas = false,
                                   Extrusion = clot.Extrusion.Trim() == "1" ? true : false,
                                   Impresion = clot.Impresion.Trim() == "1" ? true : false,
                                   Rotograbado = clot.Lamiando.Trim() == "1" ? true : false,
                                   Laminado = clot.Laminado2.Trim() == "1" ? true : false,
                                   Corte = clot.Corte.Trim() == "1" ? true : false,
                                   Sellado = clot.Pterminado.Trim() == "1" ? true : false,
                                   Cantidad_Pedida = clot.PtPresentacionNom == "Kilo" ? clot.DatoscantKg : clot.DatoscantBolsa,
                                   Peso_Neto = clot.DatosotKg,
                                   ValorKg = clot.DatosValorKg,
                                   ValorUnidad = clot.DatosvalorBolsa,
                                   //Cantidad_Extrusion = (from pe in _context.ProcExtrusions where pe.Ot == Convert.ToString(orden) && pe.NomStatus == "EXTRUSION" && pe.Observacion != "Etiqueta eliminada desde App Plasticaribe" select pe.Extnetokg == null ? 0 : pe.Extnetokg).Sum(),
                                   //Cantidad_Impresion = (from pe in _context.ProcExtrusions where pe.Ot == Convert.ToString(orden) && pe.NomStatus == "IMPRESION" && pe.Observacion != "Etiqueta eliminada desde App Plasticaribe" select pe.Extnetokg == null ? 0 : pe.Extnetokg).Sum(),
                                   //Cantidad_Rotograbado = (from pe in _context.ProcExtrusions where pe.Ot == Convert.ToString(orden) && pe.NomStatus == "ROTOGRABADO" && pe.Observacion != "Etiqueta eliminada desde App Plasticaribe" select pe.Extnetokg == null ? 0 : pe.Extnetokg).Sum(),
                                   Cantidad_Proceso = (from pe in _context.ProcExtrusions where pe.Ot == Convert.ToString(orden) && pe.NomStatus.StartsWith(process) && pe.Observacion != "Etiqueta eliminada desde App Plasticaribe" select pe.Extnetokg == null ? 0 : pe.Extnetokg).Sum(),
                                   Cantidad_Sellado = (from s in _context.ProcSellados where s.Ot == Convert.ToString(orden) && s.NomStatus.StartsWith(process) && s.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe" select s.Qty == null ? 0 : s.Qty).Sum(),
                                   Peso_Sellado = (from s in _context.ProcSellados where s.Ot == Convert.ToString(orden) && s.NomStatus.StartsWith(process) && s.RemplazoItem != "Etiqueta eliminada desde App Plasticaribe" select s.Qty == null ? 0 : s.Peso).Sum(),
                                   NitCliente = (from cl in _context.Clientes where cl.CodBagpro == Convert.ToString(clot.Cliente) select cl.IdentNro).FirstOrDefault(),
                                   Wicket = (from w in _context.Wiketiandos where w.Id == Convert.ToString(clot.ClienteItems) select w.Id.Trim()).FirstOrDefault(),
                                   

                                   // Información de Extrusión
                                   Id_Material = Convert.ToInt32(clot.ExtMaterial),
                                   Material = clot.ExtMaterialNom.Trim(),
                                   Id_Pigmento_Extrusion = Convert.ToInt32(clot.ExtPigmento),
                                   Pigmento_Extrusion = clot.ExtPigmentoNom.Trim(),
                                   Id_Formato_Extrusion = Convert.ToInt32(ot.ExtFormato) == 5 ? 4 : Convert.ToInt32(ot.ExtFormato),
                                   Formato_Extrusin = ot.ExtFormatoNom.Trim(),
                                   Id_Tratado = Convert.ToInt32(clot.ExtTratado),
                                   Tratado = clot.ExtTratadoNom.Trim(),
                                   Calibre_Extrusion = clot.ExtCalibre,
                                   Und_Extrusion = clot.ExtUnidadesNom.Trim(),
                                   Ancho1_Extrusion = clot.ExtAcho1,
                                   Ancho2_Extrusion = clot.ExtAcho2,
                                   Ancho3_Extrusion = clot.ExtAcho3,
                                   Peso_Extrusion = clot.ExtPeso,

                                   // Información de Impresión
                                   Id_Tipo_Imptesion = clot.ImpFlexo.Trim(),
                                   Tipo_Impresion = clot.ImpFlexoNom.Trim(),
                                   Rodillo = clot.ImpRodillo,
                                   Pista = clot.ImpPista,
                                   Id_Tinta1 = clot.ImpTinta1.Trim(),
                                   Tinta1 = clot.ImpTinta1Nom.Trim() == "" ? "NO APLICA" : clot.ImpTinta1Nom.Trim(),
                                   Id_Tinta2 = clot.ImpTinta2.Trim(),
                                   Tinta2 = clot.ImpTinta2Nom.Trim() == "" ? "NO APLICA" : clot.ImpTinta2Nom.Trim(),
                                   Id_Tinta3 = clot.ImpTinta3.Trim(),
                                   Tinta3 = clot.ImpTinta3Nom.Trim() == "" ? "NO APLICA" : clot.ImpTinta3Nom.Trim(),
                                   Id_Tinta4 = clot.ImpTinta4.Trim(),
                                   Tinta4 = clot.ImpTinta4Nom.Trim() == "" ? "NO APLICA" : clot.ImpTinta4Nom.Trim(),
                                   Id_Tinta5 = clot.ImpTinta5.Trim(),
                                   Tinta5 = clot.ImpTinta5Nom.Trim() == "" ? "NO APLICA" : clot.ImpTinta5Nom.Trim(),
                                   Id_Tinta6 = clot.ImpTinta6.Trim(),
                                   Tinta6 = clot.ImpTinta6Nom.Trim() == "" ? "NO APLICA" : clot.ImpTinta6Nom.Trim(),
                                   Id_Tinta7 = clot.ImpTinta7.Trim(),
                                   Tinta7 = clot.ImpTinta7Nom.Trim() == "" ? "NO APLICA" : clot.ImpTinta7Nom.Trim(),
                                   Id_Tinta8 = clot.ImpTinta8.Trim(),
                                   Tinta8 = clot.ImpTinta8Nom.Trim() == "" ? "NO APLICA" : clot.ImpTinta8Nom.Trim(),

                                   // Información de Laminado
                                   Id_Capa1 = clot.LamCapa1.Trim(),
                                   Laminado_Capa1 = clot.LamCapa1Nom.Trim(),
                                   Calibre_Laminado_Capa1 = clot.LamCalibre1,
                                   Cantidad_Laminado_Capa1 = clot.Cant1,
                                   Id_Capa2 = clot.LamCapa2.Trim(),
                                   Laminado_Capa2 = clot.LamCapa2Nom.Trim(),
                                   Calibre_Laminado_Capa2 = clot.LamCalibre2,
                                   Cantidad_Laminado_Capa2 = clot.Cant2,
                                   Id_Capa3 = clot.LamCapa3.Trim(),
                                   Laminado_Capa3 = clot.LamCapa3Nom.Trim(),
                                   Calibre_Laminado_Capa3 = clot.LamCalibre3,
                                   Cantidad_Laminado_Capa3 = clot.Cant3,

                                   // Información de Sellado
                                   Id_Formato_Producto = clot.PtFormatopt.Trim(),
                                   Formato_Producto = clot.PtFormatoptNom.Trim(),
                                   SelladoCorte_Ancho    = clot.PtAnchopt,
                                   SelladoCorte_Largo = clot.PtLargopt,
                                   SelladoCorte_Fuelle = clot.PtFuelle,
                                   SelladoCorte_Etiqueta_Ancho = ot.Etiqueta.Trim() == "" || ot.Etiqueta.Trim() == "0" ? Convert.ToString(ot.PtAnchopt) : ot.Etiqueta.Trim(),
                                   SelladoCorte_Etiqueta_Largo = ot.EtiquetaLargo.Trim() == "" || ot.Etiqueta.Trim() == "0" ? Convert.ToString(ot.PtLargopt) : ot.EtiquetaLargo.Trim(),
                                   SelladoCorte_Etiqueta_Fuelle = ot.EtiquetaFuelle.Trim() == "" || ot.Etiqueta.Trim() == "0" ? Convert.ToString(ot.PtFuelle) : ot.EtiquetaFuelle.Trim(),
                                   TpSellado_Id = clot.PtSelladoPt.Trim(),
                                   TpSellados_Nombre = clot.PtSelladoPtNom.Trim(),
                                   SelladoCorte_PesoMillar = clot.PtPesoMillar,
                                   SelladoCorte_PesoBulto = clot.PesoBulto,
                                   SelladoCorte_CantBolsasBulto = clot.PtQtyBulto,
                                   SelladoCorte_CantBolsasPaquete = clot.PtQtyPquete,
                                   SelladoCorte_PesoPaquete = clot.Pesopaquete,
                                   SelladoCorte_PesoProducto = clot.PtPesopt,
                                   SelladoCorte_PrecioSelladoDia = ot.Dia,
                                   SelladoCorte_PrecioSelladoNoche = ot.Noche,
                                   SelladoCorte_PesoRollo = clot.PtPesoRollo,
                                   //SelladoCorte_PrecioDia_Wik_Mq50 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == clot.ClienteItems && wik.Mq == 50 select wik.Dia).First() == null ? Convert.ToDecimal(ot.Dia) : (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == clot.ClienteItems && wik.Mq == 50 select wik.Dia).First(),
                                   //SelladoCorte_PrecioNoche_Wik_Mq50 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == clot.ClienteItems && wik.Mq == 50 select wik.Noche).First() == null ? Convert.ToDecimal(ot.Noche) : (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == clot.ClienteItems && wik.Mq == 50 select wik.Noche).First(),
                                   //SelladoCorte_PrecioDia_Wik_Mq9 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == clot.ClienteItems && wik.Mq == 9 select wik.Dia).First() == null ? Convert.ToDecimal(ot.Dia) : (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == clot.ClienteItems && wik.Mq == 9 select wik.Dia).First(),
                                   //SelladoCorte_PrecioNoche_Wik_Mq9 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == clot.ClienteItems && wik.Mq == 9 select wik.Noche).First() == null ? Convert.ToDecimal(ot.Noche) : (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == clot.ClienteItems && wik.Mq == 9 select wik.Noche).First(),

                                   // Información de Mezclas
                                   Mezcla_Id = clot.MezModo.Trim(),
                                   Mezcla_Nombre = clot.MezModoNom.Trim(),
                                   Mezcla_NroCapas = clot.Ncapas,
                                   // Informacion Capa 1
                                   Mezcla_PorcentajeCapa1 = clot.Mezporc1,
                                   Mezcla_PorcentajeMaterial1_Capa1 = clot.Porc1cap1,
                                   MezMaterial_Id1xCapa1 = clot.Mezprod1cap1.Trim(),
                                   M1C1_nombre = clot.Mezprod1cap1Nom.Trim(),

                                   Mezcla_PorcentajeMaterial2_Capa1 = clot.Porc2cap1,
                                   MezMaterial_Id2xCapa1 = clot.Mezprod2cap1.Trim(),
                                   M2C1_nombre = clot.Mezprod2cap1Nom.Trim(),

                                   Mezcla_PorcentajeMaterial3_Capa1 = clot.Porc3cap1,
                                   MezMaterial_Id3xCapa1 = clot.Mezprod3cap1.Trim(),
                                   M3C1_nombre = clot.Mezprod3cap1Nom.Trim(),

                                   Mezcla_PorcentajeMaterial4_Capa1 = clot.Porc4cap1,
                                   MezMaterial_Id4xCapa1 = clot.Mezprod4cap1.Trim(),
                                   M4C1_nombre = clot.Mezprod4cap1Nom.Trim(),

                                   Mezcla_PorcentajePigmto1_Capa1 = clot.Porcpig1cap1,
                                   MezPigmto_Id1xCapa1 = clot.Mezpigm1cap1.Trim(),
                                   P1C1_Nombre = clot.Mezpigm1cap1.Trim(),

                                   Mezcla_PorcentajePigmto2_Capa1 = clot.Porcpig2cap1,
                                   MezPigmto_Id2xCapa1 = clot.Mezpigm2cap1.Trim(),
                                   P2C1_Nombre = clot.Mezpigm2cap1.Trim(),
                                   //Informacion Capa 2
                                   Mezcla_PorcentajeCapa2 = clot.Mezporc2,
                                   Mezcla_PorcentajeMaterial1_Capa2 = clot.Porc1cap2,
                                   MezMaterial_Id1xCapa2 = clot.Mezprod1cap2.Trim(),
                                   M1C2_nombre = clot.Mezprod1cap2Nom.Trim(),

                                   Mezcla_PorcentajeMaterial2_Capa2 = clot.Porc2cap2,
                                   MezMaterial_Id2xCapa2 = clot.Mezprod2cap2.Trim(),
                                   M2C2_nombre = clot.Mezprod2cap2Nom.Trim(),

                                   Mezcla_PorcentajeMaterial3_Capa2 = clot.Porc3cap2,
                                   MezMaterial_Id3xCapa2 = clot.Mezprod3cap2.Trim(),
                                   M3C2_nombre = clot.Mezprod3cap2Nom.Trim(),

                                   Mezcla_PorcentajeMaterial4_Capa2 = clot.Porc4cap2,
                                   MezMaterial_Id4xCapa2 = clot.Mezprod4cap2.Trim(),
                                   M4C2_nombre = clot.Mezprod4cap2Nom.Trim(),

                                   Mezcla_PorcentajePigmto1_Capa2 = clot.Porcpig1cap2,
                                   MezPigmto_Id1xCapa2 = clot.Mezpigm1cap2.Trim(),
                                   P1C2_Nombre = clot.Mezpigm1cap2.Trim(),

                                   Mezcla_PorcentajePigmto2_Capa2 = clot.Porcpig2cap2,
                                   MezPigmto_Id2xCapa2 = clot.Mezpigm2cap2.Trim(),
                                   P2C2_Nombre = clot.Mezpigm2cap2.Trim(),
                                   //Informacion Capa 3
                                   Mezcla_PorcentajeCapa3 = clot.Mezporc3,
                                   Mezcla_PorcentajeMaterial1_Capa3 = clot.Porc1cap3,
                                   MezMaterial_Id1xCapa3 = clot.Mezprod1cap3.Trim(),
                                   M1C3_nombre = clot.Mezprod1cap3Nom.Trim(),

                                   Mezcla_PorcentajeMaterial2_Capa3 = clot.Porc2cap3,
                                   MezMaterial_Id2xCapa3 = clot.Mezprod2cap3.Trim(),
                                   M2C3_nombre = clot.Mezprod2cap3Nom.Trim(),

                                   Mezcla_PorcentajeMaterial3_Capa3 = clot.Porc3cap3,
                                   MezMaterial_Id3xCapa3 = clot.Mezprod3cap3.Trim(),
                                   M3C3_nombre = clot.Mezprod3cap3Nom.Trim(),

                                   Mezcla_PorcentajeMaterial4_Capa3 = clot.Porc4cap3,
                                   MezMaterial_Id4xCapa3 = clot.Mezprod4cap3.Trim(),
                                   M4C3_nombre = clot.Mezprod4cap3Nom.Trim(),

                                   Mezcla_PorcentajePigmto1_Capa3 = clot.Porcpig1cap3,
                                   MezPigmto_Id1xCapa3 = clot.Mezpigm1cap3.Trim(),
                                   P1C3_Nombre = clot.Mezpigm1cap3.Trim(),

                                   Mezcla_PorcentajePigmto2_Capa3 = clot.Porcpig2cap3,
                                   MezPigmto_Id2xCapa3 = clot.Mezpigm2cap3.Trim(),
                                   P2C3_Nombre = clot.Mezpigm2cap3.Trim(),
                               };

            return ordenTrabajo.Any() ? Ok(ordenTrabajo) : NotFound();
#pragma warning restore CS0472 // The result of the expression is always the same since a value of this type is never equal to 'null'
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        /** Consultar ultima OT*/
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

        /** Consultar el primer item en el rango de fechas elegido y que tenga un precio igual al ingresado */
        [HttpGet("consultarItem/{fecha1}/{fecha2}/{item}/{precio}")]
        public ActionResult consultarItem(DateTime fecha1, DateTime fecha2, int item, decimal precio)
        {
            var con = (from ot in _context.Set<ClientesOt>()
                       where ot.FechaCrea >= fecha1
                             && ot.FechaCrea <= fecha2
                             && ot.ClienteItems == item
                             && (ot.DatosValorKg == precio || ot.DatosvalorBolsa == precio)
                       orderby ot.Id
                       select ot.FechaCrea).FirstOrDefault();

            return Ok(con);
        }

        /** Obtener OT's por Usuario Vendedor. */
        [HttpGet("BuscarOTxVendedores/{OT}")]
        public ActionResult GetOtXUsuarioVendedor(int OT)
        {
            var clientesOt = _context.ClientesOts.Where(cOt => cOt.Item == OT)
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

            if (clientesOt == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(clientesOt);
            }
        }

        //Consulta que devolerá la ultima presentación que tuvo un Item 
        [HttpGet("getPresentacionItem/{item}")]
        public ActionResult GetPresentacionItem(int item)
        {
            var presentacion = (from ot in _context.Set<ClientesOtItem>()
                                where ot.ClienteItems == item
                                orderby ot.Item descending
                                select ot).FirstOrDefault();
            return Ok(presentacion);
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

        [HttpGet("getCostoOrdenesUltimoMes/{fecha1}/{fecha2}")]
        public ActionResult getCostoOrdenesUltimoMes(DateTime fecha1, DateTime fecha2, string? sales)
        {
            var con = from ot in _context.Set<ClientesOt>()
                      where ot.FechaCrea >= fecha1
                            && ot.FechaCrea <= fecha2
                            && (string.IsNullOrEmpty(sales) || ot.UsrModifica == (sales))
                      group ot by new
                      {
                          ot.ExtMaterial
                      }
                      into ot
                      select new
                      {
                          costo = ot.Sum(x => x.DatosvalorOt),
                          peso = ot.Sum(x => x.DatosotKg)
                      };
            return Ok(con);
        }

        [HttpGet("getPesoProcesosUltimoMes/{fecha1}/{fecha2}")]
        public ActionResult getPesoProcesosUltimoMes(DateTime fecha1, DateTime fecha2)
        {
            var extrusion = from ot in _context.Set<ProcExtrusion>()
                            where ot.Fecha >= fecha1
                                  && ot.Fecha <= fecha2
                            group ot by new
                            {
                                ot.NomStatus
                            }
                      into ot
                            select new
                            {
                                ot.Key.NomStatus,
                                peso = ot.Sum(x => x.Extnetokg),
                                und = ot.Sum(x => x.Extnetokg),
                            };

            var sellado = from ot in _context.Set<ProcSellado>()
                          where ot.FechaEntrada >= fecha1
                                && ot.FechaEntrada <= fecha2
                          group ot by new
                          {
                              ot.NomStatus
                          }
                      into ot
                          select new
                          {
                              ot.Key.NomStatus,
                              peso = ot.Sum(x => x.Peso),
                              und = ot.Sum(x => x.Qty)
                          };

            return Ok(extrusion.Concat(sellado));

        }

        [HttpGet("getCantOrdenesMateriales/{fecha1}/{fecha2}")]
        public ActionResult getCantOrdenesMateriales(DateTime fecha1, DateTime fecha2, string? sales)
        {
            var con = from ot in _context.Set<ClientesOt>()
                      where ot.FechaCrea >= fecha1
                            && ot.FechaCrea <= fecha2
                            && (string.IsNullOrEmpty(sales) || ot.UsrModifica == (sales))
                      group ot by new
                      {
                          ot.ExtMaterialNom
                      } into ot
                      select new
                      {
                          ot.Key.ExtMaterialNom,
                          peso = ot.Sum(x => x.DatosotKg),
                          cantidad = ot.Count(),
                          costo = ot.Sum(x => x.DatosvalorOt)
                      };
            return Ok(con);
        }

        [HttpGet("getCostoOredenesUltimoMes_Vendedores/{fecha1}/{fecha2}")]
        public ActionResult getCostoOredenesUltimoMes_Vendedores(DateTime fecha1, DateTime fecha2, string? sales)
        {
            var con = from ot in _context.Set<ClientesOt>()
                      from vendedor in _context.Set<Vendedore>()
                      where ot.FechaCrea >= fecha1
                            && ot.FechaCrea <= fecha2
                            && vendedor.Id == ot.UsrModifica
                            && (string.IsNullOrEmpty(sales) || ot.UsrModifica == (sales))
                      group ot by new
                      {
                          ot.UsrModifica,
                          vendedor.NombreCompleto
                      } into ot
                      select new
                      {
                          ot.Key.UsrModifica,
                          ot.Key.NombreCompleto,
                          costo = ot.Sum(x => x.DatosvalorOt),
                          cantidad = ot.Count(),
                          peso = ot.Sum(x => x.DatosotKg)
                      };
            return Ok(con);
        }

        [HttpGet("getCostoOredenesUltimoMes_Clientes/{fecha1}/{fecha2}")]
        public ActionResult getCostoOredenesUltimoMes_Clientes(DateTime fecha1, DateTime fecha2, string? sales)
        {
            var con = from ot in _context.Set<ClientesOt>()
                      where ot.FechaCrea >= fecha1
                            && ot.FechaCrea <= fecha2
                            && (string.IsNullOrEmpty(sales) || ot.UsrModifica == (sales))
                      group ot by new
                      {
                          ot.ClienteNom
                      } into ot
                      select new
                      {
                          ot.Key.ClienteNom,
                          costo = ot.Sum(x => x.DatosvalorOt),
                          cantidad = ot.Count(),
                          peso = ot.Sum(x => x.DatosotKg)
                      };
            return Ok(con);
        }

        [HttpGet("getOrdenesTrabajo/{fechaInicial}/{fechaFinal}")]
        public ActionResult GetOrdenesTrabajo(DateTime fechaInicial, DateTime fechaFinal, string? cliente = "", string? item = "", string? material = "", string? pigmento = "", string? ancho = "", string? largo = "", string? fuelle = "", string? calibre = "")
        {

            /*var client = (from cl in _context.Clientes
                          where cl.IdentNro == Convert.ToString(cliente)
                          select cl.CodBagpro).FirstOrDefault();*/

#pragma warning disable IDE0075 // Simplify conditional expression
            var ordenesTrabajo = from ot in _context.Set<ClientesOt>()
                                 //join cli in _context.Set<Cliente>() on Convert.ToString(ot.Cliente) equals cli.CodBagpro
                                 where ot.FechaCrea >= fechaInicial &&
                                       ot.FechaCrea <= fechaFinal &&
                                       (cliente != "" ? Convert.ToString(ot.Cliente).Contains(cliente) : true) &&
                                       (item != "" ? Convert.ToString(ot.ClienteItems) == item : true) &&
                                       (material != "" ? ot.ExtMaterialNom == material : true) &&
                                       (pigmento != "" ? ot.ExtPigmentoNom == pigmento : true) &&
                                       (ancho != "" ? ot.PtAnchopt == Convert.ToDecimal(ancho) : true) &&
                                       (largo != "" ? ot.PtLargopt == Convert.ToDecimal(largo) : true) &&
                                       (fuelle != "" ? ot.PtFuelle == Convert.ToDecimal(fuelle) : true) &&
                                       (calibre != "" ? ot.ExtCalibre == Convert.ToDecimal(calibre) : true)
                                 select new
                                 {
                                     OrdenTrabajo = ot.Item,
                                     FechaCreacion = ot.FechaCrea,
                                     IdCliente = ot.Cliente,
                                     /*NitCliente = (from cl in _context.Clientes
                                                   where cl.CodBagpro == Convert.ToString(ot.Cliente)
                                                   select cl.IdentNro).FirstOrDefault(),*/
                                     Cliente = ot.ClienteNom,
                                     Item = ot.ClienteItems,
                                     Referencia = ot.ClienteItemsNom,
                                     Item_Referencia = ot.ClienteItems + " " + ot.ClienteItemsNom,
                                     Cantidad = ot.PtPresentacionNom == "Kilo" ? ot.DatoscantKg : ot.DatoscantBolsa,
                                     Precio = ot.PtPresentacionNom == "Kilo" ? ot.DatosValorKg : ot.DatosvalorBolsa,
                                     Existencia = 0,
                                     Presentacion = ot.PtPresentacionNom.Trim(),
                                     Material = ot.ExtMaterialNom.Trim(),
                                     Pigmento = ot.ExtPigmentoNom.Trim(),
                                     UndExtrusion = ot.ExtUnidadesNom.Trim(),
                                     Calibre = ot.ExtCalibre,
                                     Formato = ot.PtFormatoptNom,
                                     Ancho = ot.PtAnchopt,
                                     Largo = ot.PtLargopt,
                                     Fuelle = ot.PtFuelle,
                                     Peso_Metro = ot.ExtPeso,
                                     Rodillo = ot.ImpRodillo,
                                     Kilos = ot.DatosotKg,
                                     Formato_Extrusion = ot.ExtFormatoNom.ToUpper().Trim(),
                                     Ancho_Extrusion = Convert.ToDecimal(ot.ExtAcho2) > 0 ? Convert.ToString(Convert.ToString(" DE ") + Convert.ToString(ot.ExtAcho1).Trim() + "+" + Convert.ToString(ot.ExtAcho2).Trim() + "+" + Convert.ToString(ot.ExtAcho3).Trim() + " " + ot.ExtUnidadesNom.ToUpper().Trim()) : Convert.ToString(" DE " + Convert.ToString(ot.ExtAcho1).Trim() + " " + ot.ExtUnidadesNom.ToUpper().Trim()),
                                     Pigmento_Extrusion = ot.ExtPigmentoNom != "NATURAL" ? Convert.ToString(" PIG.: " + ot.ExtPigmentoNom.Trim()) : "",
                                     Calibre_Extrusion = Convert.ToString(" CAL. " + Convert.ToString(ot.ExtCalibre) + " MILS``"),
                                     Maquinas = (from pe in _context.Set<ProcExtrusion>() where Convert.ToInt32(pe.Ot.Trim()) == ot.Item && pe.NomStatus == Convert.ToString("EXTRUSION") && pe.Fecha >= fechaInicial group pe by new { pe.Maquina } into pe select pe.Key.Maquina).ToList(),
                                     //Colores
                                     Color_1 = ot.ImpTinta1Nom.Trim(),
                                     Color_2 = ot.ImpTinta2Nom.Trim(),
                                     Color_3 = ot.ImpTinta3Nom.Trim(),
                                     Color_4 = ot.ImpTinta4Nom.Trim(),
                                     Color_5 = ot.ImpTinta5Nom.Trim(),
                                     Color_6 = ot.ImpTinta6Nom.Trim(),
                                     Color_7 = ot.ImpTinta7Nom.Trim(),
                                     Color_8 = ot.ImpTinta8Nom.Trim(),
                                     Fecha_Despacho = ot.DatosFechaDespachar,
                                     AnchoPT = ot.ExtAcho1,
                                     LargoPT = ot.ExtAcho2,
                                     FuellePT = ot.ExtAcho3,
                                     Tipo_Sellado = ot.PtSelladoPtNom,
                                     Etiqueta = ot.Etiqueta,
                                     Etiqueta_Largo = ot.EtiquetaLargo,
                                     Etiqueta_Fuelle = ot.EtiquetaFuelle,
                                     Cant_Unidades = ot.DatoscantBolsa,
                                     Fuelle_Izquierdo = ot.ExtAcho2,
                                     Fuelle_Derecho = ot.ExtAcho3,
                                     Fuelle_Fondo = ot.PtFuelle,

                                     AnchoReal = ot.PtSelladoPtNom == "LATERAL" ? ot.PtAnchopt : ot.ExtAcho1,
                                     LargoReal = ot.PtSelladoPtNom == "LATERAL" ? ot.PtLargopt : ot.ExtAcho2,
                                     FuelleReal = ot.PtSelladoPtNom == "LATERAL" ? ot.PtFuelle : ot.ExtAcho3, 
                                     Peso_Millar = ot.PtPesoMillar,
                                 };
            return Ok(ordenesTrabajo);
#pragma warning restore IDE0075 // Simplify conditional expression
        }

        //
        [HttpGet("getOtsForCustomerOrders1/{date1}/{date2}/{item}/{presentation}/{idClient}/{orderSale}")]
        public ActionResult getOtsForCustomerOrders(DateTime date1, DateTime date2, int item, string presentation, string idClient, string orderSale)
        {
            var codBagpro = (from c in _context.Set<Cliente>()
                             where c.IdentNro == idClient
                             select c.CodBagpro).FirstOrDefault();

            var ordersProduction = (from cl in _context.Set<ClientesOt>()
                                    where cl.FechaCrea >= date1 &&
                                    cl.FechaCrea <= date2 &&
                                    cl.ClienteItems == item &&
                                    cl.PtPresentacionNom == presentation &&
                                    cl.Cliente == Convert.ToInt32(codBagpro)
                                    select new { 
                                       OT = cl.Item, 
                                       Consecutivo = orderSale 
                                    }).FirstOrDefault();

            return Ok(ordersProduction);
        }

        //
        [HttpPost("getOtsForCustomerOrders2")]
        public ActionResult getOtsForCustomerOrders([FromBody] List<CustomerOrders> CustomerOrders)
        {
            List<Object> orders = new List<Object>();
            int counter = 0;
            foreach (var item in CustomerOrders)
            {
                //var codBagpro = (from c in _context.Set<Cliente>()
                //                 where c.IdentNro == item.idClient
                //                 select c.CodBagpro).FirstOrDefault();

                var ordersProduction = (from cl in _context.Set<ClientesOt>()
                                        where cl.FechaCrea >= item.date1 &&
                                              cl.FechaCrea <= item.date2 &&
                                              cl.ClienteItems == item.item //&&
                                              //cl.PtPresentacionNom == item.presentation //&&
                                              //cl.Cliente == Convert.ToInt32(codBagpro)
                                        select new
                                        { 
                                            OT = cl.Item,
                                            Consecutivo = item.consecutivo,
                                            Item = cl.ClienteItems
                                        }).FirstOrDefault();

                counter++;
                orders.Add(ordersProduction);
                if (CustomerOrders.Count() == counter) return Ok(orders);
            }
            return Ok(orders);
        }

        [HttpGet("getOtControlCalidadExtrusion/{OT}/{status}")]
        public ActionResult GetOtControlCalidadExtrusion(string OT, string status)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
            var query = from cl in _context.Set<ClientesOt>()
                        where Convert.ToString(cl.Item) == OT
                        select new
                        {
                            OT = cl.Item,
                            Id_Cliente = cl.Cliente,
                            Cliente = cl.ClienteNom,
                            Item = cl.ClienteItems,
                            Referencia = cl.ClienteItemsNom,
                            Maquina = (from pe in _context.Set<ProcExtrusion>() 
                                       where pe.Ot == Convert.ToString(cl.Item) && pe.NomStatus == status 
                                       group pe by new { pe.Maquina } 
                                       into pe
                                       select Convert.ToInt32(pe.Key.Maquina)).ToList(),
                            PigmentoId = cl.ExtPigmento.Trim(),
                            Pigmento = cl.ExtPigmentoNom.Trim(),
                            Calibre = cl.ExtCalibre,
                            Ancho = cl.PtAnchopt,
                            Largo = cl.PtLargopt,
                            CantBolsasxPaq = cl.PtQtyPquete,
                            AnchoFuelle_Derecha = cl.ExtAcho1,
                            AnchoFuelle_Izquierda = cl.ExtAcho2,
                            AnchoFuelle_Abajo = cl.ExtAcho3,
                            TratadoId = Convert.ToString(cl.ExtTratado).Trim(),
                            Tratado = Convert.ToString(cl.ExtTratadoNom).Trim(),
                            Impresion = Convert.ToString(cl.Impresion).Trim(),
                            FormatoId = Convert.ToString(cl.ExtFormato).Trim(),
                            Formato = Convert.ToString(cl.ExtFormatoNom).Trim(),
                        };

            /*var query2 = from cl in _context.Set<ClientesOt>()
                         where Convert.ToString(cl.Item) == OT
                         select new
                         {
                             OT = cl.Item,
                             Id_Cliente = cl.Cliente,
                             Cliente = cl.ClienteNom,
                             Item = cl.ClienteItems,
                             Referencia = cl.ClienteItemsNom,
                             Maquina = (from ps in _context.Set<ProcSellado>() 
                                        where ps.Ot ==  && 
                                        ps.NomStatus == status 
                                        select Convert.ToInt32(ps.Maquina)).ToList(),
                             PigmentoId = cl.ExtPigmento.Trim(),
                             Pigmento = cl.ExtPigmentoNom.Trim(),
                             Calibre = cl.ExtCalibre,
                             Ancho = cl.PtAnchopt,
                             Largo = cl.PtLargopt,
                             CantBolsasxPaq = cl.PtQtyPquete,
                             AnchoFuelle_Derecha = cl.ExtAcho1,
                             AnchoFuelle_Izquierda = cl.ExtAcho2,
                             AnchoFuelle_Abajo = cl.ExtAcho3,
                             TratadoId = Convert.ToString("No aplica"),
                             Tratado = Convert.ToString("No aplica"),
                             Impresion = Convert.ToString(cl.Impresion).Trim(),
                         };*/

            //if (query == null && query2 == null) return BadRequest("La OT consultada no se encuentra en el proceso seleccionado!");
            //return Ok(query.Concat(query2));

            return Ok(query);
#pragma warning restore CS8604 // Possible null reference argument.
#pragma warning restore CS8602 // Dereference of a possibly null reference.
        }

        [HttpPut("putOrdenTrabajo{orden}")]
        public async Task<IActionResult> PutOrdenTrabajo(int orden, ClientesOt clientesOt)
        {
            if (orden != clientesOt.Item)  return BadRequest();

            _context.Entry(clientesOt).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesOtExists(orden))
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

        [HttpGet("Prueba")]
        public IActionResult GeneratePDF()
        {
            using (var stream = new System.IO.MemoryStream())
            {
                using (var writer = new PdfWriter(stream))
                {
                    using (var pdf = new PdfDocument(writer))
                    {
                        var doc = new Document(pdf);
                        doc.Add(new Paragraph("Hola mundo"));
                    }
                }
                Response.Headers.Add("content-disposition", "attachment; filename = test.pdf");
                return File(stream.ToArray(), "application / pdf");
            }    
        }
    }
}

public class CustomerOrders
{
    public DateTime date1 { get; set; }

    public DateTime date2 { get; set; }

    public int item { get; set; }

    //public string presentation { get; set; }

    //public string idClient { get; set; }

    public string consecutivo { get; set; }
}
