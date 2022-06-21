using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ProcSellado
    {
        public int Item { get; set; }
        public string Ot { get; set; } = null!;
        public string CodCliente { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public string Referencia { get; set; } = null!;
        public string NomReferencia { get; set; } = null!;
        public string Cedula { get; set; } = null!;
        public string Operario { get; set; } = null!;
        public string? Cedula2 { get; set; }
        public string? Operario2 { get; set; }
        public string? Cedula3 { get; set; }
        public string? Operario3 { get; set; }
        public string? Cedula4 { get; set; }
        public string? Operario4 { get; set; }
        public decimal? Cedula5 { get; set; }
        public decimal? Operario5 { get; set; }
        public string Maquina { get; set; } = null!;
        public decimal Qty { get; set; }
        public decimal Peso { get; set; }
        public DateTime FechaEntrada { get; set; }
        public string Supervisor { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string? DatoEntrada { get; set; }
        public string? DatoSalida { get; set; }
        public string? FechaCambio { get; set; }
        public string? Unidad { get; set; }
        public string? Hora { get; set; }
        public string? EnvioZeus { get; set; }
        public string? Turnos { get; set; }
        public decimal? Pesot { get; set; }
        public decimal? Desv { get; set; }
        public decimal? PesoMillar { get; set; }
        public int? DivBulto { get; set; }
        public string? RemplazoItem { get; set; }
        public string? NomStatus { get; set; }
        public string? Ac { get; set; }
        public string? EstadoInventario { get; set; }
        public decimal? QtyDisponible { get; set; }
        public decimal? QtyNoDisponible { get; set; }
        public string? Facturado { get; set; }
        public DateTime? FechaRecibidoDespacho { get; set; }
        public DateTime? FechaFacturado { get; set; }
        public DateTime? FechaSalida { get; set; }
        public string? Observaciones { get; set; }
    }
}
