using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class TraVen
    {
        public long IdTranVen { get; set; }
        public long? IdTramite { get; set; }
        public long? IdVentanilla { get; set; }

        public virtual Tramite? IdTramiteNavigation { get; set; }
        public virtual Ventanilla? IdVentanillaNavigation { get; set; }
    }
}
