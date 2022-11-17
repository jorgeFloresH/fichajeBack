using apiServices.Data;
using apiServices.Data.Filters;
using apiServices.Data.Queries;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;
using System.Xml.Linq;

namespace apiServices.Services
{
    public class AgenciaService : IAgenciaService
    {
        siscolasgamcContext _context;

        public AgenciaService(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<AgenciaResponse>> GetAgenciaAsync(AgenciaFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<AgenciaResponse> result = new List<AgenciaResponse>();
            if (paginationFilter == null)
            {
                //return await queryable.Include(x => x.Tags).ToListAsync();
            }
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            result = AddFiltersOnQuery(filter, _context, skip, paginationFilter);
            return result;
        }

        private static List<AgenciaResponse> AddFiltersOnQuery(AgenciaFilter filter, siscolasgamcContext context, int skip, PaginationFilter pFilter)
        {
            string? fnombre = null;
            var resp =
                (
                    from dt in context.Agencia
                    select
                        new
                        {
                            idAgencia = dt.IdAgencia,
                            nomAgencia = dt.NomAgencia,
                            estado = dt.Estado,
                            ascdesc = dt.Acdes,
                            mapa = dt.Mapa,
                            multimedia = dt.Multimedia,
                            consulta = dt.Consulta
                        }
                );
            if (filter.sort != null)
            {
                if (filter.sort.Contains("-"))
                {
                    var critery = filter.sort.Substring(1);
                    switch (critery)
                    {
                        case "idAgencia":
                            resp = resp.OrderByDescending(o => o.idAgencia);
                            break;
                        case "nomAgencia":
                            resp = resp.OrderByDescending(o => o.nomAgencia);
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
                        case "idAgencia":
                            resp = resp.OrderBy(o => o.idAgencia);
                            break;
                        case "nomAgencia":
                            resp = resp.OrderBy(o => o.nomAgencia);
                            break;
                        case "estado":
                            resp = resp.OrderBy(o => o.estado);
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(filter?.nombre))
            {
                fnombre = filter.nombre;
                resp = resp.Where(o => o.nomAgencia == fnombre);
            }
            var resResult = resp.Skip(skip).Take(pFilter.PageSize).ToList();
            List<AgenciaResponse> result = new List<AgenciaResponse>();
            foreach (var fila in resResult)
            {
                var queryBeforeSec =
                    (
                        from dt in context.Agencia
                        select
                            new
                            {
                                idAgencia = dt.IdAgencia,
                                nomAgencia = dt.NomAgencia,
                                estado = dt.Estado,
                                ascdesc = dt.Acdes,
                                mapa = dt.Mapa,
                                multimedia = dt.Multimedia,
                                consulta = dt.Consulta
                            }
                    ).ToList();
                
                AgenciaResponse element = new AgenciaResponse();
                element.idAgencia = fila.idAgencia;
                element.nomAgencia = fila.nomAgencia;
                element.estado = fila.estado;
                element.acdes = fila.ascdesc;
                element.mapa = fila.mapa;
                element.multimedia = fila.multimedia;
                element.consulta = fila.consulta;
                result.Add(element);
            }
            return result;
        }
    }
    public interface IAgenciaService
    {
        Task<List<AgenciaResponse>>
        GetAgenciaAsync(AgenciaFilter filter = null, PaginationFilter paginationFilter = null);
    }
}