using Microsoft.AspNetCore.Mvc;
using Sivilab.Data.Repositories;
using Sivilab.Models.Models;
using Sivilab.API.Services;
using System.Security.Cryptography;
using System.Text;

namespace Sivilab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccesoWebController : ControllerBase
    {
        private readonly IAccesoWebRepository _repository;
        private readonly IEmailService _emailService;
        private readonly ILogger<AccesoWebController> _logger;

        public AccesoWebController(
            IAccesoWebRepository repository, 
            IEmailService emailService,
            ILogger<AccesoWebController> logger)
        {
            _repository = repository;
            _emailService = emailService;
            _logger = logger;
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

                var existe = await _repository.ExisteEmail(request.Email);
                if (existe)
                    return Conflict(new { mensaje = "Este correo ya tiene acceso registrado" });

                var codigoVerificacion = GenerarCodigoVerificacion();

                var acceso = new AccesoWeb
                {
                    Curp = request.Curp,
                    Nombre = request.Nombre,
                    Paterrno = request.Paterno,
                    Materno = request.Materno ?? "",
                    UserName = request.Email,
                    Email = request.Email,
                    PasswordHash = HashPassword(request.Contrasena),
                    IsEmailConfirmed = false,
                    ConfirmationCode = codigoVerificacion,
                    Role = "Candidato"
                };

                var nuevoId = await _repository.Crear(acceso);

                if (nuevoId > 0)
                {
                    // Enviar correo
                    var nombreCompleto = $"{request.Nombre} {request.Paterno} {request.Materno}".Trim();
                    
                    _logger.LogInformation("Intentando enviar código a {Email}", request.Email);
                    
                    var correoEnviado = await _emailService.EnviarCodigoVerificacion(
                        request.Email, 
                        nombreCompleto, 
                        codigoVerificacion
                    );

                    if (!correoEnviado)
                    {
                        _logger.LogWarning("No se pudo enviar correo a {Email}, pero acceso creado", request.Email);
                    }

                    return Ok(new 
                    { 
                        id = nuevoId, 
                        mensaje = "Acceso creado exitosamente",
                        codigoDesarrollo = codigoVerificacion // ELIMINAR EN PRODUCCIÓN
                    });
                }

                return BadRequest(new { mensaje = "No se pudo crear el acceso" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear acceso");
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
                _logger.LogInformation("Solicitud de envío de código para {Email}", request.Email);

                var acceso = await _repository.ObtenerPorEmail(request.Email);
                
                if (acceso == null)
                {
                    _logger.LogWarning("No se encontró acceso para {Email}", request.Email);
                    return NotFound(new { mensaje = "No se encontró acceso con ese email" });
                }

                // Generar nuevo código
                var codigo = GenerarCodigoVerificacion();
                acceso.ConfirmationCode = codigo;
                
                var actualizado = await _repository.Actualizar(acceso);
                
                if (!actualizado)
                {
                    _logger.LogError("No se pudo actualizar el código en BD para {Email}", request.Email);
                    return StatusCode(500, new { mensaje = "No se pudo actualizar el código" });
                }

                // Enviar correo
                var nombreCompleto = $"{acceso.Nombre} {acceso.Paterrno} {acceso.Materno}".Trim();
                
                _logger.LogInformation("Enviando correo a {Email} con código {Codigo}", request.Email, codigo);
                
                var enviado = await _emailService.EnviarCodigoVerificacion(
                    request.Email, 
                    nombreCompleto, 
                    codigo
                );

                if (enviado)
                {
                    _logger.LogInformation("Código enviado exitosamente a {Email}", request.Email);
                    return Ok(new 
                    { 
                        mensaje = "Código enviado exitosamente",
                        codigoDesarrollo = codigo // ELIMINAR EN PRODUCCIÓN
                    });
                }

                _logger.LogError("EmailService retornó false para {Email}", request.Email);
                return StatusCode(500, new { mensaje = "No se pudo enviar el correo" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Excepción al enviar código a {Email}", request.Email);
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