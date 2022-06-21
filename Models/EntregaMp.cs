using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class EntregaMp
    {
        public int Id { get; set; }
        public int Item { get; set; }
        public int Req { get; set; }
        public int Ot { get; set; }
        public string Cliente { get; set; } = null!;
        public string ItemCliente { get; set; } = null!;
        public int? CodMaterial { get; set; }
        public string Material { get; set; } = null!;
        public decimal QtySolicitada { get; set; }
        public decimal QtyEntregada { get; set; }
        public string? Estado { get; set; }
        public int? CodEstado { get; set; }
        public string UsrCrea { get; set; } = null!;
        public DateTime? FechaCrea { get; set; }
        public string? UsrModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
    }
}
