using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class User_TicketController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;

        public User_TicketController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        public IActionResult getUserTicket()
        {
            try
            {

               var idsUserTicket = _dbcontext.UserTickets.Select(t =>
               new
               {
                   idUserTicket = t.IdUserTicket,
                   idTicket = t.IdTicket,
                   idUsuario = t.IdUsuario
               }
               ).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = idsUserTicket });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPost]
        public IActionResult postUserTicket([FromBody] UserTicket objeto)
        {
            try
            {
                _dbcontext.UserTickets.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente"});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult putUsertTicket([FromBody] UserTicket agen)
        {
            UserTicket idUsTick = _dbcontext.UserTickets.Find(agen.IdUserTicket);
            if (idUsTick == null)
            {
                return BadRequest("Agencia no  Actualizada");
            }
            try
            {
                idUsTick.IdTicket = agen.IdTicket is null ? idUsTick.IdTicket : agen.IdTicket;
                idUsTick.IdUsuario = agen.IdUsuario is null ? idUsTick.IdUsuario : agen.IdUsuario;

                _dbcontext.UserTickets.Update(idUsTick);
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
