namespace apiServices.Data.Filters
{
    public class TicketFilter
    {
        public string nombreAgencia { get; set; }
        public string nombreTramite { get; set; }
        public string nombreVentanilla { get; set; }
        public string tipoCliente { get; set; }
        public string fecha { get; set; }
        public string sort { get; set; }
    }
}
