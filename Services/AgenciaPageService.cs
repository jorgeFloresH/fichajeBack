using apiServices.Data.Filters;
using apiServices.Data.Queries;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;
using System.Xml.Linq;


    public class AgenciaPageService : IAgenciaPageService
    {
        siscolasgamcContext _context;

        public AgenciaPageService(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<PaginationResponse>> GetAgenciaPageAsync(AgenciaFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<PaginationResponse> result = new List<PaginationResponse>();
            result = AddFiltersOnQuery(filter, _context, paginationFilter);
            return result;
        }

        private static List<PaginationResponse> AddFiltersOnQuery(AgenciaFilter filter, siscolasgamcContext context, PaginationFilter pFilter)
        {
            List<PaginationResponse> result = new List<PaginationResponse>();
            PaginationResponse element = new PaginationResponse();
            if (filter.nombre != null)
            {
                var datos = new PaginationMetaData(context.Agencia.Where(a => a.NomAgencia.Contains(filter.nombre)).Count(), pFilter.PageNumber, pFilter.PageSize);
                element.CurrentPage = datos.CurrentPage;
                element.TotalCount = datos.TotalCount;
                element.TotalPages = datos.TotalPages;
                element.HasPrevious = datos.HasPrevious;
                element.HasNext = datos.HasNext;
                result.Add(element);
                return result;
            }
            else 
            {
                var datos = new PaginationMetaData(context.Agencia.Count(), pFilter.PageNumber, pFilter.PageSize);
                element.CurrentPage = datos.CurrentPage;
                element.TotalCount = datos.TotalCount;
                element.TotalPages = datos.TotalPages;
                element.HasPrevious = datos.HasPrevious;
                element.HasNext = datos.HasNext;
                result.Add(element);
                return result;
            }
        }
    }

    public interface IAgenciaPageService
    {
        Task<List<PaginationResponse>>
        GetAgenciaPageAsync(AgenciaFilter filter = null, PaginationFilter paginationFilter = null);
    }
