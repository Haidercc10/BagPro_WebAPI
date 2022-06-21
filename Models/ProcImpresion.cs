using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ProcImpresion
    {
        public int Id { get; set; }
        public int? Pistas { get; set; }
        public string? Ots1 { get; set; }
        public string? Ots2 { get; set; }
        public string? Ots3 { get; set; }
        public string? Ots4 { get; set; }
        public string? Ots5 { get; set; }
        public int? Maquina { get; set; }
        public decimal? Ancho { get; set; }
        public decimal? Peso { get; set; }
        public decimal? Tirilla { get; set; }
        public string? UsrCrea { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Estado { get; set; }
    }
}
