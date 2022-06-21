using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Cargue
    {
        public int Rollo { get; set; }
        public string Ot { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public string ClienteNombre { get; set; } = null!;
        public string ClienteItem { get; set; } = null!;
        public string ClienteItemNombre { get; set; } = null!;
        public decimal Extancho { get; set; }
        public string ExtCono2 { get; set; } = null!;
        public decimal Extnetokg { get; set; }
        public string Estado { get; set; } = null!;
        public DateTime? Fecha { get; set; }
    }
}
