using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ProvedoresMateriaprima
    {
        public int Id { get; set; }
        public string Nit { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Grupo { get; set; }
    }
}
