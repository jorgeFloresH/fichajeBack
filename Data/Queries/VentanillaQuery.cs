using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace apiServices.Data.Queries
{
    public class VentanillaQuery
    {
        [FromQuery(Name = "nombreVentanilla")]
        public string? nombreVentanilla { get; set; }

        [FromQuery(Name = "nombreAgencia")]
        public string? nombreAgencia { get; set; }

        [FromQuery(Name = "sort")]
        public string? sort { get; set; }
    }
}
