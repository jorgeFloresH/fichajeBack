using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class Contacto
    {
        public long IdContacto { get; set; }
        public string? Correo { get; set; }
        public int? NCelular { get; set; }
        public long? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
