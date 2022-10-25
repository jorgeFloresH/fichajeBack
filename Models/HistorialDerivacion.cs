using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class HistorialDerivacion
    {
        public long IdDerivacion { get; set; }
        public long? IdTicket { get; set; }
        public long? VentanillaOrigen { get; set; }
        public long? VentanillaDestino { get; set; }

        public virtual Ticket? IdTicketNavigation { get; set; }
    }
}
