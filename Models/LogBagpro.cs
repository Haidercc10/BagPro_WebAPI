using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class LogBagpro
    {
        public string? Proceso { get; set; }
        public string? Metodo { get; set; }
        public string? Bulto { get; set; }
        public string? Cliente { get; set; }
        public string? Ot { get; set; }
        public string? Item { get; set; }
        public string? Fecha { get; set; }
        public string? Hora { get; set; }
        public string? Observacion { get; set; }
        public string? Operario { get; set; }
    }
}
