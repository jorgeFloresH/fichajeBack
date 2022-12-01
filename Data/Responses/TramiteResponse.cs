namespace apiServices.Data.Responses
{
    public class TramiteResponse
    {
        public long idTramite { get; set; }
        public long? idAgencia { get; set; }
        public string? nomTramite { get; set; }
        public int? estado { get; set; }
        public string? nomAgencia { get; set; }
    }
}
