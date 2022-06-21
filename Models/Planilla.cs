using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Planilla
    {
        public int Item { get; set; }
        public string? Referencia { get; set; }
        public int? Inventario { get; set; }
    }
}
