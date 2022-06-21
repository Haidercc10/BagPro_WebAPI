using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ProcImpresionTblImpresion2
    {
        public int Id { get; set; }
        public string Impresion { get; set; } = null!;
        public int Pista { get; set; }
        public string Ots { get; set; } = null!;
        public string CodItem { get; set; } = null!;
        public string Item { get; set; } = null!;
        public string CodCliente { get; set; } = null!;
        public string Cliente { get; set; } = null!;
        public string? Flexo { get; set; }
        public int? Rodillo { get; set; }
        public decimal Ancho { get; set; }
        public string? Tinta1 { get; set; }
        public string? Tinta2 { get; set; }
        public string? Tinta3 { get; set; }
        public string? Tinta4 { get; set; }
        public string? Tinta5 { get; set; }
        public string? Tinta6 { get; set; }
        public string? Tinta7 { get; set; }
        public string? Tinta8 { get; set; }
        public string UsrCrea { get; set; } = null!;
        public DateTime Fecha { get; set; }
        public int Maquina { get; set; }
    }
}
