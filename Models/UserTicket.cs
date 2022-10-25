using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class UserTicket
    {
        public long IdUserTicket { get; set; }
        public long? IdTicket { get; set; }
        public long? IdUsuario { get; set; }

        public virtual Ticket? IdTicketNavigation { get; set; }
        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
