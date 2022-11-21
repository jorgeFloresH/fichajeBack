using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace apiServices.Data.Queries
{
    public class RequisitosQuery
    {
        [FromQuery(Name = "nombre")]
        public string? nombre { get; set; }

        [FromQuery(Name = "sort")]
        public string? sort { get; set; }
    }
}
