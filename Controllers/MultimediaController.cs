using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class MultimediaController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;

        public MultimediaController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        public IActionResult multimedia()
        {
            try
            {

                var multimedia = _dbcontext.Multimedia.Select(t =>
               new
               {
                   IdMulti = t.IdMulti,
                   NomVideo = t.NomVideo,
                   Estado = t.Estado,
                   Tipo = t.Tipo,
                   Ruta = t.Ruta,
                   IdAgencia = t.IdAgencia
               }
               ).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = multimedia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("{id:long}")]
        public IActionResult GetMultimedia(long id)
        {
            //Prioridad prioridades = _dbcontext.Prioridads.Find(id);

            try
            {
                var multimedia = _dbcontext.Multimedia.Select(t =>
                new
                {
                    IdMulti = t.IdMulti,
                    NomVideo = t.NomVideo,
                    Estado = t.Estado,
                    Tipo = t.Tipo,
                    Ruta = t.Ruta,
                    IdAgencia = t.IdAgencia
                }
                ).Where(p => p.IdMulti == id).ToList();
                if (multimedia == null)
                {
                    return BadRequest("Multimedia no encontrada");
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = multimedia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPost]
        public IActionResult Guardar([FromForm] List<IFormFile> objetos)
        {
            List<Multimedium> videos = new List<Multimedium>();
            try
            {
                if(objetos.Count > 0)
                {
                    foreach (var objeto in objetos)
                    {
                        var filePath = "C:\\Users\\DANGO\\Documents\\SCOLAS\\BACK\\NEWmiercoles\\s.c.g.c-back\\videos\\" + objeto.FileName;
                        using (var stream = System.IO.File.Create(filePath))
                        {
                            objeto.CopyToAsync(stream);
                        }
                        Multimedium video = new Multimedium();
                        video.NomVideo = Path.GetFileNameWithoutExtension(objeto.FileName);
                        video.Ruta = filePath;
                        video.Estado = 1;
                        videos.Add(video);
                    }
                    _dbcontext.Multimedia.AddRange(videos);
                    _dbcontext.SaveChanges();
                   
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult Edit([FromBody] Multimedium objeto)
        {
            Multimedium multimedia = _dbcontext.Multimedia.Find(objeto.IdMulti);
            if (multimedia == null)
            {
                return BadRequest("Multimeida no encontrada");
            }
            try
            {
                multimedia.Tipo = objeto.Tipo is null ? multimedia.Tipo : objeto.Tipo;
                multimedia.Ruta = objeto.Ruta is null ? multimedia.Ruta : objeto.Ruta;
                multimedia.IdAgencia = objeto.IdAgencia is null ? multimedia.IdAgencia : objeto.IdAgencia;
                multimedia.NomVideo = objeto.NomVideo is null ? multimedia.NomVideo : objeto.NomVideo;
                multimedia.Estado = objeto.Estado is null ? multimedia.Estado : objeto.Estado;
                _dbcontext.Multimedia.Update(multimedia);
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
