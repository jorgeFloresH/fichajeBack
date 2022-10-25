using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class RequisitoTramite
    {
        public long IdRequitram { get; set; }
        public long? IdRequisitos { get; set; }
        public long? IdTramite { get; set; }

        public virtual Requisito? IdRequisitosNavigation { get; set; }
        public virtual Tramite? IdTramiteNavigation { get; set; }
    }
}
