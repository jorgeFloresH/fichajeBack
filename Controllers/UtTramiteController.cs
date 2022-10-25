using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class UtTramiteController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;
        public UtTramiteController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        public IActionResult AllUt_T()
        {
            try
            {
                var uttramite = _dbcontext.UtTramites.Select(t =>
                new
                {
                   id_ut_tramite = t.IdUtTramite,
                   idUsuario = t.IdUsuario,
                   idTramite = t.IdTramite
                }
                ).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = uttramite });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        //get for idUser 
        [HttpGet("Filter_UtTramite/{idUsiario}")]
        public IActionResult GetTicketUtTramite(int idUsiario)
        {
            //List<Ticket> dateticket = new List<Ticket>();

            try
            {

                //DateTime fe = DateTime.UtcNow.AddHours(-04);
                //Console.WriteLine(fe);
                var tickets = _dbcontext.UtTramites.Where(t =>  t.IdUsuario == idUsiario  || t.IdUsuarioNavigation.CiUsuario == idUsiario).Select(t =>

                new
                {
                    idUtTramite = t.IdUtTramite,
                    idTramite = t.IdTramite,
                    nomTramite = t.IdTramiteNavigation.NomTramite
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
        [HttpPost]
        public IActionResult InsertUtT([FromBody] UtTramite agen)
        {
            try
            {
                _dbcontext.UtTramites.Add(agen);
                //agen.FechaCreacion = DateTime.UtcNow;
                _dbcontext.SaveChanges();
                var idUtT = agen.IdUtTramite;
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente", idUtTramite = idUtT });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult idEditUtT([FromBody] UtTramite agen)
        {
            UtTramite idUtT = _dbcontext.UtTramites.Find(agen.IdUtTramite);
            if (idUtT == null)
            {
                return BadRequest("Ut T no Actualizada");
            }
            try
            {
               //idUtT.IdUsuario = agen.IdUsuario is null ? idUtT.IdUsuario : agen.IdUtTramite;
                idUtT.IdTramite = agen.IdTramite is null ? idUtT.IdTramite : agen.IdTramite;
                _dbcontext.UtTramites.Update(idUtT);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Actualizado Satisfactoriamente" });
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
                _dbcontext.UtTramites.Where(c => c.IdUsuario == id)
               .ToList().ForEach(p => _dbcontext.UtTramites.Remove(p));
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Delete Successfull", idUsuario = id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }

    }
}
