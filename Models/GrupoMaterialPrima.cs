using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class GrupoMaterialPrima
    {
        public int Id { get; set; }
        public string? CodigoMaterial { get; set; }
        public string NombreMaterial { get; set; } = null!;
    }
}
