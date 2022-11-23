using apiServices.Data.Filters;
using apiServices.Data.Queries;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;
using System.Diagnostics.Metrics;

namespace apiServices.Services
{
    public class VentanillaPageService : IVentanillaPageService
    {
        siscolasgamcContext _context;

        public VentanillaPageService(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<PaginationResponse>> GetVentanillaPageAsync(VentanillaFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<PaginationResponse> result = new List<PaginationResponse>();
            result = AddFiltersOnQuery(filter, _context, paginationFilter);
            return result;
        }

        private static List<PaginationResponse> AddFiltersOnQuery(VentanillaFilter filter, siscolasgamcContext context, PaginationFilter pFilter)
        {
            List<PaginationResponse> res = new List<PaginationResponse>();
            var resp =
            (
                    from dt in context.Ventanillas
                    select
                        new
                        {
                            idVentanilla = dt.IdVentanilla,
                            nomVentanilla = dt.NomVentanilla,
                            estadoV = dt.EstadoV,
                            idAgencia = dt.IdAgencia,
                            nomAgencia = dt.IdAgenciaNavigation.NomAgencia,
                        }
                );
            if (filter.idAgencia != 0)
            {
                resp = resp.Where(o => o.idAgencia == filter.idAgencia);
            }
            if (!string.IsNullOrEmpty(filter.nombreVentanilla))
            {
                resp = resp.Where(v => v.nomVentanilla.Contains(filter.nombreVentanilla));
            }
            if (!string.IsNullOrEmpty(filter.nombreAgencia))
            {
                resp = resp.Where(v => v.nomAgencia.Contains(filter.nombreAgencia));
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

    public interface IVentanillaPageService
    {
        Task<List<PaginationResponse>>
        GetVentanillaPageAsync(VentanillaFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
