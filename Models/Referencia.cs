using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Referencia
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
    }
}
