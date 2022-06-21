using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class BpClientesItem
    {
        public int Empresa { get; set; }
        public int Codigo { get; set; }
        public string Cliente { get; set; } = null!;
        public string ClienteIdentNro { get; set; } = null!;
        public string ClienteNombre { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string ItemBarCode { get; set; } = null!;
        public string OtIndicaciones { get; set; } = null!;
        public string MezclaPreDef { get; set; } = null!;
        public short MezclasNro { get; set; }
        public string Material { get; set; } = null!;
        public decimal Material1X100 { get; set; }
        public decimal Material2X100 { get; set; }
        public decimal Material3X100 { get; set; }
        public int M1p1id { get; set; }
        public string M1p1nombre { get; set; } = null!;
        public decimal M1p1x100 { get; set; }
        public int M1p2id { get; set; }
        public string M1p2nombre { get; set; } = null!;
        public decimal M1p2x100 { get; set; }
        public int M1p3id { get; set; }
        public string M1p3nombre { get; set; } = null!;
        public decimal M1p3x100 { get; set; }
        public int M1p4id { get; set; }
        public string M1p4nombre { get; set; } = null!;
        public decimal M1p4x100 { get; set; }
        public int M1pig1Id { get; set; }
        public int M1pig2Id { get; set; }
        public string M1pig1Nombre { get; set; } = null!;
        public string M1pig2Nombre { get; set; } = null!;
        public decimal M1pig1X100 { get; set; }
        public decimal M1pig2X100 { get; set; }
        public int M2p1id { get; set; }
        public string M2p1nombre { get; set; } = null!;
        public decimal M2p1x100 { get; set; }
        public int M2p2id { get; set; }
        public string M2p2nombre { get; set; } = null!;
        public decimal M2p2x100 { get; set; }
        public int M2p3id { get; set; }
        public string M2p3nombre { get; set; } = null!;
        public decimal M2p3x100 { get; set; }
        public int M2p4id { get; set; }
        public string M2p4nombre { get; set; } = null!;
        public decimal M2p4x100 { get; set; }
        public int M2pig1Id { get; set; }
        public int M2pig2Id { get; set; }
        public string M2pig1Nombre { get; set; } = null!;
        public string M2pig2Nombre { get; set; } = null!;
        public decimal M2pig1X100 { get; set; }
        public decimal M2pig2X100 { get; set; }
        public int M3p1id { get; set; }
        public string M3p1nombre { get; set; } = null!;
        public decimal M3p1x100 { get; set; }
        public int M3p2id { get; set; }
        public string M3p2nombre { get; set; } = null!;
        public decimal M3p2x100 { get; set; }
        public int M3p3id { get; set; }
        public string M3p3nombre { get; set; } = null!;
        public decimal M3p3x100 { get; set; }
        public int M3p4id { get; set; }
        public string M3p4nombre { get; set; } = null!;
        public decimal M3p4x100 { get; set; }
        public int M3pig1Id { get; set; }
        public int M3pig2Id { get; set; }
        public string M3pig1Nombre { get; set; } = null!;
        public string M3pig2Nombre { get; set; } = null!;
        public decimal M3pig1X100 { get; set; }
        public decimal M3pig2X100 { get; set; }
        public bool Snext { get; set; }
        public bool Snimp { get; set; }
        public bool Snlam { get; set; }
        public bool Snpt { get; set; }
        public string ExtPigmento { get; set; } = null!;
        public string ExtUnidad { get; set; } = null!;
        public decimal ExtAncho1 { get; set; }
        public decimal ExtAncho2 { get; set; }
        public decimal ExtAncho3 { get; set; }
        public string ExtFormato { get; set; } = null!;
        public short ExtTratadoCaras { get; set; }
        public decimal ExtCalibre { get; set; }
        public decimal ExtPesoMetroL { get; set; }
        public string ExtIndicaciones { get; set; } = null!;
        public string ImpCyrelRoto { get; set; } = null!;
        public int ImpRodilloNro { get; set; }
        public int ImpNroPistas { get; set; }
        public string ImpTinta1 { get; set; } = null!;
        public string ImpTinta2 { get; set; } = null!;
        public string ImpTinta3 { get; set; } = null!;
        public string ImpTinta4 { get; set; } = null!;
        public string ImpTinta5 { get; set; } = null!;
        public string ImpTinta6 { get; set; } = null!;
        public string ImpTinta7 { get; set; } = null!;
        public string ImpTinta8 { get; set; } = null!;
        public string LamCapa1 { get; set; } = null!;
        public string LamCapa2 { get; set; } = null!;
        public string LamCapa3 { get; set; } = null!;
        public decimal LamCalibre1 { get; set; }
        public decimal LamCalibre2 { get; set; }
        public decimal LamCalibre3 { get; set; }
        public decimal LamCantidad1 { get; set; }
        public decimal LamCantidad2 { get; set; }
        public decimal LamCantidad3 { get; set; }
        public string PtFormato { get; set; } = null!;
        public decimal PtAncho { get; set; }
        public decimal PtLargo { get; set; }
        public decimal PtFuelle { get; set; }
        public string PtSellado { get; set; } = null!;
        public decimal PtMargenSeg { get; set; }
        public decimal PtPesoMillar { get; set; }
        public string PtPresentacion { get; set; } = null!;
        public decimal PtPesoRolloKg { get; set; }
        public int PtCantidadXpaquete { get; set; }
        public int PtCantidadXbulto { get; set; }
        public string ContabItem { get; set; } = null!;
        public bool Estado { get; set; }
        public string UsrCrea { get; set; } = null!;
        public DateTime FechaCrea { get; set; }
        public string UsrModifica { get; set; } = null!;
        public DateTime FechaModifica { get; set; }
    }
}
