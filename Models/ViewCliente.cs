using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ViewCliente
    {
        public int Codigo { get; set; }
        public string? CodBagpro { get; set; }
        public string IdentTipo { get; set; } = null!;
        public string IdentNro { get; set; } = null!;
        public string NombreComercial { get; set; } = null!;
        public string UsrCrea { get; set; } = null!;
        public DateTime FechaCrea { get; set; }
        public string? UsrModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
        public string Estado { get; set; } = null!;
    }
}
