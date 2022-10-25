using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class TipoPerfil
    {
        public TipoPerfil()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public long IdPerfil { get; set; }
        public string? NomTipoP { get; set; }
        public int? Estado { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
