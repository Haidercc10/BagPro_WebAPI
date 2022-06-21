using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ConosCalibre
    {
        public int Id { get; set; }
        public string Referencia { get; set; } = null!;
        public decimal KgCmAncho { get; set; }
    }
}
