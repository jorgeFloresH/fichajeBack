using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace apiServices.Data.Queries
{
    public class TicketQuery
    {
        [FromQuery(Name = "nombreAgencia")]
        public string? nombreAgencia { get; set; }

        [FromQuery(Name = "nombreTramite")]
        public string? nombreTramite { get; set; }

        [FromQuery(Name = "nombreVentanilla")]
        public string? nombreVentanilla { get; set; }

        [FromQuery(Name = "tipoCliente")]
        public string? tipoCliente { get; set; }

        [FromQuery(Name = "fecha")]
        public string? fecha { get; set; }

        [FromQuery(Name = "sort")]
        public string? sort { get; set; }
    }
}
