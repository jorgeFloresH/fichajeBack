using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class Tramite
    {
        public Tramite()
        {
            DetalleObservacions = new HashSet<DetalleObservacion>();
            RequisitoTramites = new HashSet<RequisitoTramite>();
            Tickets = new HashSet<Ticket>();
            TraVens = new HashSet<TraVen>();
            UtTramites = new HashSet<UtTramite>();
        }

        public long IdTramite { get; set; }
        public long? IdAgencia { get; set; }
        public string? NomTramite { get; set; }
        public int? Estado { get; set; }

        public virtual Agencium? IdAgenciaNavigation { get; set; }
        public virtual ICollection<DetalleObservacion> DetalleObservacions { get; set; }
        public virtual ICollection<RequisitoTramite> RequisitoTramites { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<TraVen> TraVens { get; set; }
        public virtual ICollection<UtTramite> UtTramites { get; set; }
    }
}
