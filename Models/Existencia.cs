using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Existencia
    {
        public int Id { get; set; }
        public string Item { get; set; } = null!;
        public decimal Qty { get; set; }
        public decimal? Peso { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string? Proceso { get; set; }
    }
}
