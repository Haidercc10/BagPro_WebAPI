using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Permiso
    {
        public int? Usuario { get; set; }
        public int? Cod { get; set; }
        public string? Descripcion { get; set; }
        public int? Permiso1 { get; set; }
        public string? Operario { get; set; }
    }
}
