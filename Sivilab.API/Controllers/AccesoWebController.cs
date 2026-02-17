using Microsoft.AspNetCore.Mvc;
using Sivilab.Data.Repositories;
using Sivilab.Models.Models;
using System.Security.Cryptography;
using System.Text;

namespace Sivilab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccesoWebController : ControllerBase
    {
        private readonly IAccesoWebRepository _repository;

        public AccesoWebController(IAccesoWebRepository repository)
        {
            _repository = repository;
        }

        // GET: api/AccesoWeb/email/{email}
        [HttpGet("email/{email}")]
        public async Task<IActionResult> ObtenerPorEmail(string email)
        {
            try
            {
                var acceso = await _repository.ObtenerPorEmail(email);
                
                if (acceso == null)
                    return NotFound(new { mensaje = "No se encontró acceso con ese email" });

                return Ok(acceso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // GET: api/AccesoWeb/username/{userName}
        [HttpGet("username/{userName}")]
        public async Task<IActionResult> ObtenerPorUserName(string userName)
        {
            try
            {
                var acceso = await _repository.ObtenerPorUserName(userName);
                
                if (acceso == null)
                    return NotFound(new { mensaje = "No se encontró acceso con ese username" });

                return Ok(acceso);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // GET: api/AccesoWeb/email-disponible/{email}
        [HttpGet("email-disponible/{email}")]
        public async Task<IActionResult> ValidarEmailDisponible(string email)
        {
            try
            {
                var existe = await _repository.ExisteEmail(email);
                return Ok(new { disponible = !existe });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // GET: api/AccesoWeb/username-disponible/{userName}
        [HttpGet("username-disponible/{userName}")]
        public async Task<IActionResult> ValidarUserNameDisponible(string userName)
        {
            try
            {
                var existe = await _repository.ExisteUserName(userName);
                return Ok(new { disponible = !existe });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // POST: api/AccesoWeb/crear
        [HttpPost("crear")]
        public async Task<IActionResult> CrearAcceso([FromBody] CrearAccesoRequest request)
        {
            try
            {
                if (request == null || !ModelState.IsValid)
                    return BadRequest(new { mensaje = "Datos inválidos", errores = ModelState });

                // Verificar si el email ya tiene acceso
                var existe = await _repository.ExisteEmail(request.Email);
                if (existe)
                    return Conflict(new { mensaje = "Este correo ya tiene acceso registrado" });

                // Crear objeto AccesoWeb
                var acceso = new AccesoWeb
                {
                    Curp = request.Curp,
                    Nombre = request.Nombre,
                    Paterrno = request.Paterno,
                    Materno = request.Materno ?? "",
                    UserName = request.Email, // Usar email como username
                    Email = request.Email,
                    PasswordHash = HashPassword(request.Contrasena),
                    IsEmailConfirmed = false,
                    ConfirmationCode = GenerarCodigoVerificacion(),
                    Role = "Candidato"
                };

                var nuevoId = await _repository.Crear(acceso);

                if (nuevoId > 0)
                {
                    return Ok(new { id = nuevoId, mensaje = "Acceso creado exitosamente" });
                }

                return BadRequest(new { mensaje = "No se pudo crear el acceso" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // POST: api/AccesoWeb/validar-credenciales
        [HttpPost("validar-credenciales")]
        public async Task<IActionResult> ValidarCredenciales([FromBody] CredencialesRequest request)
        {
            try
            {
                var passwordHash = HashPassword(request.Contrasena);
                var esValido = await _repository.ValidarCredenciales(request.Email, passwordHash);
                
                if (esValido)
                    return Ok(new { valido = true, mensaje = "Credenciales válidas" });
                
                return Unauthorized(new { valido = false, mensaje = "Credenciales inválidas" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // POST: api/AccesoWeb/enviar-codigo
        [HttpPost("enviar-codigo")]
        public async Task<IActionResult> EnviarCodigoVerificacion([FromBody] EmailRequest request)
        {
            try
            {
                var acceso = await _repository.ObtenerPorEmail(request.Email);
                
                if (acceso == null)
                    return NotFound(new { mensaje = "No se encontró acceso con ese email" });

                // Generar nuevo código
                var codigo = GenerarCodigoVerificacion();
                acceso.ConfirmationCode = codigo;
                
                await _repository.Actualizar(acceso);

                // TODO: Enviar correo con el código
                // Por ahora solo retornamos éxito
                return Ok(new { mensaje = "Código enviado", codigoDesarrollo = codigo });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // POST: api/AccesoWeb/confirmar-email
        [HttpPost("confirmar-email")]
        public async Task<IActionResult> ConfirmarEmail([FromBody] ConfirmacionRequest request)
        {
            try
            {
                var confirmado = await _repository.ConfirmarEmail(request.Email, request.Codigo);
                
                if (confirmado)
                    return Ok(new { mensaje = "Email confirmado exitosamente" });
                
                return BadRequest(new { mensaje = "Código incorrecto o expirado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // PUT: api/AccesoWeb/actualizar-contrasena
        [HttpPut("actualizar-contrasena")]
        public async Task<IActionResult> ActualizarContrasena([FromBody] ActualizarContrasenaRequest request)
        {
            try
            {
                var passwordHash = HashPassword(request.NuevaContrasena);
                var actualizado = await _repository.ActualizarContrasena(request.Email, passwordHash);
                
                if (actualizado)
                    return Ok(new { mensaje = "Contraseña actualizada exitosamente" });
                
                return NotFound(new { mensaje = "No se encontró el usuario" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // Métodos auxiliares
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private string GenerarCodigoVerificacion()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }

    // DTOs para requests
    public class CredencialesRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }

    public class EmailRequest
    {
        public string Email { get; set; } = string.Empty;
    }

    public class ConfirmacionRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Codigo { get; set; } = string.Empty;
    }

    public class ActualizarContrasenaRequest
    {
        public string Email { get; set; } = string.Empty;
        public string NuevaContrasena { get; set; } = string.Empty;
    }

    public class CrearAccesoRequest
    {
        public string Curp { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Paterno { get; set; } = string.Empty;
        public string? Materno { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Contrasena { get; set; } = string.Empty;
    }
}