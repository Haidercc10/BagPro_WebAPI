using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ProcImpresionTblRollosImp
    {
        public int Id { get; set; }
        public string Impresion { get; set; } = null!;
        public int IdMat { get; set; }
        public string Tipo { get; set; } = null!;
        public string Material { get; set; } = null!;
        public decimal Calibre { get; set; }
        public decimal Ancho { get; set; }
        public decimal PesoNeto { get; set; }
        public decimal Tirilla { get; set; }
        public decimal? P1kg { get; set; }
        public decimal? Pista1 { get; set; }
        public decimal? P2kg { get; set; }
        public decimal? Pista2 { get; set; }
        public decimal? P3kg { get; set; }
        public decimal? Pista3 { get; set; }
        public decimal? P4kg { get; set; }
        public decimal? Pista4 { get; set; }
        public decimal? P5kg { get; set; }
        public decimal? Pista5 { get; set; }
        public string UsrCrea { get; set; } = null!;
        public DateTime Fecha { get; set; }
    }
}
