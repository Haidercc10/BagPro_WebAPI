using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ImpresionTintum
    {
        public int Id { get; set; }
        public string NombreMaterial { get; set; } = null!;
        public string? Referencia { get; set; }
        public string UsrCrea { get; set; } = null!;
        public DateTime FechaCrea { get; set; }
        public string UsrModifica { get; set; } = null!;
        public DateTime FechaModifica { get; set; }
    }
}
