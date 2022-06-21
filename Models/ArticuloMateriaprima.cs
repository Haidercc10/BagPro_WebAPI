using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class ArticuloMateriaprima
    {
        public string Codigo { get; set; } = null!;
        public string? Nombre { get; set; }
        public string? Descripcion { get; set; }
        public string Grupo { get; set; } = null!;
        public string? Tipo { get; set; }
        public string? Presentacion { get; set; }
        public decimal? Peso { get; set; }
        public decimal? PorcentajeIva { get; set; }
        public decimal PrecioVenta { get; set; }
        public string? Cuentaiva { get; set; }
        public string? CuentaIvaventas { get; set; }
        public string? DescripcionOtroIdioma { get; set; }
        public decimal? CostoArticulo { get; set; }
    }
}
