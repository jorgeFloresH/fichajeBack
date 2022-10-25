using apiServices.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace apiServices.Controllers
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : Controller
    {
        public readonly siscolasgamcContext _dbcontext;

        public UsersController(siscolasgamcContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        public IActionResult Alluser()
        {
            try
            {
                var usuarios = _dbcontext.Usuarios.Select(t =>
                new
                {
                    idUsuario = t.IdUsuario,
                    userName = t.UserName,
                    userPassword = t.UserPassword,
                    idPerfil = t.IdPerfil,
                    fechaCreacion = t.FechaCreacion,
                    estado = t.Estado,
                    idAgencia = t.IdAgencia,
                    nomAgencia = t.IdAgenciaNavigation.NomAgencia,
                    nomUsuario = t.NomUsuario,
                    apePaterno = t.ApePaterno,
                    apeMaterno = t.ApeMaterno,
                    ciUsuario = t.CiUsuario,
                    nomTipoP = t.IdPerfilNavigation.NomTipoP,
                    mapa = t.IdAgenciaNavigation.Mapa,
                    multimedia = t.IdAgenciaNavigation.Multimedia,
                    consulta = t.IdAgenciaNavigation.Consulta,
 
                }
                )
                    .OrderByDescending(a=>a.idUsuario)
                    .ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = usuarios });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        [HttpGet("{id:long}")]
        public IActionResult idUser(long id)
        {
            Usuario iduser = _dbcontext.Usuarios.Find(id);
            if (iduser == null)
            {
                return BadRequest("Usuario no encontrado");
            }
            try
            {
                iduser = _dbcontext.Usuarios.Find(id);
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = iduser });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        //usuario por agencia
        [HttpGet("UserFilter/{agencia}")]
        public IActionResult GetUserFilter(int agencia)
        { 
            try
            {

                DateTime fe = DateTime.UtcNow.AddHours(-04);
                Console.WriteLine(fe);
                var usuarios = _dbcontext.Usuarios.Where(t => t.IdAgencia == agencia).Select(t =>

                new
                {
                    idUsuario = t.IdUsuario,
                    userName = t.UserName,
                    userPassword = t.UserPassword,
                    idPerfil = t.IdPerfil,
                    fechaCreacion = t.FechaCreacion,
                    estado = t.Estado,
                    idAgencia = t.IdAgencia,
                    nomAgencia = t.IdAgenciaNavigation.NomAgencia,
                    nomUsuario = t.NomUsuario,
                    apePaterno = t.ApePaterno,
                    apeMaterno = t.ApeMaterno,
                    ciUsuario = t.CiUsuario,
                    nomTipoP = t.IdPerfilNavigation.NomTipoP
                }
               )
                    .OrderByDescending(a => a.idUsuario)
                    .ToList();

                if (usuarios == null)
                {
                    return BadRequest("Usuario no encontrado");
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = usuarios});
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrado", response = ex });
            }
        }
        //usuario por agencia
        [HttpGet("UserFilterCount/{agencia}")]
        public IActionResult GetUserFilterCount(int agencia)
        {
            try
            {

                DateTime fe = DateTime.UtcNow.AddHours(-04);
                Console.WriteLine(fe);
                var usuariosCount = _dbcontext.Usuarios.Where(t => t.IdAgencia == agencia).Count();
                var usuarios = _dbcontext.Usuarios.Where(t => t.IdAgencia == agencia).Select(t =>
                new
                {
                    idAgencia = t.IdAgencia,
                    nomAgencia = t.IdAgenciaNavigation.NomAgencia,
                }
               ).FirstOrDefault(); 

                if (usuarios == null)
                {
                    return BadRequest("Usuarios no encontrados");
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = usuarios, conteo = usuariosCount });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrados", response = ex });
            }
        }
        //usuario ALL
        [HttpGet("UserFilterCountAll")]
        public IActionResult GetUserFilterCountAll()
        {
            try
            {

                DateTime fe = DateTime.UtcNow.AddHours(-04);
                Console.WriteLine(fe);
                //var usuariosCount = _dbcontext.Usuarios.Where(t => t.IdAgencia == agencia).Count();
                var usuariosAll = _dbcontext.Usuarios.Count();
                var usuarios = _dbcontext.Usuarios.GroupBy(x => x.IdAgencia).Select(x => 
                new { 
                    idAgencia = x.Key, 
                    Conteo = x.Count() 
                }); 

                if (usuarios == null)
                {
                    return BadRequest("Usuarios no encontrados");
                }
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "success", response = usuarios ,totalUsuarios = usuariosAll });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status422UnprocessableEntity, new { mensaje = "No encontrados", response = ex });
            }
        }
        [HttpGet("{username}/{password}")]
        public ActionResult<List<Usuario>> GetIniciarSesion(string username, string password)
        {
            var usuarios = _dbcontext.Usuarios.Where(usuario => usuario.UserName.Equals(username) && usuario.UserPassword.Equals(password)).Select(t=>
            
            new
            {
                idUsuario = t.IdUsuario,
                apeMaterno = t.ApeMaterno,
                apePaterno = t.ApePaterno,
                ciUsuario = t.CiUsuario,
                estado = t.Estado,
                fechaCreacion = t.FechaCreacion,
                idAgencia = t.IdAgencia,
                estadoA = t.IdAgenciaNavigation.Estado,
                idPerfil = t.IdPerfil,
                nomPerfil = t.IdPerfilNavigation.NomTipoP,
                nomUsuario = t.NomUsuario,
                userName = t.UserName,
                usePassword = t.UserPassword,
                nomAgencia = t.IdAgenciaNavigation.NomAgencia,
                mapa = t.IdAgenciaNavigation.Mapa,
                multimedia = t.IdAgenciaNavigation.Multimedia,
                consulta = t.IdAgenciaNavigation.Consulta,
            }
            
            ).ToList();
            if (usuarios == null)
            {
                return NotFound();
            }

            return StatusCode(StatusCodes.Status200OK, new { usuarios });
        }
          

        [HttpPost]
        public IActionResult Insert([FromBody] Usuario agen)
        {
            try
            {
                
                
                _dbcontext.Usuarios.Add(agen);
                agen.FechaCreacion = DateTime.UtcNow;
                _dbcontext.SaveChanges();
                var idUsuario = agen.IdUsuario;
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Guardado Satisfactoriamente", idUsuario = idUsuario });
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex);
            }
        }
        //fuction put users
        [HttpPut]
        public IActionResult idEdit([FromBody] Usuario agen)
        {
            Usuario iduser = _dbcontext.Usuarios.Find(agen.IdUsuario);
            if (iduser == null)
            {
                return BadRequest("Usuario no  Actualizada");
            }
            try
            {
                iduser.UserName = agen.UserName is null ? iduser.UserName : agen.UserName;
                iduser.UserPassword = agen.UserPassword is null ? iduser.UserPassword : agen.UserPassword;
                iduser.Estado = agen.Estado is null ? iduser.Estado : agen.Estado;
                iduser.IdPerfil = agen.IdPerfil is null ? iduser.IdPerfil : agen.IdPerfil;
                iduser.IdAgencia = agen.IdAgencia is null ? iduser.IdAgencia : agen.IdAgencia;
                iduser.NomUsuario = agen.NomUsuario is null ? iduser.NomUsuario : agen.NomUsuario;
                iduser.ApePaterno = agen.ApePaterno is null ? iduser.ApePaterno : agen.ApePaterno;
                iduser.ApeMaterno = agen.ApeMaterno is null ? iduser.ApeMaterno : agen.ApeMaterno;
                iduser.CiUsuario = agen.CiUsuario is null ? iduser.CiUsuario : agen.CiUsuario;

                _dbcontext.Usuarios.Update(iduser);
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
