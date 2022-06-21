using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Vendedore
    {
        public string Id { get; set; } = null!;
        public string NombreCompleto { get; set; } = null!;
        public string IdBagpro { get; set; } = null!;
        public string? Permiso { get; set; }
    }
}
