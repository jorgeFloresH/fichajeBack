using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class DetalleObservacion
    {
        public long IdDetalleObs { get; set; }
        public long? IdObservaciones { get; set; }
        public long? IdTramite { get; set; }

        public virtual Observacione? IdObservacionesNavigation { get; set; }
        public virtual Tramite? IdTramiteNavigation { get; set; }
    }
}
