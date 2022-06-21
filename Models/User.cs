using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string UsrCrea { get; set; } = null!;
        public DateTime? FechaCrea { get; set; }
        public string? UsrModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
        public string? CampoZeus { get; set; }
        public string? Reportes { get; set; }
        public string? Nomina { get; set; }
    }
}
