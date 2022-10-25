using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class H_DerivacionController : Controller

    {
        public readonly siscolasgamcContext _dbcontext;

        public H_DerivacionController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }
        [HttpGet]
        public IActionResult getDerivateIds()
        {
            try
            {

                var agencia = _dbcontext.HistorialDerivacions.Select(t =>
               new
               {
                   idDerivacion = t.IdDerivacion,
                   idTicket = t.IdTicket,
                   origen = t.VentanillaOrigen,
                   destino = t.VentanillaDestino,
               }
               )
                
               .ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = agencia });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("getByIdTicket/{GetTicket}")]
        public IActionResult getByIdTicket(int GetTicket)
        {
            try
            {

               var tiketHistory = _dbcontext.HistorialDerivacions.Where(a=>a.IdTicket == GetTicket).Select(t =>
               new
               {
                   idDerivacion = t.IdDerivacion,
                   idTicket = t.IdTicket,
                   origen = t.VentanillaOrigen,
                   destino = t.VentanillaDestino,
               }
               ).FirstOrDefault();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = tiketHistory });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPost]
        public IActionResult postHistorial([FromBody] HistorialDerivacion agen)
        {
            try
            {
                _dbcontext.HistorialDerivacions.Add(agen);
                _dbcontext.SaveChanges();
                var idDerivaPost = agen.IdDerivacion;
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente",idDer = idDerivaPost });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpPut]
        public IActionResult putHistorial([FromBody] HistorialDerivacion agen)
        {
            HistorialDerivacion idderivation = _dbcontext.HistorialDerivacions.Find(agen.IdDerivacion);
            if (idderivation == null)
            {
                return BadRequest("Agencia no  Actualizada");
            }
            try
            {
                idderivation.IdTicket = agen.IdTicket is null ? idderivation.IdTicket : agen.IdTicket;
                idderivation.VentanillaOrigen = agen.VentanillaOrigen is null ? idderivation.VentanillaOrigen : agen.VentanillaOrigen;
                idderivation.VentanillaDestino = agen.VentanillaDestino is null ? idderivation.VentanillaDestino : agen.VentanillaDestino;
                _dbcontext.HistorialDerivacions.Update(idderivation);
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
