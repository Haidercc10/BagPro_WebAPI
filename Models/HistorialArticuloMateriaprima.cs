using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class HistorialArticuloMateriaprima
    {
        public int IdArticulo { get; set; }
        public string Codigo { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public decimal? Peso { get; set; }
        public decimal? PrecioVenta { get; set; }
        public decimal? CostoArticulo { get; set; }
        public string? Fuente { get; set; }
        public string? Nit { get; set; }
        public string? Proveedor { get; set; }
        public string? Doc { get; set; }
        public string? Micra { get; set; }
        public string? Ancho { get; set; }
        public string? Grupo { get; set; }
        public string? Tipo { get; set; }
        public string? Ot { get; set; }
        public string Fecha { get; set; } = null!;
    }
}
