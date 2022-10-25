using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;

        public TicketController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        public IActionResult ticketsGet()
        {
            //List<Ticket> tickets = new List<Ticket>();
            try
            {

                 var tickets = _dbcontext.Tickets.Select(t =>
                new
                {
                    idTicket = t.IdTicket,
                    idTramite = t.NTicket,
                    nomTramite = t.IdTramiteNavigation.NomTramite,
                    idUsuario = t.IdUsuario,
                    estado = t.Estado,
                    idVentanilla = t.IdVentanilla,
                    nomVentanilla = t.IdVentanillaNavigation.NomVentanilla,
                    fechaHora = t.FechaHora,
                    horaAtencion = t.HoraAtencion,
                    duracionAtencion = t.DuracionAtencion,
                    idAgencia = t.IdAgencia,
                    NomAgencia = t.IdAgenciaNavigation.NomAgencia,
                    nTicket = t.NTicket,
                    idPrioridad = t.IdPrioridad,
                    tipo = t.IdPrioridadNavigation.Tipo
                }
                )
                    .OrderByDescending(t => t.idTicket)
                    .ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tickets });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("StatusTicket/{estado}/{agencia}")]
        public IActionResult GetstatusTicket( int estado,int agencia)
        {
            //List<Ticket> dateticket = new List<Ticket>();

            try
            {

                DateTime fe = DateTime.UtcNow.AddHours(-4);
                Console.WriteLine(fe);
                var tickets = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.Estado == estado && t.IdAgencia == agencia).Select(t =>
               new
               {
                   idTicket = t.IdTicket,
                   idTramite = t.NTicket,
                   nomTramite = t.IdTramiteNavigation.NomTramite,
                   idUsuario = t.IdUsuario,
                   estado = t.Estado,
                   idVentanilla = t.IdVentanilla,
                   nomVentanilla = t.IdVentanillaNavigation.NomVentanilla,
                   fechaHora = t.FechaHora,
                   horaAtencion = t.HoraAtencion,
                   duracionAtencion = t.DuracionAtencion,
                   idAgencia = t.IdAgencia,
                   NomAgencia = t.IdAgenciaNavigation.NomAgencia,
                   nTicket = t.NTicket,
                   idPrioridad = t.IdPrioridad,
                   tipo = t.IdPrioridadNavigation.Tipo
               }
               )
                    .OrderByDescending(c=>c.idTicket)
                    .ToList();

                if (tickets == null)
                {
                    return BadRequest("Ticket no encontrado");
                }
                //requisito = (RequisitoTramite)_dbcontext.RequisitoTramites.Where(p => p.IdTramite == idtramite);
                //dateticket = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.Estado == estado && t.IdAgencia == agencia).Include(o => o.IdTramiteNavigation).ToList();
                
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tickets });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        [HttpGet("StatusTicket/{agencia}")]
        public IActionResult GetstatusTicketFilter( int agencia)
        {
            //List<Ticket> dateticket = new List<Ticket>();

            try
            {
                
                DateTime fe = DateTime.UtcNow.AddHours(-04);
                Console.WriteLine(fe);
                var tickets = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.IdAgencia == agencia && t.Estado == 2).Select(t =>
               
                new
               {
                   idTicket = t.IdTicket,
                   idTramite = t.IdTramite,
                   nomTramite = t.IdTramiteNavigation.NomTramite,
                   estado = t.Estado,
                   idVentanilla = t.IdVentanilla,
                   nomVentanilla = t.IdVentanillaNavigation.NomVentanilla,
                   fechaHora = t.FechaHora,
                   nTicket = t.IdTramiteNavigation.NomTramite +"-"+t.NTicket,
                   idPrioridad = t.IdPrioridad,
                   tipo = t.IdPrioridadNavigation.Tipo
               }
               )
                    .OrderByDescending(c => c.idTicket)
                    .ToList();

                if (tickets == null)
                {
                    return BadRequest("Ticket no encontrado");
                }
                //requisito = (RequisitoTramite)_dbcontext.RequisitoTramites.Where(p => p.IdTramite == idtramite);
                //dateticket = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.Estado == estado && t.IdAgencia == agencia).Include(o => o.IdTramiteNavigation).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tickets });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        [HttpGet("GetTicketByIdAgenTdy/{agencia}")]
        public IActionResult FilterByIdAgency(int agencia)
        {
            //List<Ticket> dateticket = new List<Ticket>();

            try
            {

                DateTime fe = DateTime.UtcNow.AddHours(-04);
                Console.WriteLine(fe);
                var tickets = _dbcontext.Tickets
                .Where(t => t.FechaHora.Value.Date == fe.Date && t.IdAgencia == agencia)
                .Select(t =>
                    new
                    {
                        idTicket = t.IdTicket,
                        idTramite = t.IdTramite,
                        nomTramite = t.IdTramiteNavigation.NomTramite,
                        estado = t.Estado,
                        idVentanilla = t.IdVentanilla,
                        nomVentanilla = t.IdVentanillaNavigation.NomVentanilla,
                        fechaHora = t.FechaHora,
                        nTicket = t.IdTramiteNavigation.NomTramite + "-" + t.NTicket,
                        idPrioridad = t.IdPrioridad,
                        tipo = t.IdPrioridadNavigation.Tipo,
                        nomUsuario = t.IdUsuarioNavigation.NomUsuario,
                        idUsuario = t.IdUsuario,
                    
                    }
                   )
                .OrderByDescending(c => c.idTicket)
                .ToList();

                if (tickets == null)
                {
                    return BadRequest("Ticket no encontrado");
                }
                //requisito = (RequisitoTramite)_dbcontext.RequisitoTramites.Where(p => p.IdTramite == idtramite);
                //dateticket = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.Estado == estado && t.IdAgencia == agencia).Include(o => o.IdTramiteNavigation).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tickets });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        [HttpGet("GetPrint/{idticket}")]
        public IActionResult GetPrint(int idticket)
        {
            try
            {
                var tickets = _dbcontext.Tickets.Where(t => t.IdTicket == idticket).Select(t =>

                new
                {
                    fechaHora = t.FechaHora,
                    nomTramite = t.IdTramiteNavigation.NomTramite,
                    nTicket = t.NTicket
                }
               ).ToList();

                if (tickets == null)
                {
                    return BadRequest("Ticket no encontrado");
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tickets });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        //VENTANILLA Y DERIVACION
        //List<Ticket> dateticket = new List<Ticket>();
       
        [HttpGet("TicketStatus_2_5/{agencia}")]
        public IActionResult GetstatusTicketFilterWindows(int agencia)
        {
            
            try
            {

                DateTime fe = DateTime.UtcNow.AddHours(-04);
                Console.WriteLine(fe);
                var tickets = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.IdAgencia == agencia && (t.Estado == 2 || t.Estado == 5 || t.Estado == 3)).Select(t =>

                new
                {
                    
                    idTicket = t.IdTicket,
                    horalist = t.HoraList,
                    Estado = t.Estado,
                    fechaHora = t.FechaHora,
                    nomVentanilla = t.IdVentanillaNavigation.NomVentanilla,
                    nTicket = t.IdPrioridadNavigation.Tipo.Substring(0, 1)  + " - " + t.IdTramiteNavigation.NomTramite.Substring(0,3) + "-" + t.NTicket,
                    
                    idPrioridad = t.IdPrioridad,
                    nomPrioridad = t.IdPrioridadNavigation.Tipo
                    
            }
               ).OrderByDescending(t=>t.horalist).ToList();
                
                if (tickets == null)
                {
                    return BadRequest("Ticket no encontrado");
                }
                //requisito = (RequisitoTramite)_dbcontext.RequisitoTramites.Where(p => p.IdTramite == idtramite);
                //dateticket = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.Estado == estado && t.IdAgencia == agencia).Include(o => o.IdTramiteNavigation).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tickets });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        [HttpGet("TicketStatus_5/{agencia}")]
        public IActionResult getCaja(int agencia)
        {

            try
            {

                DateTime fe = DateTime.UtcNow.AddHours(-04);
                Console.WriteLine(fe);
                var tickets = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.IdAgencia == agencia && (t.Derivacion == 1 || t.Derivacion==2 ||t.Derivacion == 3 )).Select(t =>

                new
                {

                    idTicket = t.IdTicket,
                    horalist = t.HoraList,
                    Estado = t.Estado,
                    fechaHora = t.FechaHora,
                    nomVentanilla = t.IdVentanillaNavigation.NomVentanilla,
                    nTicket = t.IdPrioridadNavigation.Tipo.Substring(0, 1) + " - " + t.IdTramiteNavigation.NomTramite.Substring(0, 3) + "-" + t.NTicket,
                    derivacion = t.Derivacion,
                    idPrioridad = t.IdPrioridad,
                    nomPrioridad = t.IdPrioridadNavigation.Tipo

                }
               ).OrderByDescending(t => t.horalist).ToList();

                if (tickets == null)
                {
                    return BadRequest("Ticket no encontrado");
                }
                //requisito = (RequisitoTramite)_dbcontext.RequisitoTramites.Where(p => p.IdTramite == idtramite);
                //dateticket = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.Estado == estado && t.IdAgencia == agencia).Include(o => o.IdTramiteNavigation).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tickets });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }

        [HttpGet("CountTicketByAgency/{agencia}")]
        public IActionResult GetsCountTicketByAgency(int agencia)
        {

            try
            {

                DateTime fe = DateTime.UtcNow.AddHours(-04);
                Console.WriteLine(fe.Date);
                var tickets = _dbcontext.Tickets.Where(t=> t.IdAgencia == agencia ).Count();
                var ticketsDay = _dbcontext.Tickets.Where(t => t.IdAgencia == agencia && t.FechaHora.Value.Date == fe.Date).Count();
                var ticketsAttendedDay = _dbcontext.Tickets.Where(t => t.IdAgencia == agencia && t.FechaHora.Value.Date == fe.Date && t.Estado == 4).Count();
                var ticketsWaitDay = _dbcontext.Tickets.Where(t => t.IdAgencia == agencia && t.FechaHora.Value.Date == fe.Date && t.Estado == 1).Count();
                var ticketsNoAtendidosDay = _dbcontext.Tickets.Where(t => t.IdAgencia == agencia && t.Estado == 6).Count();
                var ticketsAttendedAll = _dbcontext.Tickets.Where(t => t.IdAgencia == agencia  && t.Estado == 4).Count();
                var ticketsWaitAll = _dbcontext.Tickets.Where(t => t.IdAgencia == agencia && t.Estado == 1).Count();
                var ticketsNoAtendidosAll = _dbcontext.Tickets.Where(t => t.IdAgencia == agencia  && t.Estado == 5).Count();
                var ticketCountUser = _dbcontext.Tickets.Where(t => t.IdAgencia == agencia && t.Estado == 4).GroupBy(t => t.IdUsuarioNavigation.NomUsuario).Select(t =>
         
                new
                {
                   NombreUser =t.Key.ToString(), 
                   conteo = t.Count(),
                    
                }
               ).ToList();
                if (tickets == null)
                {
                    return BadRequest("Ticket no encontrado");
                }
                //requisito = (RequisitoTramite)_dbcontext.RequisitoTramites.Where(p => p.IdTramite == idtramite);
                //dateticket = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.Estado == estado && t.IdAgencia == agencia).Include(o => o.IdTramiteNavigation).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success",
                    //conteo por agencia
                    countAllTicketAgency = tickets ,
                    countDayTicketAgency = ticketsDay,
                    countticketsAttendedDay = ticketsAttendedDay,
                    countticketsWaitDay = ticketsWaitDay,
                    countticketsNoAtendidosDay = ticketsNoAtendidosDay,
                    countticketsAttendedAll = ticketsAttendedAll,
                    countticketsWaitAll = ticketsWaitAll,
                    countticketsNoAtendidosAll = ticketsNoAtendidosAll,
                    CountUserAgency = ticketCountUser,
                });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        [HttpGet("CountTicketAll")]
        public IActionResult GetsCountTicketAll()
        {

            try
            {

                DateTime fe = DateTime.UtcNow.AddHours(-04);
                Console.WriteLine(fe.Date);

                var ticketsSUPERADMIN = _dbcontext.Tickets.Count();
                var ticketsDaySUPERADMIN = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date).Count();
                var ticketsAttendedDaySUPERADMIN = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.Estado == 4).Count();
                var ticketsWaitDaySUPERADMIN = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.Estado == 1).Count();
                var ticketsNoAtendidosDaySUPERADMIN = _dbcontext.Tickets.Where(t => t.Estado == 6).Count();
                var ticketsAttendedAllSUPERADMIN = _dbcontext.Tickets.Where(t => t.Estado == 4).Count();
                var ticketsWaitAllSUPERADMIN = _dbcontext.Tickets.Where(t => t.Estado == 1).Count();
                var ticketsNoAtendidosAllSUPERADMIN = _dbcontext.Tickets.Where(t => t.Estado == 6).Count();
                var ticketCountUserALL = _dbcontext.Tickets.Where(t => t.Estado == 4).GroupBy(t => t.IdUsuarioNavigation.NomUsuario).Select(t =>

               new
               {
                   NombreUser = t.Key.ToString(),
                   conteo = t.Count(),

               }
              ).ToList();
                if (ticketsSUPERADMIN == null)
                {
                    return BadRequest("Ticket no encontrado");
                }
                //requisito = (RequisitoTramite)_dbcontext.RequisitoTramites.Where(p => p.IdTramite == idtramite);
                //dateticket = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.Estado == estado && t.IdAgencia == agencia).Include(o => o.IdTramiteNavigation).ToList();

                return StatusCode(StatusCodes.Status200OK, new
                {
                    mensaje = "success",
                    //conteo superadmin
                    countAllAdmin = ticketsSUPERADMIN,
                    countDayAdmin = ticketsDaySUPERADMIN,
                    countAttendedDayAdmin = ticketsAttendedDaySUPERADMIN,
                    countWaitDayAdmin = ticketsWaitDaySUPERADMIN,
                    countNoAtendidosDayAdmin = ticketsNoAtendidosDaySUPERADMIN,
                    countAttendedAdmin = ticketsAttendedAllSUPERADMIN,
                    countticketsWaitAdmin = ticketsWaitAllSUPERADMIN,
                    countNoAtendidosAdmin = ticketsNoAtendidosAllSUPERADMIN,
                    CountUser = ticketCountUserALL
                });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        //VENTANILLA ATENCION
        [HttpGet("TicketStatus1235/{agencia}")]
        public IActionResult GetstatusTicketFilterVentanilla(int agencia)
        {
            //List<Ticket> dateticket = new List<Ticket>();

            try
            {

                DateTime fe = DateTime.UtcNow.AddHours(-04);
                Console.WriteLine(fe);
                var tickets = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.IdAgencia == agencia && (t.Estado == 1 || t.Estado == 2 || t.Estado == 3 || t.Estado == 5 || t.Estado == 7)).Select(t =>

                new
                {
                    idTicket = t.IdTicket,
                    idTramite = t.IdTramite,
                    nomTramite = t.IdTramiteNavigation.NomTramite,
                    estado = t.Estado,
                    idVentanilla = t.IdVentanilla, 
                    DuracionAtencion = t.DuracionAtencion,
                    HoraAtencion = t.HoraAtencion,
                    nomVentanilla = t.IdVentanillaNavigation.NomVentanilla,
                    fechaHora = t.FechaHora,
                    nTicket = t.IdTramiteNavigation.NomTramite + "-" + t.NTicket,
                    idPrioridad = t.IdPrioridad,
                    tipo = t.IdPrioridadNavigation.Tipo
                }
               ).ToList();

                if (tickets == null)
                {
                    return BadRequest("Ticket no encontrado");
                }
                //requisito = (RequisitoTramite)_dbcontext.RequisitoTramites.Where(p => p.IdTramite == idtramite);
                //dateticket = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.Estado == estado && t.IdAgencia == agencia).Include(o => o.IdTramiteNavigation).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tickets });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }

        [HttpPost("Ticket_Status1235/{agencia}")]
        public IActionResult GetFilterVentanilla(int agencia, [FromBody] List<int> ids)
        {
            DateTime fe = DateTime.UtcNow.AddHours(-04);

            Console.WriteLine("Aqui va el array prro" + fe);
            var tickets = _dbcontext.Tickets.Where(c => ids.Contains((int)c.IdTramite) && c.FechaHora.Value.Date == fe.Date && c.IdAgencia == agencia && (c.Estado == 1 || c.Estado == 2 || c.Estado == 3 || c.Estado == 5 || c.Estado == 7)).Select(t =>
            new
            {
                idTicket = t.IdTicket,
                idTramite = t.IdTramite,
                idUsuario = t.IdUsuario,
                nomTramite = t.IdTramiteNavigation.NomTramite,
                estado = t.Estado,
                idVentanilla = t.IdVentanilla,
                DuracionAtencion = t.DuracionAtencion,
                HoraAtencion = t.HoraAtencion,
                nomVentanilla = t.IdVentanillaNavigation.NomVentanilla,
                fechaHora = t.FechaHora,
                nTicket = t.IdTramiteNavigation.NomTramite + "-" + t.NTicket,
                idPrioridad = t.IdPrioridad,
                tipo = t.IdPrioridadNavigation.Tipo,
                derivacion = t.Derivacion,
            })
                .OrderBy(a =>  a.idPrioridad )
                .ToList();
            return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tickets });
        }

        [HttpPost]
        public IActionResult Guardar([FromBody] Ticket objeto)
        {
            //asignacion de numeracion deacuerdo a tipo de tramite
            List<Ticket> dateticket = new List<Ticket>();
            DateTime fe = DateTime.UtcNow.AddHours(-04);
            Console.WriteLine(fe);
            Console.WriteLine(objeto.IdTramite);
            //requisito = (RequisitoTramite)_dbcontext.RequisitoTramites.Where(p => p.IdTramite == idtramite);
            dateticket = _dbcontext.Tickets.Where(t => t.FechaHora.Value.Date == fe.Date && t.IdTramite == objeto.IdTramite).Include(r => r.IdTramiteNavigation).ToList();
            Console.WriteLine(dateticket.Count());
            //fin de asignacion de numeracion a tipo de tramite
            try
            {
                _dbcontext.Tickets.Add(objeto);
                objeto.FechaHora = fe;     
                objeto.NTicket = dateticket.Count()+1;
                _dbcontext.SaveChanges();
                var id = objeto.IdTicket;
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente",id = id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult Edit([FromBody] Ticket objeto)
        {
            Ticket tickets = _dbcontext.Tickets.Find(objeto.IdTicket);

            if (tickets == null)
            {
                return BadRequest("Ticket no encontrado");
            }
            try
            {

                tickets.IdTramite = objeto.IdTramite is null ? tickets.IdTramite : objeto.IdTramite;
                tickets.IdUsuario = objeto.IdUsuario is null ? tickets.IdUsuario : objeto.IdUsuario;
                tickets.Estado = objeto.Estado is null ? tickets.Estado : objeto.Estado;
                tickets.IdVentanilla = objeto.IdVentanilla is null ? tickets.IdVentanilla : objeto.IdVentanilla;
                
                
                //tickets.FechaHora = objeto.FechaHora is null ? tickets.FechaHora : objeto.FechaHora;
                if (objeto.Estado == 2 || objeto.Derivacion == 2)
                {
                    DateTime fe = DateTime.UtcNow.AddHours(-04);
                    string hourActual = fe.TimeOfDay.ToString();
                    tickets.HoraList = TimeOnly.Parse(hourActual);
                }
                if (objeto.Estado == 3)
                {
                    DateTime fe = DateTime.UtcNow.AddHours(-04);
                    string hourActual = fe.TimeOfDay.ToString();
                    tickets.HoraAtencion = TimeOnly.Parse(hourActual);
                    int second = fe.Second;
                    Console.WriteLine("aca van los segundos : ");
                    Console.WriteLine(second);

                }

                    // objeto.HoraAtencion is null ? tickets.HoraAtencion : objeto.HoraAtencion;
                    //tickets.DuracionAtencion = objeto.DuracionAtencion is null ? tickets.DuracionAtencion : objeto.DuracionAtencion;
                    tickets.IdAgencia = objeto.IdAgencia is null ? tickets.IdAgencia : objeto.IdAgencia;
                tickets.NTicket =  objeto.NTicket is null ? tickets.NTicket : objeto.NTicket;
                tickets.IdPrioridad = objeto.IdPrioridad is null ? tickets.IdPrioridad : objeto.IdPrioridad;
                tickets.Derivacion = objeto.Derivacion is null ? tickets.Derivacion : objeto.Derivacion;
                _dbcontext.Tickets.Update(tickets);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Actualizado Satisfactoriamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
    }
}
