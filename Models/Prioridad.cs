using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class Prioridad
    {
        public Prioridad()
        {
            Tickets = new HashSet<Ticket>();
        }

        public long IdPrioridad { get; set; }
        public string? Tipo { get; set; }
        public int? Rango { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
    }
}
