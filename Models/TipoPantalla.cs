using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class TipoPantalla
    {
        public TipoPantalla()
        {
            PantallaMuls = new HashSet<PantallaMul>();
        }

        public long IdTipo { get; set; }
        public string? NomTipo { get; set; }
        public long? IdAgencia { get; set; }

        public virtual ICollection<PantallaMul> PantallaMuls { get; set; }
    }
}
