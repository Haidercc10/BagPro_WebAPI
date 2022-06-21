using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Inventario
    {
        public int Id { get; set; }
        public string Tipo { get; set; } = null!;
        public string Material { get; set; } = null!;
        public decimal? CalibreMicras { get; set; }
        public decimal? Anchomm { get; set; }
        public string? Referencia { get; set; }
        public decimal? Pesoneto { get; set; }
        public string? Asignado { get; set; }
        public string? UsrCrea { get; set; }
        public DateTime? Fecha { get; set; }
    }
}
