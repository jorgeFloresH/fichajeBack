using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class TraVenController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;

        public TraVenController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        public IActionResult TraVenGetAll()
        {
            //List<Ticket> tickets = new List<Ticket>();
            try
            {

                var TramitesVentanillas = _dbcontext.TraVens.Select(t =>
               new
               {
                   idTraVen = t.IdTranVen,
                   idTramite = t.IdTramite,
                   idVentanilla = t.IdVentanilla
               }
               ).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = TramitesVentanillas });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("SelectIdVen/{idVentanilla}")]
        public IActionResult GetSelectForIdVen(int idVentanilla)
        {
            //List<Ticket> dateticket = new List<Ticket>();

            try
            {

                //DateTime fe = DateTime.UtcNow.AddHours(-04);
                //Console.WriteLine(fe);
                var IdVenTraVen = _dbcontext.TraVens.Where(t => t.IdVentanilla == idVentanilla).Select(t =>
                new
                {
                    idVentanilla = t.IdVentanilla,
                    nomVentanilla = t.IdVentanillaNavigation.NomVentanilla,
                    estadoV = t.IdVentanillaNavigation.EstadoV,
                    idAgencia = t.IdVentanillaNavigation.IdAgencia,
                    idTramite = t.IdTramite,
                    nomTramite = t.IdTramiteNavigation.NomTramite,
                    estadoT = t.IdTramiteNavigation.Estado
                }
               ).ToList();

                if (IdVenTraVen == null)
                {
                    return BadRequest("Ticket no encontrado");
                }
                
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = IdVenTraVen });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        [HttpPost]
        public IActionResult InsertTraVen([FromBody] TraVen agen)
        {
            try
            {
                _dbcontext.TraVens.Add(agen);
                //agen.FechaCreacion = DateTime.UtcNow;
                _dbcontext.SaveChanges();
                var idTraVen = agen.IdTranVen;
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente", idUtTramite = idTraVen });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpDelete("{id:long}")]
        public IActionResult Delete(long id)
        {
            try
            {
                _dbcontext.TraVens.Where(c => c.IdVentanilla == id)
               .ToList().ForEach(p => _dbcontext.TraVens.Remove(p));
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Delete Successfull", idVentanilla = id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
    }
    
}
