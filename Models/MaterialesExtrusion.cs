using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class MaterialesExtrusion
    {
        public int Id { get; set; }
        public int Cod { get; set; }
        public string Material { get; set; } = null!;
        public string MatAsignado { get; set; } = null!;
    }
}
