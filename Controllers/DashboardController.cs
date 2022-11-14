using apiServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace apiServices.Controllers
{
    public class DashboardController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;
        public DashboardController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet("api/countUserWTicket/{idAgencia}")]
        public IActionResult countUserByTikets(int idAgencia)
        {
            try
            {
                var ticketCountUser = _dbcontext.UserTickets
                    .Where(t =>
                        t.IdTicketNavigation.IdAgencia == idAgencia
                            && t.IdTicketNavigation.Estado == 4 && t.IdUsuarioNavigation.IdPerfil == 3)
                    .GroupBy(t => t.IdUsuarioNavigation.NomUsuario)
                    .Select(t =>
                new
                {
                    NombreUser = t.Key.ToString(),
                    conteo = t.Count(),

                }
               )
               .ToList();


                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = ticketCountUser });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }

        [HttpGet("api/getByDate/{ciUsuario}/{fecha1}/{fecha2}")]
        public IActionResult reportUser(int ciUsuario, DateTime fecha1,DateTime fecha2)
        {
            try
            {
                DateTime fecha1UTC = fecha1.ToUniversalTime();
                DateTime fecha2UTC = fecha2.ToUniversalTime();
                Console.WriteLine("fecha : "+ fecha1.Date);
                var ticketCountUser = _dbcontext.UserTickets
                    .Where(t=> t.IdTicketNavigation.FechaHora.Value.Date >= fecha1UTC.Date && 
                            t.IdTicketNavigation.FechaHora.Value.Date <= fecha2UTC.Date && 
                            t.IdUsuarioNavigation.CiUsuario == ciUsuario)
                    .Select(a=> new
                    {
                        idUserTicket = a.IdUserTicket,
                        idTicket = a.IdTicket,
                        idUsuario = a.IdUsuario,
                        nomUsuario =  a.IdUsuarioNavigation.NomUsuario,
                        apPaterno = a.IdUsuarioNavigation.ApePaterno,
                        apMaterno = a.IdUsuarioNavigation.ApeMaterno,
                        idTramite = a.IdTicketNavigation.IdTramite,
                        nomTramite = a.IdTicketNavigation.IdTramiteNavigation.NomTramite,
                        fechaHoraTicket = a.IdTicketNavigation.FechaHora,
                        horaAtencion = a.IdTicketNavigation.HoraAtencion,
                        duracionAtencion = a.IdTicketNavigation.DuracionAtencion,
                    })
                    .OrderByDescending(a=>a.idUserTicket)
                    .ToList();

                var countTicketTramite  = _dbcontext.UserTickets
                    .Where(t => t.IdTicketNavigation.FechaHora.Value.Date >= fecha1UTC.Date &&
                            t.IdTicketNavigation.FechaHora.Value.Date <= fecha2UTC.Date &&
                            t.IdUsuarioNavigation.CiUsuario == ciUsuario)
                    .GroupBy(t => t.IdTicketNavigation.IdTramiteNavigation.NomTramite)
                    .Select(t =>
                        new
                        {
                            nomTramite = t.Key.ToString(),
                            conteo = t.Count(),
                            

                        }
                       )
                     .ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = ticketCountUser  ,response2 = countTicketTramite });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("api/getUserbyCI/{ciUsuario}")]
        public IActionResult getUserbyCI(int ciUsuario)
        {
            try
            {

                var usuario = _dbcontext.Usuarios
                     .Where(a=> a.CiUsuario == ciUsuario)   
                    .Select(t =>
                       new
                       {
                         nomUsuario =  t.NomUsuario,
                         apPaterno = t.ApePaterno,
                         apMaterno = t.ApeMaterno,
                         ci = t.CiUsuario,
                         perfil = t.IdPerfilNavigation.NomTipoP,
                         idPerfil = t.IdPerfil,
                       }
                       ).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("api/getDataByName/{nomUsuario}/{idAgencia}")]
        public IActionResult getDataByName(string nomUsuario, int idAgencia)
        {
            try
            {
                var usuario = _dbcontext.UserTickets
                                .Where(p => p.IdUsuarioNavigation.NomUsuario == nomUsuario && p.IdUsuarioNavigation.IdAgencia == idAgencia )
                                .GroupBy(a => 
                                new { 
                                    a.IdUsuarioNavigation.CiUsuario,
                                    a.IdUsuarioNavigation.NomUsuario, 
                                    a.IdUsuarioNavigation.ApePaterno, 
                                    a.IdUsuarioNavigation.ApeMaterno,
                                    a.IdUsuarioNavigation.IdPerfil,
                                    a.IdUsuarioNavigation.IdPerfilNavigation.NomTipoP
                                })
                                .Select(t => new
                                {
                                    cont = t.Count(),
                                    contAtendidos = t.Count(f => f.IdTicketNavigation.Estado == 4),
                                    contNoAtendidos = t.Count(f => f.IdTicketNavigation.Estado == 6),
                                    data = t.Key,
                                })
                                .ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = usuario });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
    }
}
