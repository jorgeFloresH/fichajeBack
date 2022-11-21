using apiServices.Data.Filters;
using apiServices.Data.Queries;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;

namespace apiServices.Services
{
    public class RequisitoPageService : IRequisitoPageService
    {
        siscolasgamcContext _context;

        public RequisitoPageService(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }
        public async Task<List<PaginationResponse>> GetRequisitoPageAsync(RequisitosFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<PaginationResponse> result = new List<PaginationResponse>();
            result = AddFiltersOnQuery(filter, _context, paginationFilter);
            return result;
        }
        private static List<PaginationResponse> AddFiltersOnQuery(RequisitosFilter filter, siscolasgamcContext context, PaginationFilter pFilter)
        {
            List<PaginationResponse> res = new List<PaginationResponse>();
            var resp =
            (
                    from dt in context.Requisitos
                    select
                        new
                        {
                            idRequisitos = dt.IdRequisitos,
                            nomRequisitos = dt.NomRequisitos,
                            estado = dt.Estado
                        }
                );
            if (!string.IsNullOrEmpty(filter.nombre))
            {
                resp = resp.Where(u =>u.nomRequisitos.Contains(filter.nombre));
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

    public interface IRequisitoPageService
    {
        Task<List<PaginationResponse>>
        GetRequisitoPageAsync(RequisitosFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
