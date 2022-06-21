using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class NominaVendedor
    {
        public string? FechaNomina { get; set; }
        public string? Rc { get; set; }
        public string? Cliente { get; set; }
        public string? Factura { get; set; }
        public decimal Valorrc { get; set; }
        public decimal Base { get; set; }
        public decimal Nc { get; set; }
        public decimal Basenc { get; set; }
        public string? Fechafact { get; set; }
        public string? Fechapago { get; set; }
        public decimal Liq { get; set; }
        public decimal Comision { get; set; }
        public string? Dias { get; set; }
        public decimal Cartera { get; set; }
        public string? Mes { get; set; }
        public string? Periodo { get; set; }
        public string? Idvende { get; set; }
        public string? Vendedor { get; set; }
        public string? Sw { get; set; }
    }
}
