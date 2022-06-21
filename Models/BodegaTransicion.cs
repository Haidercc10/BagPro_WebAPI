using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class BodegaTransicion
    {
        public int? Id { get; set; }
        public string? Bulto { get; set; }
        public string? Maq { get; set; }
        public decimal? Qty { get; set; }
        public decimal? Peeso { get; set; }
        public DateTime? FechaEntrada { get; set; }
        public string? Operario { get; set; }
        public string? NomReferencia { get; set; }
        public string? Codigo { get; set; }
        public string? Cliente { get; set; }
        public string? Estado { get; set; }
    }
}
