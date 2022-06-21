using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ProcdesperdicioDetalle
    {
        public int Cons { get; set; }
        public string? Turno { get; set; }
        public int Item { get; set; }
        public string Ot { get; set; } = null!;
        public string ClienteNombre { get; set; } = null!;
        public string ClienteItemNombre { get; set; } = null!;
        public decimal Extancho { get; set; }
        public decimal Extunidad { get; set; }
        public decimal Extnetokg { get; set; }
        public int Tipo { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Material { get; set; }
        public decimal? Calibre { get; set; }
        public int Maquina { get; set; }
        public decimal Exttotalrollos { get; set; }
    }
}
