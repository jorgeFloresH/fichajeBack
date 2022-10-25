using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoPerfilController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;

        public TipoPerfilController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        public IActionResult tipoperfil()
        {
            try
            {

                var tipoperfils = _dbcontext.TipoPerfils.Select(t =>
               new
               {
                   idPerfil = t.IdPerfil,
                   nomTipoP = t.NomTipoP,
                   estado = t.Estado
               }
               ).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tipoperfils });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("{id:long}")]
        public IActionResult GetTipoPerfil(long id)
        {
            TipoPerfil tipoperfil = _dbcontext.TipoPerfils.Find(id);
            if (tipoperfil == null)
            {
                return BadRequest("Perfil no encontrado");
            }
            try
            {
                tipoperfil = _dbcontext.TipoPerfils.Find(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = tipoperfil });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPost]
        public IActionResult Guardar([FromBody] TipoPerfil objeto)
        {
            try
            {
                _dbcontext.TipoPerfils.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult Edit([FromBody] TipoPerfil objeto)
        {
            TipoPerfil tipoperfil = _dbcontext.TipoPerfils.Find(objeto.IdPerfil);
            if (tipoperfil == null)
            {
                return BadRequest("tipo perfil no encontrado");
            }
            try
            {

                tipoperfil.NomTipoP = objeto.NomTipoP is null ? tipoperfil.NomTipoP : objeto.NomTipoP;
                tipoperfil.Estado = objeto.Estado is null ? tipoperfil.Estado : objeto.Estado;
                _dbcontext.TipoPerfils.Update(tipoperfil);
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
