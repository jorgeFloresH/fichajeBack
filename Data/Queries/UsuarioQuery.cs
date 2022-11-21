using Microsoft.AspNetCore.Mvc;

namespace apiServices.Data.Queries
{
    public class UsuarioQuery
    {
        [FromQuery(Name = "nombreCompleto")]
        public string? nombreCompleto { get; set; }
        
        [FromQuery(Name = "ci")]
        public string? ci { get; set; }
        
        [FromQuery(Name = "nombreUsuario")]
        public string? nombreUsuario { get; set; }
        
        [FromQuery(Name = "cargo")]
        public string? cargo { get; set; }
        
        [FromQuery(Name = "agencia")]
        public string? agencia { get; set; }
        
        [FromQuery(Name = "sort")]
        public string? sort { get; set; }
    }
}
