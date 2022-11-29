using apiServices.Data.Filters;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;
using System.Text.RegularExpressions;

namespace apiServices.Services
{
    public class MultimediaService : IMultimediaService
    {
        siscolasgamcContext _context;

        public MultimediaService(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<MultimediaResponse>> GetMultimediaAsync(MultimediaFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<MultimediaResponse> result = new List<MultimediaResponse>();
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            result = AddFiltersOnQuery(filter, _context, skip, paginationFilter);
            return result;
        }
        private static List<MultimediaResponse> AddFiltersOnQuery(MultimediaFilter filter, siscolasgamcContext contex, int skip, PaginationFilter pFilter)
        {
            var resp =
                (
                    from dt in contex.Multimedia
                    select
                        new
                        {
                            idMulti = dt.IdMulti,
                            nomVideo = dt.NomVideo,
                            estado = dt.Estado,
                            tipo = dt.Tipo,
                            ruta = dt.Ruta,
                            idAgencia = dt.IdAgencia,
                            nomAgencia = dt.IdAgenciaNavigation.NomAgencia
                        }
                );
            if (filter.idAgencia != 0)
            {
                resp = resp.Where(o => o.idAgencia == filter.idAgencia);
            }

            if (filter.sort != null)
            {
                if (filter.sort.Contains("-"))
                {
                    var critery = filter.sort.Substring(1);
                    switch (critery)
                    {
                        case "idMulti":
                            resp = resp.OrderByDescending(o => o.idMulti);
                            break;
                        case "nomVideo":
                            resp = resp.OrderByDescending(o => o.nomVideo);
                            break;
                        case "estado":
                            resp = resp.OrderByDescending(o => o.estado);
                            break;
                    }
                }
                else
                {
                    switch (filter.sort)
                    {
                        case "idMulti":
                            resp = resp.OrderBy(o => o.idMulti);
                            break;
                        case "nomVideo":
                            resp = resp.OrderBy(o => o.nomVideo);
                            break;
                        case "estado":
                            resp = resp.OrderBy(o => o.estado);
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(filter.nombreVideo))
            {
                resp = resp.Where(u => u.nomVideo.Contains(filter.nombreVideo));
            }

            var resResult = resp.Skip(skip).Take(pFilter.PageSize).ToList();
           
            List<MultimediaResponse> result = new List<MultimediaResponse>();
            foreach (var fila in resResult)
            {
                var queryBeforeSec =
                    (
                        from dt in contex.Multimedia

                        select
                            new
                            {
                                idMulti = dt.IdMulti,
                                nomVideo = dt.NomVideo,
                                estado = dt.Estado,
                                tipo = dt.Tipo,
                                ruta = dt.Ruta,
                                idAgencia = dt.IdAgencia,
                                nomAgencia = dt.IdAgenciaNavigation.NomAgencia
                            }
                    ).ToList();
                MultimediaResponse element = new MultimediaResponse();
                element.idMulti = fila.idMulti;
                element.nomVideo = fila.nomVideo;
                element.estado = fila.estado;
                element.tipo = fila.tipo;
                element.ruta = fila.ruta;
                element.idAgencia = fila.idAgencia;
                element.nomAgencia = fila.nomAgencia;
                result.Add(element);
            }
            return result;
        }
    }
    public interface IMultimediaService
    {
        Task<List<MultimediaResponse>>
        GetMultimediaAsync(MultimediaFilter filter = null, PaginationFilter paginationFilter = null);
    }

}
