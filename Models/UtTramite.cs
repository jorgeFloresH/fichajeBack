using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class UtTramite
    {
        public long IdUtTramite { get; set; }
        public long? IdUsuario { get; set; }
        public long? IdTramite { get; set; }

        public virtual Tramite? IdTramiteNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
