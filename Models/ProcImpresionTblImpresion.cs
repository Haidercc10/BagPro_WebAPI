using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ProcImpresionTblImpresion
    {
        public int Id { get; set; }
        public string Impresion { get; set; } = null!;
        public int Pista { get; set; }
        public string Ots1 { get; set; } = null!;
        public string? Ots2 { get; set; }
        public string? Ots3 { get; set; }
        public string? Ots4 { get; set; }
        public string? Ots5 { get; set; }
        public string? CodItem { get; set; }
        public string? Item { get; set; }
        public string? CodCliente { get; set; }
        public string? Cliente { get; set; }
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
        public string UsrCrea { get; set; } = null!;
        public decimal Ancho { get; set; }
        public decimal AnchoMed { get; set; }
        public DateTime Fecha { get; set; }
        public decimal? DatosOtKg { get; set; }
        public int? Maquina { get; set; }
    }
}
