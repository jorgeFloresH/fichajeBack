using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class TramiteController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;

        public TramiteController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        public IActionResult tramite()
        {
            try
            {

                var tramites = _dbcontext.Tramites.Select(t =>
               new
               {
                   idTramite = t.IdTramite,
                   idAgencia = t.IdAgencia,
                   nomTramite = t.NomTramite,
                   estado = t.Estado,
                   NomAgencia = t.IdAgenciaNavigation.NomAgencia
               }
               ).OrderBy(a=>a.idTramite).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tramites });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        //Filter for agency
        [HttpGet("FilterAgencia/{agencia}")]
        public IActionResult GetFiltertramite(int agencia)
        {

            try
            {

                var tramites = _dbcontext.Tramites.Where(t => t.IdAgencia == agencia).Select(t =>
               new
               {
                   idTramite = t.IdTramite,
                   idAgencia = t.IdAgencia,
                   nomTramite = t.NomTramite,
                   estado = t.Estado,
                   NomAgencia = t.IdAgenciaNavigation.NomAgencia
               }
               ).OrderBy(a=>a.idTramite).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tramites});
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("{id:long}")]
        public IActionResult GetTramitesFilterID(long id)
        {
            try
            {

                var tramites = _dbcontext.Tramites.Where(t => t.IdTramite == id).Select(t =>
               new
               {
                   idTramite = t.IdTramite,
                   idAgencia = t.IdAgencia,
                   nomTramite = t.NomTramite,
                   estado = t.Estado,
                   NomAgencia = t.IdAgenciaNavigation.NomAgencia
               }
               ).OrderBy(a => a.idTramite).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tramites });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPost]
        public IActionResult Guardar([FromBody] Tramite objeto)
        {
            try
            {
                _dbcontext.Tramites.Add(objeto);
                _dbcontext.SaveChanges();
                var idT = objeto.IdTramite;
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente", idTramite = idT });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult Edit([FromBody] Tramite objeto)
        {
            Tramite tramites = _dbcontext.Tramites.Find(objeto.IdTramite);
            if (tramites == null)
            {
                return BadRequest("Tramite no encontrado");
            }
            try
            {

                tramites.IdAgencia = objeto.IdAgencia is null ? tramites.IdAgencia : objeto.IdAgencia;
                tramites.NomTramite = objeto.NomTramite is null ? tramites.NomTramite : objeto.NomTramite;
                tramites.Estado = objeto.Estado is null ? tramites.Estado : objeto.Estado;
                _dbcontext.Tramites.Update(tramites);
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
