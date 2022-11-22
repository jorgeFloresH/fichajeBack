using apiServices.Data.Filters;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;

namespace apiServices.Services
{
    public class VentanillaService : IVentanillaService
    {
        siscolasgamcContext _context;

        public VentanillaService(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }
        public async Task<List<VentanillaResponse>> GetVentanillaAsync(VentanillaFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<VentanillaResponse> result = new List<VentanillaResponse>();
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            result = AddFiltersOnQuery(filter, _context, skip, paginationFilter);
            return result;
        }
        private static List<VentanillaResponse> AddFiltersOnQuery(VentanillaFilter filter, siscolasgamcContext contex, int skip, PaginationFilter pFilter)
        {
            var resp =
                (
                    from dt in contex.Ventanillas
                    select
                        new
                        {
                            idVentanilla = dt.IdVentanilla,
                            nomVentanilla = dt.NomVentanilla,
                            estadoV = dt.EstadoV,
                            idAgencia = dt.IdAgencia,
                            nomAgencia = dt.IdAgenciaNavigation.NomAgencia
                        }
                );
            if (filter.sort != null)
            {
                if (filter.sort.Contains("-"))
                {
                    var critery = filter.sort.Substring(1);
                    switch (critery)
                    {
                        case "idVentanilla":
                            resp = resp.OrderByDescending(o => o.idVentanilla);
                            break;
                        case "nomVentanilla":
                            resp = resp.OrderByDescending(o => o.nomVentanilla);
                            break;
                        case "nomAgencia":
                            resp = resp.OrderByDescending(o => o.nomAgencia);
                            break;
                    }
                }
                else
                {
                    switch (filter.sort)
                    {
                        case "idVentanilla":
                            resp = resp.OrderBy(o => o.idVentanilla);
                            break;
                        case "nomVentanilla":
                            resp = resp.OrderBy(o => o.nomVentanilla);
                            break;
                        case "nomAgencia":
                            resp = resp.OrderBy(o => o.nomAgencia);
                            break;
                    }
                }
            }
            if (!string.IsNullOrEmpty(filter.nombreAgencia))
            {
                resp = resp.Where(u => u.nomAgencia.Contains(filter.nombreAgencia));
            }
            if (!string.IsNullOrEmpty(filter.nombreVentanilla))
            {
                resp = resp.Where(u => u.nomVentanilla.Contains(filter.nombreVentanilla));
            }
            var resResult = resp.Skip(skip).Take(pFilter.PageSize).ToList();
            List<VentanillaResponse> result = new List<VentanillaResponse>();
            foreach (var fila in resResult)
            {
                var queryBeforeSec =
                    (
                        from dt in contex.Ventanillas
                        select
                            new
                            {
                                idVentanilla = dt.IdVentanilla,
                                nomVentanilla = dt.NomVentanilla,
                                estadoV = dt.EstadoV,
                                idAgencia = dt.IdAgencia,
                                nomAgencia = dt.IdAgenciaNavigation.NomAgencia
                            }
                    ).ToList();
                VentanillaResponse element = new VentanillaResponse();
                element.idVentanilla = fila.idVentanilla;
                element.nomVentanilla = fila.nomVentanilla;
                element.estadoV = fila.estadoV;
                element.idAgencia = fila.idAgencia;
                element.nomAgencia = fila.nomAgencia;
                result.Add(element);
            }
            return result;
        }
    }
    public interface IVentanillaService
    {
        Task<List<VentanillaResponse>>
        GetVentanillaAsync(VentanillaFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
