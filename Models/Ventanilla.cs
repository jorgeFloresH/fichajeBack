using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class Ventanilla
    {
        public Ventanilla()
        {
            Tickets = new HashSet<Ticket>();
            TraVens = new HashSet<TraVen>();
            VenUsuarios = new HashSet<VenUsuario>();
        }

        public long IdVentanilla { get; set; }
        public string? NomVentanilla { get; set; }
        public int? EstadoV { get; set; }
        public long? IdAgencia { get; set; }

        public virtual Agencium? IdAgenciaNavigation { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<TraVen> TraVens { get; set; }
        public virtual ICollection<VenUsuario> VenUsuarios { get; set; }
    }
}
