using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class OperariosProceso
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Planta { get; set; }
        public string? Cedula { get; set; }
    }
}
