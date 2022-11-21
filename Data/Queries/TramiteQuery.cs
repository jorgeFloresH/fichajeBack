using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace apiServices.Data.Queries
{
    public class TramiteQuery
    {
        [FromQuery(Name = "nombreTramite")]
        public string? nombreTramite { get; set; }

        [FromQuery(Name = "nombreAgencia")]
        public string? nombreAgencia { get; set; }

        [FromQuery(Name = "sort")]
        public string? sort { get; set; }
    }
}
