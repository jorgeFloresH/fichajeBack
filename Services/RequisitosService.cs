using apiServices.Data;
using apiServices.Data.Filters;
using apiServices.Data.Queries;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;
using System.Xml.Linq;

namespace apiServices.Services
{
    public class RequisitosService : IRequisitoService
    {
        siscolasgamcContext _context;

        public RequisitosService(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<RequisitosResponse>> GetRequisitoAsync(RequisitosFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<RequisitosResponse> result = new List<RequisitosResponse>();
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            result = AddFiltersOnQuery(filter, _context, skip, paginationFilter);
            return result;
        }

        private static List<RequisitosResponse> AddFiltersOnQuery(RequisitosFilter filter, siscolasgamcContext context, int skip, PaginationFilter pFilter)
        {
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
            if (filter.sort != null)
            {
                if (filter.sort.Contains("-"))
                {
                    var critery = filter.sort.Substring(1);
                    switch (critery)
                    {
                        case "idRequisitos":
                            resp = resp.OrderByDescending(o => o.idRequisitos);
                            break;
                        case "nomRequisitos":
                            resp = resp.OrderByDescending(o => o.nomRequisitos);
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
                        case "idRequisitos":
                            resp = resp.OrderBy(o => o.idRequisitos);
                            break;
                        case "nomRequisitos":
                            resp = resp.OrderBy(o => o.nomRequisitos);
                            break;
                        case "estado":
                            resp = resp.OrderBy(o => o.estado);
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(filter?.nombre))
            {
                resp = resp.Where(o => o.nomRequisitos.Contains(filter.nombre));
            }
            var resResult = resp.Skip(skip).Take(pFilter.PageSize).ToList();
            List<RequisitosResponse> result = new List<RequisitosResponse>();
            foreach (var fila in resResult)
            {
                var queryBeforeSec =
                    (
                        from dt in context.Requisitos
                        select
                            new
                            {
                                idRequisitos = dt.IdRequisitos,
                                nomRequisitos = dt.NomRequisitos,
                                estado = dt.Estado
                            }
                    ).ToList();

                RequisitosResponse element = new RequisitosResponse();
                element.idRequisitos = fila.idRequisitos;
                element.nomRequisitos = fila.nomRequisitos;
                element.estado = fila.estado;
                result.Add(element);
            }
            return result;
        }
    }
    public interface IRequisitoService
    {
        Task<List<RequisitosResponse>>
        GetRequisitoAsync(RequisitosFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
