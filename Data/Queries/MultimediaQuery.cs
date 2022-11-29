using Microsoft.AspNetCore.Mvc;

namespace apiServices.Data.Queries
{
    public class MultimediaQuery
    {
        [FromQuery(Name = "nombreVideo")]
        public string? nombreVideo { get; set; }

        [FromQuery(Name = "sort")]
        public string? sort { get; set; }        
    }
}
