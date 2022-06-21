using System;
using System.Collections.Generic;

namespace BagproWebAPI.Models
{
    public partial class MaterialMp
    {
        public int Codigo { get; set; }
        public string CodGrupo { get; set; } = null!;
        public string Grupo { get; set; } = null!;
        public string ItemGrupo { get; set; } = null!;
        public string ItemDescripcion { get; set; } = null!;
    }
}
