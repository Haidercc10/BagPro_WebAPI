using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Material
    {
        public int Id { get; set; }
        public string? NombreMaterial { get; set; }
        public string UsrCrea { get; set; } = null!;
        public DateTime? FechaCrea { get; set; }
        public string? UsrModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
    }
}
