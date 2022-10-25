using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class PantallaMul
    {
        public long IdPantallaMul { get; set; }
        public long? IdMulti { get; set; }
        public long? IdTipo { get; set; }
        public long? IdAgencia { get; set; }

        public virtual Agencium? IdAgenciaNavigation { get; set; }
        public virtual Multimedium? IdMultiNavigation { get; set; }
        public virtual TipoPantalla? IdTipoNavigation { get; set; }
    }
}
