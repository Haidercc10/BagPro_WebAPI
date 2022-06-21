using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class HistorialPrima
    {
        public int Id { get; set; }
        public string Fact { get; set; } = null!;
        public string? Req { get; set; }
        public string? IdProveedor { get; set; }
        public string? Proveedores { get; set; }
        public decimal? Compra { get; set; }
        public decimal? Valor { get; set; }
        public string? Item { get; set; }
        public string? Material { get; set; }
        public string? Tipo { get; set; }
    }
}
