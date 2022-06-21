using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ProcImpresionRollosBopp
    {
        public int Id { get; set; }
        public string OtImp { get; set; } = null!;
        public string? Tipo { get; set; }
        public int Maq { get; set; }
        public string? Ot { get; set; }
        public string? CodCliente { get; set; }
        public string? Cliente { get; set; }
        public string? CodItem { get; set; }
        public string? Item { get; set; }
        public decimal? Adj { get; set; }
        public decimal? Ancho { get; set; }
        public decimal? Kls { get; set; }
        public string? Flexo { get; set; }
        public decimal? Rodillo { get; set; }
        public string? Tinta1 { get; set; }
        public string? Tinta2 { get; set; }
        public string? Tinta3 { get; set; }
        public string? Tinta4 { get; set; }
        public string? Tinta5 { get; set; }
        public string? Tinta6 { get; set; }
        public string? Tinta7 { get; set; }
        public string? Tinta8 { get; set; }
        public string Rollos { get; set; } = null!;
        public decimal? RCalibre { get; set; }
        public decimal? RAncho { get; set; }
        public decimal? RPeso { get; set; }
        public string Estado { get; set; } = null!;
        public decimal? SAncho { get; set; }
        public decimal? STirilla { get; set; }
        public decimal? Tirilla { get; set; }
        public DateTime Fecha { get; set; }
    }
}
