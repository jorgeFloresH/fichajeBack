using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class Observacione
    {
        public Observacione()
        {
            DetalleObservacions = new HashSet<DetalleObservacion>();
        }

        public long IdObservaciones { get; set; }
        public string? NomObservaciones { get; set; }
        public int? Estado { get; set; }

        public virtual ICollection<DetalleObservacion> DetalleObservacions { get; set; }
    }
}
