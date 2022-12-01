using apiServices.Data.Filters;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;

namespace apiServices.Services
{
    public class TicketService : ITicketService
    {
        siscolasgamcContext _context;

        public TicketService(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<TicketResponse>> GetTicketAsync(TicketFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<TicketResponse> result = new List<TicketResponse>();
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            result = AddFiltersOnQuery(filter, _context, skip, paginationFilter);
            return result;
        }
        private static List<TicketResponse> AddFiltersOnQuery(TicketFilter filter, siscolasgamcContext contex, int skip, PaginationFilter pFilter)
        {
            var resp =
                (
                    from dt in contex.Tickets
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
            if (filter.sort != null)
            {
                if (filter.sort.Contains("-"))
                {
                    var critery = filter.sort.Substring(1);
                    switch (critery)
                    {
                        case "idTicket":
                            resp = resp.OrderByDescending(o => o.idTicket);
                            break;
                        case "nomAgencia":
                            resp = resp.OrderByDescending(o => o.nomAgencia);
                            break;
                        case "nomTramite":
                            resp = resp.OrderByDescending(o => o.nomTramite);
                            break;
                        case "nomVentanilla":
                            resp = resp.OrderByDescending(o => o.nomVentanilla);
                            break;
                        case "tipo":
                            resp = resp.OrderByDescending(o => o.tipo);
                            break;
                        case "fechaHora":
                            resp = resp.OrderByDescending(o => o.fechaHora);
                            break;
                        case "horaAtencion":
                            resp = resp.OrderByDescending(o => o.horaAtencion);
                            break;
                    }
                }
                else
                {
                    switch (filter.sort)
                    {
                        case "idTicket":
                            resp = resp.OrderBy(o => o.idTicket);
                            break;
                        case "nomAgencia":
                            resp = resp.OrderBy(o => o.nomAgencia);
                            break;
                        case "nomTramite":
                            resp = resp.OrderBy(o => o.nomTramite);
                            break;
                        case "nomVentanilla":
                            resp = resp.OrderBy(o => o.nomVentanilla);
                            break;
                        case "tipo":
                            resp = resp.OrderBy(o => o.tipo);
                            break;
                        case "fechaHora":
                            resp = resp.OrderBy(o => o.fechaHora);
                            break;
                        case "horaAtencion":
                            resp = resp.OrderBy(o => o.horaAtencion);
                            break;
                    }
                }
            }
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

            var resResult = resp.Skip(skip).Take(pFilter.PageSize).ToList();
            List<TicketResponse> result = new List<TicketResponse>();
            foreach (var fila in resResult)
            {
                var queryBeforeSec =
                    (
                        from dt in contex.Tickets
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
                                NomAgencia = dt.IdAgenciaNavigation.NomAgencia,
                                nTicket = dt.NTicket,
                                idPrioridad = dt.IdPrioridad,
                                tipo = dt.IdPrioridadNavigation.Tipo
                            }
                    ).ToList();
                TicketResponse element = new TicketResponse();
                element.idTicket = fila.idTicket;
                element.idTramite = fila.idTramite;
                element.nomTramite = fila.nomTramite;
                element.idUsuario = fila.idUsuario;
                element.estado = fila.estado;
                element.idVentanilla = fila.idVentanilla;
                element.nomVentanilla = fila.nomVentanilla;
                element.fechaHora = fila.fechaHora;
                element.horaAtencion = fila.horaAtencion;
                element.duracionAtencion = fila.duracionAtencion;
                element.idAgencia = fila.idAgencia;
                element.NomAgencia = fila.nomAgencia;
                element.nTicket = fila.nTicket;
                element.idPrioridad = fila.idPrioridad;
                element.tipo = fila.tipo;
                result.Add(element);
            }
            return result;
        }
    }

    public interface ITicketService
    {
        Task<List<TicketResponse>>
        GetTicketAsync(TicketFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
