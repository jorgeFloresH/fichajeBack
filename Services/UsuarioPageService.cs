using apiServices.Data.Filters;
using apiServices.Data.Queries;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;
using System.Diagnostics.Metrics;

namespace apiServices.Services
{
    public class UsuarioPageService : IUsuarioPageService
    {
        siscolasgamcContext _context;
        public UsuarioPageService(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<PaginationResponse>> GetUsuarioPageAsync(UsuarioFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<PaginationResponse> result = new List<PaginationResponse>();
            result = AddFiltersOnQuery(filter, _context, paginationFilter);
            return result;
        }
        
        private static List<PaginationResponse> AddFiltersOnQuery(UsuarioFilter filter, siscolasgamcContext context, PaginationFilter pFilter)
        {
            List<PaginationResponse> res = new List<PaginationResponse>();
            var resp =
            (
                    from dt in context.Usuarios
                    select
                        new
                        {
                            idUsuario = dt.IdUsuario,
                            userName = dt.UserName,
                            userPassword = dt.UserPassword,
                            idPerfil = dt.IdPerfil,
                            fechaCreacion = dt.FechaCreacion,
                            estado = dt.Estado,
                            idAgencia = dt.IdAgencia,
                            nomAgencia = dt.IdAgenciaNavigation.NomAgencia,
                            nomUsuario = dt.NomUsuario,
                            apePaterno = dt.ApePaterno,
                            apeMaterno = dt.ApeMaterno,
                            ciUsuario = dt.CiUsuario,
                            nomTipoP = dt.IdPerfilNavigation.NomTipoP,
                            mapa = dt.IdAgenciaNavigation.Mapa,
                            multimedia = dt.IdAgenciaNavigation.Multimedia,
                            consulta = dt.IdAgenciaNavigation.Consulta,
                        }
                );
            if (!string.IsNullOrEmpty(filter.nombreCompleto))
            {
                resp = resp.Where(u => string.Join(" ", u.nomUsuario, u.apePaterno, u.apeMaterno).Contains(filter.nombreCompleto));
            }
            if (!string.IsNullOrEmpty(filter.ci))
            {
                resp = resp.Where(u => u.ciUsuario == int.Parse(filter.ci));
            }
            if (!string.IsNullOrEmpty(filter.nombreUsuario))
            {
                resp = resp.Where(u => u.userName.Contains(filter.nombreUsuario));
            }
            if (!string.IsNullOrEmpty(filter.cargo))
            {
                resp = resp.Where(u => u.nomTipoP.Contains(filter.cargo));
            }
            if (!string.IsNullOrEmpty(filter.agencia))
            {
                resp = resp.Where(u => u.nomAgencia.Contains(filter.agencia));
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

    public interface IUsuarioPageService
    {
        Task<List<PaginationResponse>>
        GetUsuarioPageAsync(UsuarioFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
