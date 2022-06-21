using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Horario
    {
        public int Id { get; set; }
        public string Turno { get; set; } = null!;
        public string Hora { get; set; } = null!;
        public string Minuto { get; set; } = null!;
        public string Hora2 { get; set; } = null!;
        public string Minuto2 { get; set; } = null!;
        public string Usuario { get; set; } = null!;
        public DateTime? Fecha { get; set; }
        public string? Activo { get; set; }
        public int Item { get; set; }
        public string? Proceso { get; set; }
        public string? Procesoplanta { get; set; }
        public string? HoraI { get; set; }
        public string? HoraF { get; set; }
    }
}
