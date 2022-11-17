using Microsoft.AspNetCore.Mvc;

namespace apiServices.Data.Queries
{
    public class AgenciaQuery
    {
        [FromQuery(Name = "nombre")]
        public string? nombre { get; set; }

        [FromQuery(Name = "sort")]
        public string? sort { get; set; }
    }
}
