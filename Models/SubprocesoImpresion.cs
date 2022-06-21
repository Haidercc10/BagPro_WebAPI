using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class SubprocesoImpresion
    {
        public int Item { get; set; }
        public string Ot { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public string ClienteNombre { get; set; } = null!;
        public string ClienteItem { get; set; } = null!;
        public string ClienteItemNombre { get; set; } = null!;
        public decimal Extancho { get; set; }
        public decimal Extlargo { get; set; }
        public decimal Extfuelle { get; set; }
        public string Extunidad { get; set; } = null!;
        public decimal Exttotal { get; set; }
        public decimal ExtConoC { get; set; }
        public string ExtCono2 { get; set; } = null!;
        public decimal ExtTara { get; set; }
        public decimal ExtBruto { get; set; }
        public decimal Extnetokg { get; set; }
        public string Operador { get; set; } = null!;
        public string Estado { get; set; } = null!;
        public string? Material { get; set; }
        public decimal? Calibre { get; set; }
        public int Maquina { get; set; }
        public string Tipo { get; set; } = null!;
        public DateTime? Fecha { get; set; }
    }
}
