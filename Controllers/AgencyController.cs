using apiServices.Data;
using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class AgencyController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;
        private IOrderedQueryable<Agencium> agency;

        public AgencyController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
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
        [HttpGet("prueba2/")]
        public async Task<ActionResult> PruebaPaginacion ([FromQuery] AgenciaPagination @params)
        {
            if (@params.sort != "null")
            {
                var s = @params.sort;
                if (s.Contains("-"))
                {
                    var filtro = s.Substring(1);
                    switch (filtro)
                    {
                        case "idAgencia":
                            agency = _dbcontext.Agencia.OrderByDescending(a => a.IdAgencia);
                            break;
                        case "nomAgencia":
                            agency = _dbcontext.Agencia.OrderByDescending(a => a.NomAgencia);
                            break;
                        case "estado":
                            agency = _dbcontext.Agencia.OrderByDescending(a => a.Estado);
                            break;
                    }
                }
                else
                {
                    switch (s)
                    {
                        case "idAgencia":
                            agency = _dbcontext.Agencia.OrderBy(a => a.IdAgencia);
                            break;
                        case "nomAgencia":
                            agency = _dbcontext.Agencia.OrderBy(a => a.NomAgencia);
                            break;
                        case "estado":
                            agency = _dbcontext.Agencia.OrderBy(a => a.Estado);
                            break;
                    }
                }
            }
            else
            {
                agency = _dbcontext.Agencia.OrderBy(a => a.IdAgencia);
            }
            var paginationMetadata = new PaginationMetaDataAgencia(agency.Count(), @params.PageNumber, @params.PageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            var items = await agency.Skip((@params.PageNumber - 1) * @params.PageSize).Take(@params.PageSize).ToListAsync();
            var res = items.Select(t =>
                new
                {
                    idAgencia = t.IdAgencia,
                    nomAgencia = t.NomAgencia,
                    estado = t.Estado,
                    ascdesc = t.Acdes,
                    mapa = t.Mapa,
                    multimedia = t.Multimedia,
                    consulta = t.Consulta
                }
            );
            return Ok(new {data = res, paginationMetadata.CurrentPage, paginationMetadata.TotalCount, paginationMetadata.TotalPages, paginationMetadata.HasPrevious, paginationMetadata.HasNext});
        }
        //------------------------------------------------------------------------------------------------------------------


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

