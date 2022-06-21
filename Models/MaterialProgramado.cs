using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class MaterialProgramado
    {
        public int Id { get; set; }
        public string Ot { get; set; } = null!;
        public string Item { get; set; } = null!;
        public string Material { get; set; } = null!;
        public decimal Solicitud { get; set; }
    }
}
