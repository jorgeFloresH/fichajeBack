using apiServices.Data.Filters;
using apiServices.Data.Responses;
using apiServices.Data.Queries;
using apiServices.Domain;
using apiServices.Models;

namespace apiServices.Services
{
    public class MultimediaPageServiceSuper : IMultimediaPageServiceSuper
    {
        siscolasgamcContext _context;

        public MultimediaPageServiceSuper(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<PaginationResponse>> GetMultimediaPageAsync(MultimediaFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<PaginationResponse> result = new List<PaginationResponse>();
            result = AddFiltersOnQuery(filter, _context, paginationFilter);
            return result;
        }

        private static List<PaginationResponse> AddFiltersOnQuery(MultimediaFilter filter, siscolasgamcContext context, PaginationFilter pFilter)
        {
            List<PaginationResponse> res = new List<PaginationResponse>();
            var resp =
            (
            from dt in context.Multimedia
            group dt by dt.NomVideo into g
            select
                new
                {
                    nomVideo = g.Key
                }
            );
            if (!string.IsNullOrEmpty(filter.nombreVideo))
            {
                resp = resp.Where(u => u.nomVideo.Contains(filter.nombreVideo));
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
    public interface IMultimediaPageServiceSuper
    {
        Task<List<PaginationResponse>>
        GetMultimediaPageAsync(MultimediaFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
