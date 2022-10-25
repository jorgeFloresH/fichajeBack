using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class Multimedium
    {
        public Multimedium()
        {
            PantallaMuls = new HashSet<PantallaMul>();
        }

        public long IdMulti { get; set; }
        public string? NomVideo { get; set; }
        public int? Estado { get; set; }
        public string? Tipo { get; set; }
        public string? Ruta { get; set; }
        public long? IdAgencia { get; set; }

        public virtual Agencium? IdAgenciaNavigation { get; set; }
        public virtual ICollection<PantallaMul> PantallaMuls { get; set; }
    }
}
