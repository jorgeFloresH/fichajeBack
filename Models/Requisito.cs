using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class Requisito
    {
        public Requisito()
        {
            RequisitoTramites = new HashSet<RequisitoTramite>();
        }

        public long IdRequisitos { get; set; }
        public string? NomRequisitos { get; set; }
        public int? Estado { get; set; }

        public virtual ICollection<RequisitoTramite> RequisitoTramites { get; set; }
    }
}
