using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ImprimirProgramacion
    {
        public string? Ot { get; set; }
        public string? Cliente { get; set; }
        public string? ClienteNom { get; set; }
        public string? Item { get; set; }
        public string? Referencia { get; set; }
        public decimal? Kilos { get; set; }
        public string? Maq { get; set; }
        public string? Fecha { get; set; }
        public string? Tipo { get; set; }
        public string? IdMaterial { get; set; }
        public string? Material { get; set; }
        public decimal? Solicitud { get; set; }
    }
}
