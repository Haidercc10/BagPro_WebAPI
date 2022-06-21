using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Tratado
    {
        public int Id { get; set; }
        public string NombreTratado { get; set; } = null!;
        public string UsrCrea { get; set; } = null!;
        public DateTime? FechaCrea { get; set; }
        public string? UsrModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
    }
}
