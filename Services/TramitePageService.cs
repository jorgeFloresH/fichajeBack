using apiServices.Data.Filters;
using apiServices.Data.Queries;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;

namespace apiServices.Services
{
    public class TramitePageService: ITramitePageService
    {
        siscolasgamcContext _context;

        public TramitePageService(siscolasgamcContext context)
        {
            _context = context;
        }
        public async Task<List<PaginationResponse>> GetTramitePageAsync(TramiteFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<PaginationResponse> result = new List<PaginationResponse>();
            result = AddFiltersOnQuery(filter, _context, paginationFilter);
            return result;
        }
        private static List<PaginationResponse> AddFiltersOnQuery(TramiteFilter filter, siscolasgamcContext context, PaginationFilter pFilter)
        {
            List<PaginationResponse> res = new List<PaginationResponse>();
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
            if (!string.IsNullOrEmpty(filter?.nombreTramite))
            {
                resp = resp.Where(t => t.nomTramite.Contains(filter.nombreTramite));
            }
            if (!string.IsNullOrEmpty(filter?.nombreAgencia))
            {
                resp = resp.Where(t => t.nomAgencia.Contains(filter.nombreAgencia));
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
    public interface ITramitePageService
    {
        Task<List<PaginationResponse>>
        GetTramitePageAsync(TramiteFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
