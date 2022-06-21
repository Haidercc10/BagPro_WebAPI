using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class OperarioImpresion
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string Tipo { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Maquina { get; set; }
    }
}
