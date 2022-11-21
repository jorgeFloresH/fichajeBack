using Microsoft.AspNetCore.Identity;

namespace apiServices.Data.Responses
{
    public class UserResponse
    {
        public long idUsuario { get; set; }
        public string? userName { get; set; }
        public string? userPassword { get; set; }
        public long? idPerfil { get; set; }
        public DateTime? fechaCreacion { get; set; }
        public int? estado { get; set; }
        public long? idAgencia { get; set; }
        public string? nomAgencia { get; set; }
        public string? nomUsuario { get; set; }
        public string? apePaterno { get; set; }
        public string? apeMaterno { get; set; }
        public int? ciUsuario { get; set; }
        public string? nomTipoP { get; set; }  
        public int? mapa { get; set; }
        public int? multimedia { get; set; }
        public int? consulta { get; set; }
    }
}
