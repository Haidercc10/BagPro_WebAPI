using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Riquisicion
    {
        public int Id { get; set; }
        public int Ot { get; set; }
        public int Item { get; set; }
        public decimal TotalOt { get; set; }
        public int CodCliente { get; set; }
        public string Cliente { get; set; } = null!;
        public int CodItem { get; set; }
        public string ItemCliente { get; set; } = null!;
        public decimal OtPedidoMp { get; set; }
        public int OtMaquinaMp { get; set; }
        public int? Capa1 { get; set; }
        public decimal? Cap1t1 { get; set; }
        public int? Mezprod1cap1 { get; set; }
        public string? Cap1t2 { get; set; }
        public decimal? Cap1porc1 { get; set; }
        public decimal? Cap1pedir1 { get; set; }
        public decimal? Cap1pedido1 { get; set; }
        public int? Mezprod2cap1 { get; set; }
        public string? Cap1t3 { get; set; }
        public decimal? Cap1porc2 { get; set; }
        public decimal? Cap1pedir2 { get; set; }
        public decimal? Cap1pedido2 { get; set; }
        public int? Mezprod3cap1 { get; set; }
        public string? Cap1t4 { get; set; }
        public decimal? Cap1porc3 { get; set; }
        public decimal? Cap1pedir3 { get; set; }
        public decimal? Cap1pedido3 { get; set; }
        public int? Mezprod4cap1 { get; set; }
        public string? Cap1t5 { get; set; }
        public decimal? Cap1porc4 { get; set; }
        public decimal? Cap1pedir4 { get; set; }
        public decimal? Cap1pedido4 { get; set; }
        public int? Mezpiqm1cap1 { get; set; }
        public string? Cap1t6 { get; set; }
        public decimal? Cap1porc5 { get; set; }
        public decimal? Cap1pedir5 { get; set; }
        public decimal? Cap1pedido5 { get; set; }
        public int? Mezpiqm2cap1 { get; set; }
        public string? Cap1t7 { get; set; }
        public decimal? Cap1porc6 { get; set; }
        public decimal? Cap1pedir6 { get; set; }
        public decimal? Cap1pedido6 { get; set; }
        public int? Capa2 { get; set; }
        public decimal? Cap2t1 { get; set; }
        public int? Mezprod1cap2 { get; set; }
        public string? Cap2t2 { get; set; }
        public decimal? Cap2porc1 { get; set; }
        public decimal? Cap2pedir1 { get; set; }
        public decimal? Cap2pedido1 { get; set; }
        public int? Mezprod2cap2 { get; set; }
        public string? Cap2t3 { get; set; }
        public decimal? Cap2porc2 { get; set; }
        public decimal? Cap2pedir2 { get; set; }
        public decimal? Cap2pedido2 { get; set; }
        public int? Mezprod3cap2 { get; set; }
        public string? Cap2t4 { get; set; }
        public decimal? Cap2porc3 { get; set; }
        public decimal? Cap2pedir3 { get; set; }
        public decimal? Cap2pedido3 { get; set; }
        public int? Mezprod4cap2 { get; set; }
        public string? Cap2t5 { get; set; }
        public decimal? Cap2porc4 { get; set; }
        public decimal? Cap2pedir4 { get; set; }
        public decimal? Cap2pedido4 { get; set; }
        public int? Mezpiqm1cap2 { get; set; }
        public string? Cap2t6 { get; set; }
        public decimal? Cap2porc5 { get; set; }
        public decimal? Cap2pedir5 { get; set; }
        public decimal? Cap2pedido5 { get; set; }
        public int? Mezpiqm2cap2 { get; set; }
        public string? Cap2t7 { get; set; }
        public decimal? Cap2porc6 { get; set; }
        public decimal? Cap2pedir6 { get; set; }
        public decimal? Cap2pedido6 { get; set; }
        public int? Capa3 { get; set; }
        public decimal? Cap3t1 { get; set; }
        public int? Mezprod1cap3 { get; set; }
        public string? Cap3t2 { get; set; }
        public decimal? Cap3porc1 { get; set; }
        public decimal? Cap3pedir1 { get; set; }
        public decimal? Cap3pedido1 { get; set; }
        public int? Mezprod2cap3 { get; set; }
        public string? Cap3t3 { get; set; }
        public decimal? Cap3porc2 { get; set; }
        public decimal? Cap3pedir2 { get; set; }
        public decimal? Cap3pedido2 { get; set; }
        public int? Mezprod3cap3 { get; set; }
        public string? Cap3t4 { get; set; }
        public decimal? Cap3porc3 { get; set; }
        public decimal? Cap3pedir3 { get; set; }
        public decimal? Cap3pedido3 { get; set; }
        public int? Mezprod4cap3 { get; set; }
        public string? Cap3t5 { get; set; }
        public decimal? Cap3porc4 { get; set; }
        public decimal? Cap3pedir4 { get; set; }
        public decimal? Cap3pedido4 { get; set; }
        public int? Mezpiqm1cap3 { get; set; }
        public string? Cap3t6 { get; set; }
        public decimal? Cap3porc5 { get; set; }
        public decimal? Cap3pedir5 { get; set; }
        public decimal? Cap3pedido5 { get; set; }
        public int? Mezpiqm2cap3 { get; set; }
        public string? Cap3t7 { get; set; }
        public decimal? Cap3porc6 { get; set; }
        public decimal? Cap3pedir6 { get; set; }
        public decimal? Cap3pedido6 { get; set; }
        public string? Estado { get; set; }
        public int? CodEstado { get; set; }
        public string UsrCrea { get; set; } = null!;
        public DateTime? FechaCrea { get; set; }
        public string? UsrModifica { get; set; }
        public DateTime? FechaModifica { get; set; }
    }
}
