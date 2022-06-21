using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ProcExtrusion
    {
        public int Item { get; set; }
        public string Ot { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public string ClienteNombre { get; set; } = null!;
        public string ClienteItem { get; set; } = null!;
        public string ClienteItemNombre { get; set; } = null!;
        public decimal Extancho { get; set; }
        public decimal Extlargo { get; set; }
        public decimal Extfuelle { get; set; }
        public string Extunidad { get; set; } = null!;
        public decimal Exttotal { get; set; }
        public decimal Exttotalextruir { get; set; }
        public decimal Exttotalrollos { get; set; }
        public decimal Exttotaldesp { get; set; }
        public decimal Extrend { get; set; }
        public decimal ExtConoC { get; set; }
        public string ExtCono2 { get; set; } = null!;
        public decimal ExtTara { get; set; }
        public decimal ExtBruto { get; set; }
        public decimal Extnetokg { get; set; }
        public string Operador { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string? Material { get; set; }
        public decimal? Calibre { get; set; }
        public int Maquina { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Status { get; set; }
        public string? NomStatus { get; set; }
        public string? Turno { get; set; }
        public string? Hora { get; set; }
        public string? Observacion { get; set; }
        public string? EnvioZeus { get; set; }
        public string? TipoDesperdicio { get; set; }
        public string? TipoSustancias { get; set; }
        public string? Ac { get; set; }
        public string? EstadoInventario { get; set; }
        public decimal? QtyNoDisponible { get; set; }
        public decimal? QtyDisponible { get; set; }
        public string? Facturado { get; set; }
        public DateTime? FechaRecibidoDespacho { get; set; }
        public DateTime? FechaFacturado { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string? Observaciones { get; set; }
    }
}
