using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Net.Http.Headers;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using apiServices.Models;
using apiServices.Data.Queries;
using apiServices.Services;
using AutoMapper;
using apiServices.Domain;
using apiServices.Data.Filters;

namespace apiServices.Controllers
{
    /// <summary>
    /// controller for upload large file
    /// </summary>
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly ILogger<FileUploadController> _logger;
        public readonly siscolasgamcContext _dbcontext;
        private readonly IMultimediaService _multimediaService;
        private readonly IMultimediaPageService _multimediaPageService;
        private readonly IMultimediaServiceSuper _multimediaServiceSuper;
        private readonly IMultimediaPageServiceSuper _multimediaPageServiceSuper;
        private readonly IMapper _mapper;

        public FileUploadController(
            ILogger<FileUploadController> logger,
            siscolasgamcContext _context,
            IMultimediaService dtService,
            IMapper mapper,
            IMultimediaPageService servicio,
            IMultimediaServiceSuper servicioSuper,
            IMultimediaPageServiceSuper servicioPagina
            )
        {
            _logger = logger;
            _dbcontext = _context;
            _multimediaService = dtService;
            _multimediaPageService = servicio;
            _multimediaServiceSuper = servicioSuper;
            _multimediaPageServiceSuper = servicioPagina;
            _mapper = mapper;
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
                   IdAgencia = t.IdAgencia,
                   nomAgencia = t.IdAgenciaNavigation.NomAgencia,
               }
               ).OrderByDescending(a => a.NomVideo).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = multimedia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }

        [HttpGet("getNomVideo")]
        public IActionResult getNameVideo()
        {
            try
            {
                var multimedia = _dbcontext.Multimedia
                    .GroupBy(a => a.NomVideo)
                    .Select(t =>
                       new
                       {

                           NomVideo = t.Key.ToString(),
                           idMulti = t.Select(p => new
                           {
                               IdMulti = p.IdMulti,
                               Estado = p.Estado,
                               Tipo = p.Tipo,
                               Ruta = p.Ruta,
                               IdAgencia = p.IdAgencia,
                               nomAgencia = p.IdAgenciaNavigation.NomAgencia
                           }),

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
                ).OrderByDescending(a => a.IdMulti).Where(p => p.IdMulti == id).ToList();
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
        [HttpGet("GetByIdAgen/{idAgencia:long}")]
        public IActionResult GetByIdAgen(long idAgencia)
        {
            //Prioridad prioridades = _dbcontext.Prioridads.Find(id);
            try
            {
                var multimedia = _dbcontext.Multimedia.Select(t =>
                new
                {
                    idMulti = t.IdMulti,
                    nomVideo = t.NomVideo,
                    estado = t.Estado,
                    tipo = t.Tipo,
                    ruta = t.Ruta,
                    idAgencia = t.IdAgencia
                }
                ).Where(p => p.idAgencia == idAgencia).OrderByDescending(a => a.idMulti).ToList();
                if (multimedia == null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = multimedia });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = multimedia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        //-----------------------------------------------------------------------------------------------
        [HttpGet("paginacion/")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] MultimediaQuery query)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = new MultimediaFilter();
            filter.nombreVideo = query.nombreVideo;
            filter.sort = query.sort;
            var dtResponse = await _multimediaServiceSuper.GetMultimediaSuperAsync(filter, pagination);
            var paginas = await _multimediaPageServiceSuper.GetMultimediaPageAsync(filter, pagination);
            return Ok(new { data = dtResponse, paginas});
        }
        //-----------------------------------------------------------------------------------------------
        [HttpGet("paginacion/agencia/{nombreMulti}")]
        public async Task<IActionResult> GetAllAgency([FromQuery] PaginationQuery paginationQuery, [FromQuery] MultimediaQuery query, string nombreMulti)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = new MultimediaFilter();
            filter.nombreVideo = query.nombreVideo;
            filter.sort = query.sort;
            filter.buscarNomV = nombreMulti;
            var dtResponse = await _multimediaService.GetMultimediaAsync(filter, pagination);
            var paginas = await _multimediaPageService.GetMultimediaPageAsync(filter, pagination);
            return Ok(new { data = dtResponse, paginas });
        }
        //-----------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------
        [HttpGet("paginacion/{agencia}")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] MultimediaQuery query, int agencia)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = new MultimediaFilter();
            filter.nombreVideo = query.nombreVideo;
            filter.sort = query.sort;
            filter.idAgencia = agencia;
            var dtResponse = await _multimediaService.GetMultimediaAsync(filter, pagination);
            var paginas = await _multimediaPageService.GetMultimediaPageAsync(filter, pagination);
            return Ok(new { data = dtResponse, paginas });
        }
        //-----------------------------------------------------------------------------------------------


        [HttpGet("GetByIdAgEs/{idAgencia:long}")]
        public IActionResult GetMultimediaByIdAgen(long idAgencia)
        {
            //Prioridad prioridades = _dbcontext.Prioridads.Find(id);
            try
            {
                var multimedia = _dbcontext.Multimedia.Select(t =>
                new
                {
                    idMulti = t.IdMulti,
                    nomVideo = t.NomVideo,
                    estado = t.Estado,
                    tipo = t.Tipo,
                    ruta = t.Ruta,
                    idAgencia = t.IdAgencia
                }
                ).Where(p => p.idAgencia == idAgencia && p.estado == 1 && p.tipo == "1").OrderByDescending(a => a.idMulti).ToList();
                if (multimedia == null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = multimedia });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = multimedia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("GetByIdAgTipe/{idAgencia:long}")]
        public IActionResult getMutltiByTipeIdAgn(long idAgencia)
        {
            //Prioridad prioridades = _dbcontext.Prioridads.Find(id);
            try
            {
                var multimedia = _dbcontext.Multimedia.Select(t =>
                new
                {
                    idMulti = t.IdMulti,
                    nomVideo = t.NomVideo,
                    estado = t.Estado,
                    tipo = t.Tipo,
                    ruta = t.Ruta,
                    idAgencia = t.IdAgencia
                }
                ).Where(p => p.idAgencia == idAgencia && p.estado == 1 && p.tipo == "0").OrderByDescending(a => a.idMulti).ToList();
                if (multimedia == null)
                {
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = multimedia });
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = multimedia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        /// <summary>
        /// Action for upload large file
        /// </summary>
        /// <remarks>
        /// Request to this action will not trigger any model binding or model validation,
        /// because this is a no-argument action
        /// </remarks>
        /// <returns></returns>

        [HttpPost("addVideoAgency")]
        public IActionResult addVideAnAgency([FromBody] Multimedium agen)
        {
            try
            {
                _dbcontext.Multimedia.Add(agen);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPost]
        [Route(nameof(UploadLargeFile))]
        public async Task<IActionResult> UploadLargeFile()
        {
            var request = HttpContext.Request;
            List<Multimedium> videos = new List<Multimedium>();
            // validation of Content-Type
            // 1. first, it must be a form-data request
            // 2. a boundary should be found in the Content-Type
            if (!request.HasFormContentType ||
                !MediaTypeHeaderValue.TryParse(request.ContentType, out var mediaTypeHeader) ||
                string.IsNullOrEmpty(mediaTypeHeader.Boundary.Value))
            {
                return new UnsupportedMediaTypeResult();
            }

            var reader = new MultipartReader(mediaTypeHeader.Boundary.Value, request.Body);
            var section = await reader.ReadNextSectionAsync();

            // This sample try to get the first file from request and save it
            // Make changes according to your needs in actual use
            while (section != null)
            {
                var hasContentDispositionHeader = ContentDispositionHeaderValue.TryParse(section.ContentDisposition,
                    out var contentDisposition);

                if (hasContentDispositionHeader && contentDisposition.DispositionType.Equals("form-data") &&
                    !string.IsNullOrEmpty(contentDisposition.FileName.Value))
                {
                    // Don't trust any file name, file extension, and file data from the request unless you trust them completely
                    // Otherwise, it is very likely to cause problems such as virus uploading, disk filling, etc
                    // In short, it is necessary to restrict and verify the upload
                    // Here, we just use the temporary folder and a random file name

                    // Get the temporary folder, and combine a random file name with it
                    var fileName = contentDisposition.FileName.Value;
                    var saveToPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyVideos) + "/VideosSisColas/", fileName);

                    using (var targetStream = System.IO.File.Create(saveToPath))
                    {
                        await section.Body.CopyToAsync(targetStream);
                    }
                    Multimedium video = new Multimedium();
                    video.NomVideo = fileName;
                    video.Ruta = saveToPath;
                    video.Estado = 1;
                    videos.Add(video);
                    _dbcontext.Multimedia.AddRange(videos);
                    _dbcontext.SaveChanges();
                    return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", videoID = video.IdMulti });
                }

                section = await reader.ReadNextSectionAsync();
            }

            // If the code runs to this location, it means that no files have been saved
            return BadRequest("No files data in the request.");
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