using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class InvMp
    {
        public int Codigo { get; set; }
        public int Item { get; set; }
        public string CodMaterialMp1 { get; set; } = null!;
        public string MaterialMp1 { get; set; } = null!;
        public decimal QtyInv { get; set; }
        public string? Asignado { get; set; }
        public decimal? QtySal { get; set; }
        public string UsrCrea { get; set; } = null!;
        public DateTime? FechaCrea { get; set; }
        public string? UsrModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
    }
}
