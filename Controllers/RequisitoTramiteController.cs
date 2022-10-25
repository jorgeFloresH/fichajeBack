using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitoTramiteController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;
        public RequisitoTramiteController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        public IActionResult requisitoTramite()
        {
            List<RequisitoTramite> requisitos = new List<RequisitoTramite>();
            try
            {
                requisitos = _dbcontext.RequisitoTramites.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = requisitos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("{id:long}")]
        public IActionResult Getrequisito(long id)
        {
            try
            {

                var TraReque = _dbcontext.RequisitoTramites.Where(t => t.IdRequisitos == id).Select(t =>
               new
               {
                   idRequisitoTramite = t.IdRequitram,
                   idTramite = t.IdTramite,
                   nomTramite = t.IdTramiteNavigation.NomTramite,
                   idRequisitos = t.IdRequisitos,
                   nomRequisitos = t.IdRequisitosNavigation.NomRequisitos
               }
               ).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = TraReque });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("idtramite/{idtramite:long}")]
        public IActionResult GetFiltertramite(int idtramite)
        {

            try
            {

                var TraReque = _dbcontext.RequisitoTramites.Where(t => t.IdTramite == idtramite).Select(t =>
               new
               {
                   idRequisitoTramite = t.IdRequitram,
                   idTramite = t.IdTramite,
                   nomTramite = t.IdTramiteNavigation.NomTramite,
                   idRequisitos = t.IdRequisitos,
                   nomRequisitos = t.IdRequisitosNavigation.NomRequisitos
               }
               ).OrderBy(t=>t.idRequisitoTramite).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = TraReque });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("idrequisitos/{idrequisito:long}")]
        public IActionResult GetFilterRequisitos(int idrequisito)
        {

            try
            {

               var TraReque = _dbcontext.RequisitoTramites.Where(t => t.IdRequisitos == idrequisito).Select(t =>
               new
               {
                   idRequisitoTramite = t.IdRequitram,
                   idTramite = t.IdTramite,
                   nomTramite = t.IdTramiteNavigation.NomTramite,
                   idRequisitos = t.IdRequisitos,
                   nomRequisitos = t.IdRequisitosNavigation.NomRequisitos
               }
               ).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = TraReque });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPost]
        public IActionResult Guardar([FromBody] RequisitoTramite objeto)
        {
            try
            {
                _dbcontext.RequisitoTramites.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult Edit([FromBody] RequisitoTramite objeto)
        {
            RequisitoTramite requisito = _dbcontext.RequisitoTramites.Find(objeto.IdRequitram);
            if (requisito == null)
            {
                return BadRequest("Requisito no encontrado");
            }
            try
            {

                requisito.IdRequisitos = objeto.IdRequisitos is null ? requisito.IdRequisitos : objeto.IdRequisitos;
                requisito.IdTramite = objeto.IdTramite is null ? requisito.IdTramite : objeto.IdTramite;
                _dbcontext.RequisitoTramites.Update(requisito);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Actualizado Satisfactoriamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpDelete("{id:long}")]
        public  IActionResult Delete(long id)
        {
            try
            {
                _dbcontext.RequisitoTramites.Where(c => c.IdTramite == id)
               .ToList().ForEach(p => _dbcontext.RequisitoTramites.Remove(p));
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Delete Successfull", idTramite=id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpDelete("ReTra/{id:long}")]
        public IActionResult Delete2(long id)
        {
            try
            {
                _dbcontext.RequisitoTramites.Where(c => c.IdRequisitos == id)
               .ToList().ForEach(p => _dbcontext.RequisitoTramites.Remove(p));
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Delete Successfull", idRequisitos = id });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }


    }
}
