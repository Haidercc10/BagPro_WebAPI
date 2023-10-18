//using Microsoft.EntityFrameworkCore;
using BagproWebAPI.Models;
using Intercom.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

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

        [HttpGet("getOrdenTrabajo/{orden}")]
        public ActionResult GetOrdenTrabajo(int orden)
        {
            var vendedor = (from ot in _context.Set<ClientesOt>()
                            from v in _context.Set<Vendedore>()
                            where ot.Item == orden &&
                                 ot.UsrModifica == v.Id
                            select v.NombreCompleto).FirstOrDefault();

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

            return con.Any() ? Ok(con) : BadRequest($"¡No se encontró una Orden de Trabajo con el consecutivo {orden}!");
        }

        /**Consulta por item y presentación*/
        [HttpGet("OT_Cliente_Item_Presentacion/{ClienteItems}/{PtPresentacionNom}")]
        public ActionResult GetItemUltOT(int ClienteItems, string PtPresentacionNom)
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            var con = from ot in _context.Set<ClientesOtItem>()
                      where ot.ClienteItems == ClienteItems
                            && ot.PtPresentacionNom == PtPresentacionNom
                      select new
                      {
                          //DATOS ORDEN
                          ot.ClienteItems,
                          ot.ClienteItemsNom,
                          ot.DatoscantBolsa,
                          ot.DatosvalorBolsa,
                          ot.DatoscantKg,
                          ot.DatosmargenKg,
                          ot.DatosotKg,
                          ot.DatosValorKg,
                          ot.DatosvalorOt,
                          Cyrel = ot.Cyrel.Trim(),
                          Observacion = ot.Observacion.Trim(),

                          //EXTRUSION
                          Extrusion = ot.Extrusion.Trim(),
                          ExtMaterial = ot.ExtMaterial.Trim(),
                          ExtMaterialNom = ot.ExtMaterialNom.Trim(),
                          ExtFormato = ot.ExtFormato.Trim(),
                          ExtFormatoNom = ot.ExtFormatoNom.Trim(),
                          ExtPigmento = ot.ExtPigmento.Trim(),
                          ExtPigmentoNom = ot.ExtPigmentoNom.Trim(),
                          ExtUnidades = ot.ExtUnidades.Trim(),
                          ExtUnidadesNom = ot.ExtUnidadesNom.Trim(),
                          ot.ExtCalibre,
                          ot.ExtPeso,
                          ot.ExtAcho1,
                          ot.ExtAcho2,
                          ot.ExtAcho3,
                          ExtTratado = ot.ExtTratado.Trim(),
                          ExtTratadoNom = ot.ExtTratadoNom.Trim(),

                          //IMPRESION
                          Laminado2 = ot.Laminado2.Trim(),
                          Impresion = ot.Impresion.Trim(),
                          ImpFlexo = ot.ImpFlexo.Trim(),
                          ImpFlexoNom = ot.ImpFlexoNom.Trim(),
                          ot.ImpRodillo,
                          ot.ImpPista,
                          ImpTinta1 = ot.ImpTinta1.Trim(),
                          ImpTinta1Nom = ot.ImpTinta1Nom.Trim(),
                          ImpTinta2 = ot.ImpTinta2.Trim(),
                          ImpTinta2Nom = ot.ImpTinta2Nom.Trim(),
                          ImpTinta3 = ot.ImpTinta3.Trim(),
                          ImpTinta3Nom = ot.ImpTinta3Nom.Trim(),
                          ImpTinta4 = ot.ImpTinta4.Trim(),
                          ImpTinta4Nom = ot.ImpTinta4Nom.Trim(),
                          ImpTinta5 = ot.ImpTinta5.Trim(),
                          ImpTinta5Nom = ot.ImpTinta5Nom.Trim(),
                          ImpTinta6 = ot.ImpTinta6.Trim(),
                          ImpTinta6Nom = ot.ImpTinta6Nom.Trim(),
                          ImpTinta7 = ot.ImpTinta7.Trim(),
                          ImpTinta7Nom = ot.ImpTinta7Nom.Trim(),
                          ImpTinta8 = ot.ImpTinta8.Trim(),
                          ImpTinta8Nom = ot.ImpTinta8Nom.Trim(),

                          //LAMINADO
                          Lamiando = ot.Lamiando.Trim(),
                          LamCapa1 = ot.LamCapa1.Trim(),
                          LamCapa1Nom = ot.LamCapa1Nom.Trim(),
                          ot.LamCalibre1,
                          ot.Cant1,
                          LamCapa2 = ot.LamCapa2.Trim(),
                          LamCapa2Nom = ot.LamCapa2Nom.Trim(),
                          ot.LamCalibre2,
                          ot.Cant2,
                          LamCapa3 = ot.LamCapa3.Trim(),
                          LamCapa3Nom = ot.LamCapa3Nom.Trim(),
                          ot.LamCalibre3,
                          ot.Cant3,

                          //PRODUCTO TERMINADO - SELLADO - CORTE
                          Pterminado = ot.Pterminado.Trim(),
                          Corte = ot.Corte.Trim(),
                          PtFormatopt = ot.PtFormatopt.Trim(),
                          PtFormatoptNom = ot.PtFormatoptNom.Trim(),
                          ot.PtPesopt,
                          ot.PtMargen,
                          ot.PtPesoMillar,
                          ot.PtPesoRollo,
                          ot.PtQtyBulto,
                          PesoBulto = ot.PesoBulto.Trim(),
                          PtPresentacion = ot.PtPresentacion.Trim(),
                          PtPresentacionNom = ot.PtPresentacionNom.Trim(),
                          ot.PtQtyPquete,
                          Pesopaquete = ot.Pesopaquete.Trim(),
                          PtSelladoPt = ot.PtSelladoPt.Trim(),
                          PtSelladoPtNom = ot.PtSelladoPtNom.Trim(),
                          ot.PtAnchopt,
                          ot.PtLargopt,
                          ot.PtFuelle,
                          Etiqueta = ot.Etiqueta.Trim(),
                          EtiquetaLargo = ot.EtiquetaLargo.Trim(),
                          EtiquetaFuelle = ot.EtiquetaFuelle.Trim(),
                          Dia = ot.Dia.Trim(),
                          Noche = ot.Noche.Trim(),
                          Wik_Dia_MQ_50 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == ot.ClienteItems && wik.Mq == 50 select wik.Dia).First(),
                          Wik_Noche_MQ_50 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == ot.ClienteItems && wik.Mq == 50 select wik.Noche).First(),
                          Wik_Dia_MQ_9 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == ot.ClienteItems && wik.Mq == 9 select wik.Dia).First(),
                          Wik_Noche_MQ_9 = (from wik in _context.Set<Wiketiando>() where Convert.ToInt32(wik.Codigo) == ot.ClienteItems && wik.Mq == 9 select wik.Noche).First(),

                          //MEZCLA
                          Mezcla = ot.Mezcla.Trim(),
                          MezModo = ot.MezModo.Trim(),
                          MezModoNom = ot.MezModoNom.Trim(),
                          ot.Ncapas,
                          //Capa 1
                          ot.Mezporc1,
                          Mezprod1cap1 = ot.Mezprod1cap1.Trim(),
                          Mezprod1cap1Nom = ot.Mezprod1cap1Nom.Trim(),
                          ot.Porc1cap1,
                          Mezprod2cap1 = ot.Mezprod2cap1.Trim(),
                          Mezprod2cap1Nom = ot.Mezprod2cap1Nom.Trim(),
                          ot.Porc2cap1,
                          Mezprod3cap1 = ot.Mezprod3cap1.Trim(),
                          Mezprod3cap1Nom = ot.Mezprod4cap1Nom.Trim(),
                          ot.Porc3cap1,
                          Mezprod4cap1 = ot.Mezprod4cap1.Trim(),
                          Mezprod4cap1Nom = ot.Mezprod4cap1Nom.Trim(),
                          ot.Porc4cap1,
                          Mezpigm1cap1 = ot.Mezpigm1cap1.Trim(),
                          Mezpigm1cap1Nom = ot.Mezpigm1cap1Nom.Trim(),
                          ot.Porcpig1cap1,
                          Mezpigm2cap1 = ot.Mezpigm2cap1.Trim(),
                          Mezpigm2cap1Nom = ot.Mezpigm2cap1Nom.Trim(),
                          ot.Porcpig2cap1,
                          //Capa 2
                          ot.Mezporc2,
                          Mezprod1cap2 = ot.Mezprod1cap2.Trim(),
                          Mezprod1cap2Nom = ot.Mezprod1cap2Nom.Trim(),
                          ot.Porc1cap2,
                          Mezprod2cap2 = ot.Mezprod2cap2.Trim(),
                          Mezprod2cap2Nom = ot.Mezprod2cap2Nom.Trim(),
                          ot.Porc2cap2,
                          Mezprod3cap2 = ot.Mezprod3cap2.Trim(),
                          Mezprod3cap2Nom = ot.Mezprod4cap2Nom.Trim(),
                          ot.Porc3cap2,
                          Mezprod4cap2 = ot.Mezprod4cap2.Trim(),
                          Mezprod4cap2Nom = ot.Mezprod4cap2Nom.Trim(),
                          ot.Porc4cap2,
                          Mezpigm1cap2 = ot.Mezpigm1cap2.Trim(),
                          Mezpigm1cap2Nom = ot.Mezpigm1cap2Nom.Trim(),
                          ot.Porcpig1cap2,
                          Mezpigm2cap2 = ot.Mezpigm2cap2.Trim(),
                          Mezpigm2cap2Nom = ot.Mezpigm2cap2Nom.Trim(),
                          ot.Porcpig2cap2,
                          //Capa 3
                          ot.Mezporc3,
                          Mezprod1cap3 = ot.Mezprod1cap3.Trim(),
                          Mezprod1cap3Nom = ot.Mezprod1cap3Nom.Trim(),
                          ot.Porc1cap3,
                          Mezprod2cap3 = ot.Mezprod2cap3.Trim(),
                          Mezprod2cap3Nom = ot.Mezprod2cap3Nom.Trim(),
                          ot.Porc2cap3,
                          Mezprod3cap3 = ot.Mezprod3cap3.Trim(),
                          Mezprod3cap3Nom = ot.Mezprod4cap3Nom.Trim(),
                          ot.Porc3cap3,
                          Mezprod4cap3 = ot.Mezprod4cap3.Trim(),
                          Mezprod4cap3Nom = ot.Mezprod4cap3Nom.Trim(),
                          ot.Porc4cap3,
                          Mezpigm1cap3 = ot.Mezpigm1cap3.Trim(),
                          Mezpigm1cap3Nom = ot.Mezpigm1cap3Nom.Trim(),
                          ot.Porcpig1cap3,
                          Mezpigm2cap3 = ot.Mezpigm2cap3.Trim(),
                          Mezpigm2cap3Nom = ot.Mezpigm2cap3Nom.Trim(),
                          ot.Porcpig2cap3,


                      };

            return con.Any() ? Ok(con) : NotFound();
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
            var presentacion = (from ot in _context.Set<ClientesOt>()
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
        public ActionResult getCostoOrdenesUltimoMes(DateTime fecha1, DateTime fecha2)
        {
            var con = from ot in _context.Set<ClientesOt>()
                      where ot.FechaCrea >= fecha1
                            && ot.FechaCrea <= fecha2
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
        public ActionResult getCantOrdenesMateriales(DateTime fecha1, DateTime fecha2)
        {
            var con = from ot in _context.Set<ClientesOt>()
                      where ot.FechaCrea >= fecha1
                            && ot.FechaCrea <= fecha2
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
        public ActionResult getCostoOredenesUltimoMes_Vendedores(DateTime fecha1, DateTime fecha2)
        {
            var con = from ot in _context.Set<ClientesOt>()
                      from vendedor in _context.Set<Vendedore>()
                      where ot.FechaCrea >= fecha1
                            && ot.FechaCrea <= fecha2
                            && vendedor.Id == ot.UsrModifica
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
        public ActionResult getCostoOredenesUltimoMes_Clientes(DateTime fecha1, DateTime fecha2)
        {
            var con = from ot in _context.Set<ClientesOt>()
                      where ot.FechaCrea >= fecha1
                            && ot.FechaCrea <= fecha2
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
