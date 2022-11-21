using apiServices.Data;
using apiServices.Data.Queries;
using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using apiServices.Domain;
using apiServices.Data.Filters;
using apiServices.Services;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;
        private readonly IAgenciaService _agenciaService;
        private readonly IAgenciaPageService _agenciaPageService;
        private IMapper _mapper;

        public AgencyController(siscolasgamcContext _context, IAgenciaService dtService, IMapper mapper, IAgenciaPageService servicio)
        {
            _dbcontext = _context;
            _agenciaService = dtService;
            _mapper = mapper;
            _agenciaPageService = servicio;
        }
        [HttpGet]
        public IActionResult allAgency()
        {
            try
            {

               var agencia = _dbcontext.Agencia.Select(t =>
               new
               {
                   idAgencia = t.IdAgencia,
                   nomAgencia = t.NomAgencia,
                   estado = t.Estado,
                   ascdesc = t.Acdes,
                   mapa = t.Mapa,  
                   multimedia = t.Multimedia,
                   consulta = t.Consulta,
               }
               ).OrderByDescending(a=>a.idAgencia).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = agencia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        [HttpGet("paginacion/")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] AgenciaQuery query)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = new AgenciaFilter();
            filter.nombre = query.nombre;
            filter.sort = query.sort;
            var dtResponse = await _agenciaService.GetAgenciaAsync(filter, pagination);
            var paginas = await _agenciaPageService.GetAgenciaPageAsync(filter, pagination);
            return Ok(new { data = dtResponse, paginas });
        }
        //---------------------------------------------------------------------------------------------------

        [HttpGet("{id:long}")]
        public IActionResult idAgency(long id)
        {
            Agencium idagency = _dbcontext.Agencia.Find(id);
            if (idagency == null)
            {
                return BadRequest("Tipo de tramite no encontrado");
            }
            try
            {
                idagency = _dbcontext.Agencia.Find(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = idagency });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPost]
        public IActionResult Insert([FromBody] Agencium agen)
        {
            try
            {
                _dbcontext.Agencia.Add(agen);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult idEdit([FromBody] Agencium agen)
        {
            Agencium idagena = _dbcontext.Agencia.Find(agen.IdAgencia);
            if (idagena == null)
            {
                return BadRequest("Agencia no  Actualizada");
            }
            try
            {
                idagena.NomAgencia = agen.NomAgencia is null ? idagena.NomAgencia : agen.NomAgencia;
                idagena.Estado = agen.Estado is null ? idagena.Estado : agen.Estado;
                idagena.Acdes = agen.Acdes is null ? idagena.Acdes : agen.Acdes;
                idagena.Mapa = agen.Mapa is null ? idagena.Mapa : agen.Mapa;
                idagena.Multimedia = agen.Multimedia is null ? idagena.Multimedia : agen.Multimedia;
                idagena.Consulta = agen.Consulta is null ? idagena.Consulta : agen.Consulta;
                _dbcontext.Agencia.Update(idagena);
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

