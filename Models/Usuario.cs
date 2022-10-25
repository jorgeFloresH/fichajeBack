using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Contactos = new HashSet<Contacto>();
            Historials = new HashSet<Historial>();
            Tickets = new HashSet<Ticket>();
            UserTickets = new HashSet<UserTicket>();
            UtTramites = new HashSet<UtTramite>();
            VenUsuarios = new HashSet<VenUsuario>();
        }

        public long IdUsuario { get; set; }
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        public long? IdPerfil { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public long? IdAgencia { get; set; }
        public int? Estado { get; set; }
        public string? NomUsuario { get; set; }
        public string? ApePaterno { get; set; }
        public string? ApeMaterno { get; set; }
        public int? CiUsuario { get; set; }
        public string? FotoPerfil { get; set; }

        public virtual Agencium? IdAgenciaNavigation { get; set; }
        public virtual TipoPerfil? IdPerfilNavigation { get; set; }
        public virtual ICollection<Contacto> Contactos { get; set; }
        public virtual ICollection<Historial> Historials { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<UserTicket> UserTickets { get; set; }
        public virtual ICollection<UtTramite> UtTramites { get; set; }
        public virtual ICollection<VenUsuario> VenUsuarios { get; set; }
    }
}
