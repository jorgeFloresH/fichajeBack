using apiServices.Data.Filters;
using apiServices.Data.Queries;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;

namespace apiServices.Services
{
    public class TicketPageService : ITicketPageService
    {
        siscolasgamcContext _context;
        public TicketPageService(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }
        public async Task<List<PaginationResponse>> GetTicketPageAsync(TicketFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<PaginationResponse> result = new List<PaginationResponse>();
            result = AddFiltersOnQuery(filter, _context, paginationFilter);
            return result;
        }

        private static List<PaginationResponse> AddFiltersOnQuery(TicketFilter filter, siscolasgamcContext context, PaginationFilter pFilter)
        {
            List<PaginationResponse> res = new List<PaginationResponse>();
            var resp =
            (
                    from dt in context.Tickets
                    select
                        new
                        {
                            idTicket = dt.IdTicket,
                            idTramite = dt.NTicket,
                            nomTramite = dt.IdTramiteNavigation.NomTramite,
                            idUsuario = dt.IdUsuario,
                            estado = dt.Estado,
                            idVentanilla = dt.IdVentanilla,
                            nomVentanilla = dt.IdVentanillaNavigation.NomVentanilla,
                            fechaHora = dt.FechaHora,
                            horaAtencion = dt.HoraAtencion,
                            duracionAtencion = dt.DuracionAtencion,
                            idAgencia = dt.IdAgencia,
                            nomAgencia = dt.IdAgenciaNavigation.NomAgencia,
                            nTicket = dt.NTicket,
                            idPrioridad = dt.IdPrioridad,
                            tipo = dt.IdPrioridadNavigation.Tipo
                        }
                );
            if (!string.IsNullOrEmpty(filter.nombreAgencia))
            {
                resp = resp.Where(u => u.nomAgencia.Contains(filter.nombreAgencia));
            }
            if (!string.IsNullOrEmpty(filter.nombreTramite))
            {
                resp = resp.Where(u => u.nomTramite.Contains(filter.nombreTramite));
            }
            if (!string.IsNullOrEmpty(filter.nombreVentanilla))
            {
                resp = resp.Where(u => u.nomVentanilla.Contains(filter.nombreVentanilla));
            }
            if (!string.IsNullOrEmpty(filter.tipoCliente))
            {
                resp = resp.Where(u => u.tipo.Contains(filter.tipoCliente));
            }
            if (!string.IsNullOrEmpty(filter.fecha))
            {
                resp = resp.Where(u => u.fechaHora.ToString().Contains(filter.fecha));
            }
            PaginationResponse element = new PaginationResponse();
            var datos = new PaginationMetaData(resp.Count(), pFilter.PageNumber, pFilter.PageSize);
            element.CurrentPage = datos.CurrentPage;
            element.TotalCount = datos.TotalCount;
            element.TotalPages = datos.TotalPages;
            element.HasPrevious = datos.HasPrevious;
            element.HasNext = datos.HasNext;
            res.Add(element);
            return res;
        }

    }
    public interface ITicketPageService
    {
        Task<List<PaginationResponse>>
        GetTicketPageAsync(TicketFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
