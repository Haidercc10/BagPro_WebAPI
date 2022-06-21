using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Cartera
    {
        public string? NombreCliente { get; set; }
        public string? IdNroCru { get; set; }
        public string? IdFecha { get; set; }
        public string? LapsoDoc { get; set; }
        public string? IdFechaVcto { get; set; }
        public string? Descripcion { get; set; }
        public string? IdVendedor { get; set; }
        public string? IdTipoCru { get; set; }
        public string? IdDiasVcto { get; set; }
        public string? SaldosTotCartera { get; set; }
        public string? FechaGen { get; set; }
        public string? IdRango { get; set; }
        public string? Direccion1 { get; set; }
        public string? Telefono1 { get; set; }
        public string? IdTerc { get; set; }
    }
}
