using apiServices.Data.Filters;
using apiServices.Data.Queries;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;

namespace apiServices.Services
{
    public class MultimediaPageService : IMultimediaPageService
    {
        siscolasgamcContext _context;

        public MultimediaPageService(siscolasgamcContext dbcontext)
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
                    select
                        new
                        {
                            idMulti = dt.IdMulti,
                            nomVideo = dt.NomVideo,
                            estado = dt.Estado,
                            tipo = dt.Tipo,
                            ruta = dt.Ruta,
                            idAgencia = dt.IdAgencia
                        }
                );
            if (filter.idAgencia != 0)
            {
                resp = resp.Where(o => o.idAgencia == filter.idAgencia);
            }
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
    public interface IMultimediaPageService
    {
        Task<List<PaginationResponse>>
        GetMultimediaPageAsync(MultimediaFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
