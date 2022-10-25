using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ContactoController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;

        public ContactoController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        public IActionResult contacto()
        {
            try
            {

                var contactos = _dbcontext.Contactos.Select(t =>
               new
               {
                  idContacto=t.IdContacto ,
                  correo=t.Correo,
                  nCelular=t.NCelular,
                  idUsuario=t.IdUsuario 
               }
               ).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = contactos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("{id:long}")]
        public IActionResult GetContactos(long id)
        {
            try
            {
                var contactos = _dbcontext.Contactos.Select(t =>
                new
                {
                    idContacto = t.IdContacto,
                    correo = t.Correo,
                    nCelular = t.NCelular,
                    idUsuario = t.IdUsuario
                }
                ).Where(p => p.idContacto == id).ToList();
                if (contactos == null)
                {
                    return BadRequest("Prioridad no encontrada");
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = contactos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("FilterIdcliente/{idcliente}")]
        public IActionResult GetstatusTicketFilter(int idcliente)
        {
            try
            {
                var contactos = _dbcontext.Contactos.Where(t => t.IdUsuario == idcliente).Select(t =>

                new
                {
                    idContacto = t.IdContacto,
                    correo = t.Correo,
                    nCelular = t.NCelular,
                    idUsuario = t.IdUsuario
                }
               ).ToList();

                if (contactos == null)
                {
                    return BadRequest("contactos no encontrados");
                }
                

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = contactos });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        [HttpPost]
        public IActionResult Guardar([FromBody] Contacto objeto)
        {
            try
            {
                _dbcontext.Contactos.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult Edit([FromBody] Contacto objeto)
        {
            Contacto contactos = _dbcontext.Contactos.Find(objeto.IdContacto);
            if (contactos == null)
            {
                return BadRequest("Contacto no encontrado");
            }
            try
            {

                contactos.Correo = objeto.Correo is null ? contactos.Correo : objeto.Correo;
                contactos.NCelular = objeto.NCelular is null ? contactos.NCelular : objeto.NCelular;
                contactos.IdUsuario = objeto.IdUsuario is null ? contactos.IdUsuario : objeto.IdUsuario;
                _dbcontext.Contactos.Update(contactos);
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
