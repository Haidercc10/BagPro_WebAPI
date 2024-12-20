﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ClientesOtItem
    {
        public int Id { get; set; }
        public int Item { get; set; }
        public int Cliente { get; set; }
        public string ClienteNom { get; set; } = null!;
        public int? ClienteItems { get; set; }
        public string ClienteItemsNom { get; set; } = null!;
        public string? Extrusion { get; set; }
        public string? ExtMaterial { get; set; }
        public string? ExtMaterialNom { get; set; }
        public string? ExtFormato { get; set; }
        public string? ExtFormatoNom { get; set; }
        public string? ExtPigmento { get; set; }
        public string? ExtPigmentoNom { get; set; }
        public string? ExtUnidades { get; set; }
        public string? ExtUnidadesNom { get; set; }
        public decimal? ExtCalibre { get; set; }
        public decimal? ExtPeso { get; set; }
        public decimal? ExtAcho1 { get; set; }
        public decimal? ExtAcho2 { get; set; }
        public decimal? ExtAcho3 { get; set; }
        public string? ExtTratado { get; set; }
        public string? ExtTratadoNom { get; set; }
        public string? Impresion { get; set; }
        public string? ImpFlexoNom { get; set; }
        public string? ImpFlexo { get; set; }
        public int? ImpRodillo { get; set; }
        public int? ImpPista { get; set; }
        public string? ImpTinta1 { get; set; }
        public string? ImpTinta1Nom { get; set; }
        public string? ImpTinta2 { get; set; }
        public string? ImpTinta2Nom { get; set; }
        public string? ImpTinta3 { get; set; }
        public string? ImpTinta3Nom { get; set; }
        public string? ImpTinta4 { get; set; }
        public string? ImpTinta4Nom { get; set; }
        public string? ImpTinta5 { get; set; }
        public string? ImpTinta5Nom { get; set; }
        public string? ImpTinta6 { get; set; }
        public string? ImpTinta6Nom { get; set; }
        public string? ImpTinta7 { get; set; }
        public string? ImpTinta7Nom { get; set; }
        public string? ImpTinta8 { get; set; }
        public string? ImpTinta8Nom { get; set; }
        public string? Lamiando { get; set; }
        public string? LamCapa1 { get; set; }
        public string? LamCapa1Nom { get; set; }
        public string? LamCapa2 { get; set; }
        public string? LamCapa2Nom { get; set; }
        public string? LamCapa3 { get; set; }
        public string? LamCapa3Nom { get; set; }
        public decimal? LamCalibre1 { get; set; }
        public decimal? LamCalibre2 { get; set; }
        public decimal? LamCalibre3 { get; set; }
        public int? Cant1 { get; set; }
        public int? Cant2 { get; set; }
        public int? Cant3 { get; set; }
        public string? Pterminado { get; set; }
        public string? PtFormatopt { get; set; }
        public string? PtFormatoptNom { get; set; }
        public decimal? PtPesopt { get; set; }
        public decimal? PtMargen { get; set; }
        [Precision(18,2)]
        public decimal? PtPesoMillar { get; set; }
        public decimal? PtPesoRollo { get; set; }
        public int? PtQtyBulto { get; set; }
        public string? PtPresentacion { get; set; }
        public string? PtPresentacionNom { get; set; }
        public int? PtQtyPquete { get; set; }
        public string? PtSelladoPt { get; set; }
        public string? PtSelladoPtNom { get; set; }
        public decimal? PtAnchopt { get; set; }
        public decimal? PtLargopt { get; set; }
        public decimal? PtFuelle { get; set; }
        public string? Mezcla { get; set; }
        public string? MezModo { get; set; }
        public string? MezModoNom { get; set; }
        public int? Ncapas { get; set; }
        public decimal? Mezporc1 { get; set; }
        public string? Mezprod1cap1 { get; set; }
        public string? Mezprod1cap1Nom { get; set; }
        public decimal? Porc1cap1 { get; set; }
        public string? Mezprod2cap1 { get; set; }
        public string? Mezprod2cap1Nom { get; set; }
        public decimal? Porc2cap1 { get; set; }
        public string? Mezprod3cap1 { get; set; }
        public string? Mezprod3cap1Nom { get; set; }
        public decimal? Porc3cap1 { get; set; }
        public string? Mezprod4cap1 { get; set; }
        public string? Mezprod4cap1Nom { get; set; }
        public decimal? Porc4cap1 { get; set; }
        public string? Mezpigm1cap1 { get; set; }
        public string? Mezpigm1cap1Nom { get; set; }
        public decimal? Porcpig1cap1 { get; set; }
        public string? Mezpigm2cap1 { get; set; }
        public string? Mezpigm2cap1Nom { get; set; }
        public decimal? Porcpig2cap1 { get; set; }
        public decimal? Mezporc2 { get; set; }
        public string? Mezprod1cap2 { get; set; }
        public string? Mezprod1cap2Nom { get; set; }
        public decimal? Porc1cap2 { get; set; }
        public string? Mezprod2cap2 { get; set; }
        public string? Mezprod2cap2Nom { get; set; }
        public decimal? Porc2cap2 { get; set; }
        public string? Mezprod3cap2 { get; set; }
        public string? Mezprod3cap2Nom { get; set; }
        public decimal? Porc3cap2 { get; set; }
        public string? Mezprod4cap2 { get; set; }
        public string? Mezprod4cap2Nom { get; set; }
        public decimal? Porc4cap2 { get; set; }
        public string? Mezpigm1cap2 { get; set; }
        public string? Mezpigm1cap2Nom { get; set; }
        public decimal? Porcpig1cap2 { get; set; }
        public string? Mezpigm2cap2 { get; set; }
        public string? Mezpigm2cap2Nom { get; set; }
        public decimal? Porcpig2cap2 { get; set; }
        public decimal? Mezporc3 { get; set; }
        public string? Mezprod1cap3 { get; set; }
        public string? Mezprod1cap3Nom { get; set; }
        public decimal? Porc1cap3 { get; set; }
        public string? Mezprod2cap3 { get; set; }
        public string? Mezprod2cap3Nom { get; set; }
        public decimal? Porc2cap3 { get; set; }
        public string? Mezprod3cap3 { get; set; }
        public string? Mezprod3cap3Nom { get; set; }
        public decimal? Porc3cap3 { get; set; }
        public string? Mezprod4cap3 { get; set; }
        public string? Mezprod4cap3Nom { get; set; }
        public decimal? Porc4cap3 { get; set; }
        public string? Mezpigm1cap3 { get; set; }
        public string? Mezpigm1cap3Nom { get; set; }
        public decimal? Porcpig1cap3 { get; set; }
        public string? Mezpigm2cap3 { get; set; }
        public string? Mezpigm2cap3Nom { get; set; }
        public decimal? Porcpig2cap3 { get; set; }
        public string? DatosOt { get; set; }
        public DateTime? DatosFechaDespachar { get; set; }
        public int? DatoscantBolsa { get; set; }
        public decimal? DatosvalorBolsa { get; set; }
        public decimal? DatoscantKg { get; set; }
        public decimal? DatosmargenKg { get; set; }
        public decimal? DatosotKg { get; set; }
        public int? DatoscantBolsa2 { get; set; }
        public decimal? DatosValorKg { get; set; }
        public decimal? DatosvalorOt { get; set; }
        public string? Corte { get; set; }
        public string UsrCrea { get; set; } = null!;
        public DateTime? FechaCrea { get; set; }
        public string? UsrModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
        public string? Pedido { get; set; }
        public string? Cyrel { get; set; }
        public string? Observacion { get; set; }
        public string? Etiqueta { get; set; }
        public string? EtiquetaLargo { get; set; }
        public string? EtiquetaFuelle { get; set; }
        public string? Pesopaquete { get; set; }
        public string? PesoBulto { get; set; }
        public string? Dia { get; set; }
        public string? Noche { get; set; }
        public string? Laminado2 { get; set; }
        public decimal? DiaC { get; set; }
        public decimal? NocheC { get; set; }
       
    }
}
