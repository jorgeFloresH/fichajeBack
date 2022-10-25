using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class Agencium
    {
        public Agencium()
        {
            MultimediaNavigation = new HashSet<Multimedium>();
            PantallaMuls = new HashSet<PantallaMul>();
            Tickets = new HashSet<Ticket>();
            Tramites = new HashSet<Tramite>();
            Usuarios = new HashSet<Usuario>();
            Ventanillas = new HashSet<Ventanilla>();
        }

        public long IdAgencia { get; set; }
        public string? NomAgencia { get; set; }
        public int? Estado { get; set; }
        public int? Acdes { get; set; }
        public int? Mapa { get; set; }
        public int? Multimedia { get; set; }
        public int? Consulta { get; set; }

        public virtual ICollection<Multimedium> MultimediaNavigation { get; set; }
        public virtual ICollection<PantallaMul> PantallaMuls { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<Tramite> Tramites { get; set; }
        public virtual ICollection<Usuario> Usuarios { get; set; }
        public virtual ICollection<Ventanilla> Ventanillas { get; set; }
    }
}
