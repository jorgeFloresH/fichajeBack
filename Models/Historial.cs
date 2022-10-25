using System;
using System.Collections.Generic;

namespace apiServices.Models
{
    public partial class Historial
    {
        public long IdHistorial { get; set; }
        public long? IdUsuario { get; set; }
        public DateTime? UltimoLogin { get; set; }
        public string? Password { get; set; }
        public int? EstadoContraseña { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public long? IdAgencia { get; set; }

        public virtual Usuario? IdUsuarioNavigation { get; set; }
    }
}
