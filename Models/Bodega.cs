using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Bodega
    {
        public int Id { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Responsable { get; set; }
        public string? Ubicacion { get; set; }
        public string? Telefono { get; set; }
    }
}
