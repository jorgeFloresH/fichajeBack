using apiServices.Data.Filters;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;

namespace apiServices.Services
{
    public class TramiteService : ITramiteService
    {
        siscolasgamcContext _context;

        public TramiteService(siscolasgamcContext context)
        {
            _context = context;
        }          
        
        public async Task<List<TramiteResponse>> GetTramiteAsync(TramiteFilter filter = null, PaginationFilter  paginationFilter= null)
        {
            List<TramiteResponse> result = new List<TramiteResponse>();
            var skip = (paginationFilter.PageNumber -1) * paginationFilter.PageSize;
            result = AddFiltersOnQuery(filter, _context, skip, paginationFilter);
            return result;
        }

        private static List<TramiteResponse> AddFiltersOnQuery(TramiteFilter filter, siscolasgamcContext context, int skip,PaginationFilter pFilter)
        {
            var resp =
                (
                    from dt in context.Tramites
                    select
                        new
                        {
                            idTramite = dt.IdTramite,
                            idAgencia = dt.IdAgencia,
                            nomTramite = dt.NomTramite,
                            estado = dt.Estado,
                            nomAgencia = dt.IdAgenciaNavigation.NomAgencia,
                        }
                );
            if (filter.idAgencia != 0)
            {
                resp = resp.Where(o => o.idAgencia == filter.idAgencia);
            }
            if (filter.sort !=null)
            {
                if (filter.sort.Contains("-"))
                {
                    var critery = filter.sort.Substring(1);
                    switch (critery)
                    {
                        case "idTramite":
                            resp = resp.OrderByDescending(o => o.idTramite);
                            break;
                        case "idAgencia":
                            resp = resp.OrderByDescending(o => o.idAgencia);
                            break;
                        case "estado":
                            resp = resp.OrderByDescending(o => o.estado);
                            break;
                        case "nomTramite":
                            resp = resp.OrderByDescending(o => o.nomTramite);
                            break;
                        case "nomAgencia":
                            resp = resp.OrderByDescending(o => o.nomAgencia);
                            break;
                    }
                }
                else
                {
                    switch (filter.sort)
                    {
                        case "idTramite":
                            resp = resp.OrderBy(o => o.idTramite);
                            break;
                        case "idAgencia":
                            resp = resp.OrderBy(o => o.idAgencia);
                            break;
                        case "estado":
                            resp = resp.OrderBy(o => o.estado);
                            break;
                        case "nomTramite":
                            resp = resp.OrderBy(o => o.nomTramite);
                            break;
                        case "nomAgencia":
                            resp = resp.OrderBy(o => o.nomAgencia);
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(filter?.nombreTramite))
            {
                resp = resp.Where(t => t.nomTramite.Contains(filter.nombreTramite));
            }
            if (!string.IsNullOrEmpty(filter?.nombreAgencia))
            {
                resp = resp.Where(t => t.nomAgencia.Contains(filter.nombreAgencia));
            }
            var resResult = resp.Skip(skip).Take(pFilter.PageSize).ToList();
            List<TramiteResponse> result = new List<TramiteResponse>();
            foreach (var fila in resResult)
            {
                var queryBeforeSec =
                    (
                        from dt in context.Tramites
                        select
                            new
                            {
                                idTramite = dt.IdTramite,
                                idAgencia = dt.IdAgencia,
                                nomTramite = dt.NomTramite,
                                estado = dt.Estado,
                                nomAgencia = dt.IdAgenciaNavigation.NomAgencia,
                            }
                    ).ToList();

                TramiteResponse element = new TramiteResponse();
                element.idTramite = fila.idTramite;
                element.idAgencia = fila.idAgencia;
                element.estado = fila.estado;
                element.nomTramite = fila.nomTramite;
                element.nomAgencia = fila.nomAgencia;
                result.Add(element);
            }
            return result;
        }
    }

    public interface ITramiteService
    {
        Task<List<TramiteResponse>>
            GetTramiteAsync(TramiteFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
