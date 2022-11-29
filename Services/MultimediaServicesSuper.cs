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
            List<MultimediaResponseSuper> result = new List<MultimediaResponseSuper>();
            var multimedia = contex.Multimedia
                   .GroupBy(a => a.NomVideo)
                   .Select(t =>
                      new
                      {

                          NomVideo = t.Key.ToString(),
                          datos = t.Select(p => new
                          {
                              IdMulti = p.IdMulti,
                              Estado = p.Estado,
                              Tipo = p.Tipo,
                              Ruta = p.Ruta,
                              IdAgencia = p.IdAgencia,
                              nomAgencia = p.IdAgenciaNavigation.NomAgencia
                          }),
                      }
              ).ToList();
            var resResult = multimedia.Skip(skip).Take(pFilter.PageSize).ToList();
            foreach (var fila in resResult)
            {
                var respuesta = contex.Multimedia
                  .GroupBy(a => a.NomVideo)
                  .Select(t =>
                     new
                     {
                         NomVideo = t.Key.ToString(),
                         datos = t.Select(p => new
                         {
                             IdMulti = p.IdMulti,
                             Estado = p.Estado,
                             Tipo = p.Tipo,
                             Ruta = p.Ruta,
                             IdAgencia = p.IdAgencia,
                             nomAgencia = p.IdAgenciaNavigation.NomAgencia
                         })

                     }
             ).ToList();
                MultimediaResponseSuper element = new MultimediaResponseSuper();
                element.nomVideo = fila.NomVideo;
                //element.data = fila.datos;
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
