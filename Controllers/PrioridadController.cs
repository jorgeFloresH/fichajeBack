using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class PrioridadController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;

        public PrioridadController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        public IActionResult prioridad()
        {
            try
            {

                var prioridad = _dbcontext.Prioridads.Select(t =>
               new
               {
                   idPrioridad = t.IdPrioridad,
                   tipo = t.Tipo,
                   rango = t.Rango
               }
               ).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = prioridad });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("{id:long}")]
        public IActionResult GetPrioridades(long id)
        {
            //Prioridad prioridades = _dbcontext.Prioridads.Find(id);
            
            try
            {
               var prioridades = _dbcontext.Prioridads.Select(t =>
               new
               {
                   idPrioridad = t.IdPrioridad,
                   tipo = t.Tipo,
                   rango = t.Rango
               }
               ).Where(p => p.idPrioridad == id).ToList();
                if (prioridades == null)
                {
                    return BadRequest("Prioridad no encontrada");
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = prioridades });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPost]
        public IActionResult Guardar([FromBody] Prioridad objeto)
        {
            try
            {
                _dbcontext.Prioridads.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult Edit([FromBody] Prioridad objeto)
        {
            Prioridad prioridades = _dbcontext.Prioridads.Find(objeto.IdPrioridad);
            if (prioridades == null)
            {
                return BadRequest("Prioridad no encontrada");
            }
            try
            {

                prioridades.Tipo = objeto.Tipo is null ? prioridades.Tipo : objeto.Tipo;
                prioridades.Rango = objeto.Rango is null ? prioridades.Rango : objeto.Rango;
                _dbcontext.Prioridads.Update(prioridades);
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
