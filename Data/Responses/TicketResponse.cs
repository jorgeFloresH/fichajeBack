namespace apiServices.Data.Responses
{
    public class TicketResponse
    {
        public long idTicket { get; set; }
        public long? idTramite { get; set; }
        public string? nomTramite { get; set; }
        public long? idUsuario { get; set; }
        public int? estado { get; set; }
        public long? idVentanilla { get; set; }
        public string? nomVentanilla { get; set; }
        public DateTime? fechaHora { get; set; }
        public TimeOnly? horaAtencion { get; set; }
        public int? duracionAtencion { get; set; }
        public long? idAgencia { get; set; }
        public string? NomAgencia { get; set; }
        public int? nTicket { get; set; }
        public long? idPrioridad { get; set; }
        public string? tipo { get; set; }
    }
}
