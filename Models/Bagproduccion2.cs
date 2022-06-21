using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Bagproduccion2
    {
        public string? Ot { get; set; }
        public string? Rollo { get; set; }
        public string? Operador { get; set; }
        public string? Cliente { get; set; }
        public string? ClienteNombre { get; set; }
        public string? ClienteItem { get; set; }
        public string? ClienteItemNombre { get; set; }
        public string? Material { get; set; }
        public decimal? Extancho { get; set; }
        public decimal? Extlargo { get; set; }
        public decimal? Extfuelle { get; set; }
        public decimal? Calibre { get; set; }
        public decimal? Extnetokg { get; set; }
        public int? Maquina { get; set; }
        public DateTime? Fecha { get; set; }
        public string? NomStatus { get; set; }
        public string? Hora { get; set; }
    }
}
