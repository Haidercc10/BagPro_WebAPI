using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ProcCorte
    {
        public int Item { get; set; }
        public string Rollo { get; set; } = null!;
        public string Material { get; set; } = null!;
        public string Referencia { get; set; } = null!;
        public decimal Peso { get; set; }
        public decimal Extancho { get; set; }
        public string Corte { get; set; } = null!;
        public string Sobrante { get; set; } = null!;
        public string Cono { get; set; } = null!;
        public int? Status { get; set; }
        public string? NomStatus { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Hora { get; set; }
        public decimal? Calibre { get; set; }
    }
}
