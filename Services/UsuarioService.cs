using apiServices.Data.Filters;
using apiServices.Data.Responses;
using apiServices.Domain;
using apiServices.Models;
using Microsoft.AspNetCore.Mvc.Filters;

namespace apiServices.Services
{
    public class UsuarioService: IUsuarioService
    {
        siscolasgamcContext _context;

        public UsuarioService(siscolasgamcContext dbcontext)
        {
            _context = dbcontext;
        }

        public async Task<List<UserResponse>> GetUsuarioAsync (UsuarioFilter filter = null, PaginationFilter paginationFilter = null)
        {
            List<UserResponse> result = new List<UserResponse>();
            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;
            result = AddFiltersOnQuery(filter, _context, skip, paginationFilter);
            return result;
        }
        private static List<UserResponse> AddFiltersOnQuery(UsuarioFilter filter, siscolasgamcContext contex, int skip, PaginationFilter pFilter)
        {
            var resp =
                (
                    from dt in contex.Usuarios
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
                        case "nombreCompleto":
                            resp = resp.OrderByDescending(o => string.Join(" ", o.nomUsuario, o.apePaterno, o.apeMaterno));
                            break;
                        case "ci":
                            resp = resp.OrderByDescending(o => o.ciUsuario);
                            break;
                        case "nombreUsuario":
                            resp = resp.OrderByDescending(o => o.userName);
                            break;
                        case "cargo":
                            resp = resp.OrderByDescending(o => o.nomTipoP);
                            break;
                        case "agencia":
                            resp = resp.OrderByDescending(o => o.nomAgencia);
                            break;
                        case "idUsuario":
                            resp = resp.OrderByDescending(o => o.idUsuario);
                            break;
                    }
                }
                else
                {
                    switch (filter.sort)
                    {
                        case "nombreCompleto":
                            resp = resp.OrderBy(o => string.Join(" ", o.nomUsuario, o.apePaterno, o.apeMaterno));
                            break;
                        case "ci":
                            resp = resp.OrderBy(o => o.ciUsuario);
                            break;
                        case "nombreUsuario":
                            resp = resp.OrderBy(o => o.userName);
                            break;
                        case "cargo":
                            resp = resp.OrderBy(o => o.nomTipoP);
                            break;
                        case "agencia":
                            resp = resp.OrderBy(o => o.nomAgencia);
                            break;
                        case "idUsuario":
                            resp = resp.OrderBy(o => o.idUsuario);
                            break;
                    }
                }
            }
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
            var resResult = resp.Skip(skip).Take(pFilter.PageSize).ToList();
            List<UserResponse> result = new List<UserResponse>();
            foreach (var fila in resResult)
            {
                var queryBeforeSec =
                    (
                        from dt in contex.Usuarios
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
                    ).ToList();
                UserResponse element = new UserResponse();
                element.idUsuario = fila.idUsuario;
                element.userName = fila.userName;
                element.userPassword = fila.userPassword;
                element.idPerfil = fila.idPerfil;
                element.fechaCreacion = fila.fechaCreacion;
                element.estado = fila.estado;
                element.idAgencia = fila.idAgencia;
                element.nomAgencia = fila.nomAgencia;
                element.nomUsuario = fila.nomUsuario;
                element.apePaterno = fila.apePaterno;
                element.apeMaterno = fila.apeMaterno;
                element.ciUsuario = fila.ciUsuario;
                element.nomTipoP = fila.nomTipoP;
                element.mapa = fila.mapa;
                element.multimedia = fila.multimedia;
                element.consulta = fila.consulta;
                result.Add(element);
            }
            return result;
        }
    }

    public interface IUsuarioService
    {
        Task<List<UserResponse>>
        GetUsuarioAsync(UsuarioFilter filter = null, PaginationFilter paginationFilter = null);
    }
}
