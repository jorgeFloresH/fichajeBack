using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class VenUsuario
    {
        public long IdVenUsuario { get; set; }
        public long? IdVentanilla { get; set; }
        public long? IdUsuario { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
        public virtual Ventanilla? IdVentanillaNavigation { get; set; }
    }
}
