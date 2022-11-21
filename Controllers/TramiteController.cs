using apiServices.Data.Filters;
using apiServices.Data.Queries;
using apiServices.Domain;
using apiServices.Models;
using apiServices.Services;
using AutoMapper;
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
        private readonly ITramiteService _tramiteService;
        private readonly ITramitePageService _tramitePageService;
        private readonly IMapper _mapper;

        public TramiteController(siscolasgamcContext _context, ITramiteService dtService, IMapper mapper, ITramitePageService servicio)
        {
            _dbcontext = _context;
            _tramiteService = dtService;
            _mapper = mapper;
            _tramitePageService = servicio;
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

        //-----------------------------------------------------------------------------------------------
        [HttpGet("paginacion/")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] TramiteQuery query)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = new TramiteFilter();
            filter.nombreTramite = query.nombreTramite;
            filter.nombreAgencia = query.nombreAgencia;
            filter.sort = query.sort;
            var dtResponse = await _tramiteService.GetTramiteAsync(filter, pagination);
            var paginas = await _tramitePageService.GetTramitePageAsync(filter, pagination);
            return Ok(new { data = dtResponse, paginas });
        }
        //-----------------------------------------------------------------------------------------------

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
