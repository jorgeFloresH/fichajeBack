using apiServices.Data.Filters;
using apiServices.Data.Queries;
using apiServices.Domain;
using apiServices.Models;
using apiServices.Services;
using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class RequisitosController : ControllerBase
    {
        public readonly siscolasgamcContext _dbcontext;
        public readonly IRequisitoService _requisitoService;
        private readonly IRequisitoPageService _requisitoPageService;
        private IMapper _mapper;

        public RequisitosController(siscolasgamcContext _context, IRequisitoService dtService, IMapper mapper, IRequisitoPageService servicio)
        {
            _dbcontext = _context;
            _requisitoService = dtService;
            _mapper = mapper;
            _requisitoPageService = servicio;
        }
        [HttpGet]
        public IActionResult requisito()
        {
            List<Requisito> requisitos = new List<Requisito>();
            try
            {
                requisitos = _dbcontext.Requisitos.ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = requisitos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        //------------------------------------------------------------------------------------------------------------------
        [HttpGet("paginacion/")]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery paginationQuery, [FromQuery] RequisitosQuery query)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = new RequisitosFilter();
            filter.nombre = query.nombre;
            filter.sort = query.sort;
            var dtResponse = await _requisitoService.GetRequisitoAsync(filter, pagination);
            var paginas = await _requisitoPageService.GetRequisitoPageAsync(filter, pagination);
            return Ok(new { data = dtResponse, paginas });
        }
        //---------------------------------------------------------------------------------------------------
        [HttpGet("{id:long}")]
        public IActionResult Getrequisito(long id)
        {
            Requisito requisito = _dbcontext.Requisitos.Find(id);
            if (requisito == null)
            {
                return BadRequest("Requisito no encontrado");
            }
            try
            {
                requisito = _dbcontext.Requisitos.Find(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = requisito });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPost]
        public IActionResult Guardar([FromBody] Requisito objeto)
        {
            try
            {
                _dbcontext.Requisitos.Add(objeto);
                _dbcontext.SaveChanges();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente" ,idRequisito = objeto.IdRequisitos });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult Edit([FromBody] Requisito objeto)
        {
            Requisito requisito = _dbcontext.Requisitos.Find(objeto.IdRequisitos);
            if (requisito == null)
            {
                return BadRequest("Requisito no encontrado");
            }
            try
            {

                requisito.NomRequisitos = objeto.NomRequisitos is null ? requisito.NomRequisitos : objeto.NomRequisitos;
                requisito.Estado = objeto.Estado is null ? requisito.Estado : objeto.Estado;
                _dbcontext.Requisitos.Update(requisito);
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
