using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class InvBopp
    {
        public int Item { get; set; }
        public string Sku { get; set; } = null!;
        public string Calibre { get; set; } = null!;
        public decimal Ancho { get; set; }
        public decimal Kilos { get; set; }
        public string? Fecha { get; set; }
        public string? Hora { get; set; }
        public int? Status { get; set; }
        public string? SkuPadre { get; set; }
        public decimal? AncPadre { get; set; }
        public decimal? PesoPadre { get; set; }
        public string? Ot { get; set; }
    }
}
