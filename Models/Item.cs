using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class Item
    {
        public int Id { get; set; }
        public string CodigoCliente { get; set; } = null!;
        public int CodigoItem { get; set; }
        public string DespItem { get; set; } = null!;
        public string? UsrCreaItem { get; set; }
        public DateTime? FechaCreaItem { get; set; }
        public string? UsrModificaItem { get; set; }
        public DateTime? FechaModificaItem { get; set; }
    }
}
