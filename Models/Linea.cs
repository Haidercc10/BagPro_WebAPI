using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Linea
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
