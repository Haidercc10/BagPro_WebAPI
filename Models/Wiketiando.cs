using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BagproWebAPI.Models
{
    public partial class Wiketiando
    {
        [Key]
        public string Id { get; set; } = null!;
        public decimal? Dia { get; set; }
        public decimal? Noche { get; set; }
        public int? Mq { get; set; }
        public string? Codigo { get; set; }
    }
}
