using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using apiServices.Models;
using Microsoft.AspNetCore.Cors;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class VentanillaController : ControllerBase
    {
        public readonly siscolasgamcContext _dbcontext;
        public VentanillaController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        public IActionResult ventanilla()
        {
            
            try
            {
               var ventanillas = _dbcontext.Ventanillas.Select(a => new
                {
                    idVentanilla = a.IdVentanilla,
                    nomVentanilla = a.NomVentanilla,
                    estadoV = a.EstadoV,
                    idAgencia = a.IdAgencia,
                    nomAgencia = a.IdAgenciaNavigation.NomAgencia,

                }).OrderBy(t=>t.idVentanilla).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = ventanillas });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("{id:long}")]
        public IActionResult Getventanilla(long id)
        {
            Ventanilla ventanilla = _dbcontext.Ventanillas.Find(id);
            if (ventanilla == null)
            {
                return BadRequest("Ventanilla no encontrado");
            }
            try
            {
                ventanilla = _dbcontext.Ventanillas.Find(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = ventanilla });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        //VENTANILLA filter por agencia
        [HttpGet("WindowsFilter/{agencia}")]
        public IActionResult GetFilterWindows(int agencia)
        {
            try
            {
                var ventanilla = _dbcontext.Ventanillas.Where(t => t.IdAgencia == agencia).Select(t =>

                new
                {
                    idVentanilla = t.IdVentanilla,
                    nomVentanilla = t.NomVentanilla,
                    estadoV = t.EstadoV
                    
                }
               ).OrderBy(a => a.idVentanilla).ToList();

                if (ventanilla == null)
                {
                    return BadRequest("Ventanilla no encontrado");
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = ventanilla });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        [HttpGet("filterByAgEs/{agencia}")]
        public IActionResult GetFilWindow(int agencia)
        {
            try
            {
                var ventanilla = _dbcontext.Ventanillas.Where(t => t.IdAgencia == agencia && t.EstadoV == 1).Select(t =>

                new
                {
                    idVentanilla = t.IdVentanilla,
                    nomVentanilla = t.NomVentanilla,
                    estadoV = t.EstadoV

                }
               ).OrderBy(t=>t.idVentanilla).ToList();

                if (ventanilla == null)
                {
                    return BadRequest("Ventanilla no encontrado");
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = ventanilla });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        [HttpPost]
        public IActionResult Guardar([FromBody] Ventanilla objeto)
        {
            try
            {
                _dbcontext.Ventanillas.Add(objeto);
                _dbcontext.SaveChanges();

                var idV = objeto.IdVentanilla;
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente", idVentnilla = idV });

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult Edit([FromBody] Ventanilla objeto)
        {
            Ventanilla ventanilla = _dbcontext.Ventanillas.Find(objeto.IdVentanilla);
            if (ventanilla == null)
            {
                return BadRequest("Ventanilla no encontrado");
            }
            try
            {

                ventanilla.NomVentanilla = objeto.NomVentanilla is null ? ventanilla.NomVentanilla : objeto.NomVentanilla;
                ventanilla.EstadoV = objeto.EstadoV is null ? ventanilla.EstadoV : objeto.EstadoV;
                _dbcontext.Ventanillas.Update(ventanilla);
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
