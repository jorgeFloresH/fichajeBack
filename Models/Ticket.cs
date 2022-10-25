using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class Ticket
    {
        public Ticket()
        {
            HistorialDerivacions = new HashSet<HistorialDerivacion>();
            UserTickets = new HashSet<UserTicket>();
        }

        public long IdTicket { get; set; }
        public long? IdTramite { get; set; }
        public long? IdUsuario { get; set; }
        public int? Estado { get; set; }
        public long? IdVentanilla { get; set; }
        public DateTime? FechaHora { get; set; }
        public TimeOnly? HoraAtencion { get; set; }
        public int? DuracionAtencion { get; set; }
        public long? IdAgencia { get; set; }
        public int? NTicket { get; set; }
        public long? IdPrioridad { get; set; }
        public int? Derivacion { get; set; }
        public TimeOnly? HoraList { get; set; }

        public virtual Agencium? IdAgenciaNavigation { get; set; }
        public virtual Prioridad? IdPrioridadNavigation { get; set; }
        public virtual Tramite? IdTramiteNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual Ventanilla? IdVentanillaNavigation { get; set; }
        public virtual ICollection<HistorialDerivacion> HistorialDerivacions { get; set; }
        public virtual ICollection<UserTicket> UserTickets { get; set; }
    }
}
