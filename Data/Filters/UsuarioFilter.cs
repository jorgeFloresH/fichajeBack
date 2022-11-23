using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace apiServices.Data.Filters
{
    public class UsuarioFilter
    {
        public string nombreCompleto { get; set; }

        public string ci { get; set; }

        public string nombreUsuario { get; set; }

        public string cargo { get; set; }

        public string agencia { get; set; }

        public string sort { get; set; }
        public int idAgencia { get; set; } 
    }
}
