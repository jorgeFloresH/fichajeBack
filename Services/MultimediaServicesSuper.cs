using apiServices.Data.Filters;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;
using System.IO.Pipelines;

namespace apiServices.Services
{
    public class MultimediaServicesSuper : IMultimediaServiceSuper
    {
        siscolasgamcContext _context;

        public MultimediaServicesSuper(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<MultimediaResponseSuper>> GetMultimediaSuperAsync(MultimediaFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<MultimediaResponseSuper> result = new List<MultimediaResponseSuper>();
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            result = AddFiltersOnQuery(filter, _context, skip, paginationFilter);
            return result;
        }
        private static List<MultimediaResponseSuper> AddFiltersOnQuery(MultimediaFilter filter, siscolasgamcContext contex, int skip, PaginationFilter pFilter)
        {
            var resp =
            (
            from dt in contex.Multimedia
            group dt by dt.NomVideo into g
            select
                new
                {
                    nomVideo = g.Key
                }
            );

            if (filter.sort != null)
            {
                if (filter.sort.Contains("-"))
                {
                    var critery = filter.sort.Substring(1);
                    switch (critery)
                    {
                        case "nomVideo":
                            resp = resp.OrderByDescending(o => o.nomVideo);
                            break;
                    }
                }
                else
                {
                    switch (filter.sort)
                    {
                        case "nomVideo":
                            resp = resp.OrderBy(o => o.nomVideo);
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(filter.nombreVideo))
            {
                resp = resp.Where(u => u.nomVideo.Contains(filter.nombreVideo));
            }

            var resResult = resp.Skip(skip).Take(pFilter.PageSize).ToList();
            List<MultimediaResponseSuper> result = new List<MultimediaResponseSuper>();
            foreach (var fila in resResult)
            {
                var respuesta = contex.Multimedia
                  .GroupBy(a => a.NomVideo)
                  .Select(t =>
                     new
                     {
                         nomVideo = t.Key.ToString()
                     }
             );
                MultimediaResponseSuper element = new MultimediaResponseSuper();
                element.nomVideo = fila.nomVideo;
                result.Add(element);
            }
            return result;
        }
    }
    public interface IMultimediaServiceSuper
    {
        Task<List<MultimediaResponseSuper>>
        GetMultimediaSuperAsync(MultimediaFilter filter = null, PaginationFilter paginationFilter = null);
    }

}
