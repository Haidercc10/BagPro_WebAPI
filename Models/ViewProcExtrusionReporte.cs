using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ViewProcExtrusionReporte
    {
        public int Item { get; set; }
        public string Ot { get; set; } = null!;
        public string Referencia { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public int Maquina { get; set; }
        public decimal Qty { get; set; }
        public DateTime? FechaEntrada { get; set; }
        public string? Hora { get; set; }
        public decimal Peso { get; set; }
        public string? EstadoInventario { get; set; }
        public decimal? QtyNoDisponible { get; set; }
        public decimal? QtyDisponible { get; set; }
    }
}
