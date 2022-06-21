using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Procdesperdicio
    {
        public int Item { get; set; }
        public string Ot { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public string ClienteNombre { get; set; } = null!;
        public string ClienteItem { get; set; } = null!;
        public string ClienteItemNombre { get; set; } = null!;
        public decimal Exttotalextruir { get; set; }
        public decimal Exttotalrollos { get; set; }
        public decimal Exttotaldesp { get; set; }
        public decimal ExtTara { get; set; }
        public decimal ExtBruto { get; set; }
        public decimal Extnetokg { get; set; }
        public string Operador { get; set; } = null!;
        public string? Material { get; set; }
        public int Maquina { get; set; }
        public string? TurnoD { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Hora { get; set; }
        public string? NomStatus { get; set; }
    }
}
