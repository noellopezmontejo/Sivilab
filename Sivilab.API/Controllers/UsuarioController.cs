using Microsoft.AspNetCore.Mvc;
using Sivilab.API.Services;
using Sivilab.Data.Repositories;
using Sivilab.Models.Models;

namespace Sivilab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IEmailService _emailService; // Servicio para enviar correos


        public UsuarioController(IUsuarioRepository usuarioRepository, IEmailService emailService)
        {
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
        }

        [HttpPost]
        public async Task<IActionResult> AgregarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("El usuario no puede ser nulo.");

            var id = await _usuarioRepository.AgregarUsuario(usuario);
            usuario.Id = id;

            return CreatedAtAction(nameof(GetUsuarioPorId), new { id = usuario.Id }, usuario);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioPorId(int id)
        {
            var usuario = await _usuarioRepository.ObtenerUsuarioPorId(id);
            if (usuario == null)
                return NotFound();

            return Ok(usuario);
        }

        [HttpPost("Registrar")]
        public async Task<IActionResult> RegistrarUsuario([FromBody] Usuario usuario)
        {
            if (usuario == null)
                return BadRequest("El usuario no puede ser nulo.");

            var id = await _usuarioRepository.RegistrarUsuario(usuario);

            // Enviar correo con el código de validación
            var mensaje = $"Hola {usuario.Nombre}, valida tu correo usando este código: {usuario.CodigoValidacion}";
            await _emailService.EnviarCorreo(usuario.Email, "Validación de Correo", mensaje);

            return Ok(new { Message = "Usuario registrado. Verifica tu correo para validarlo." });
        }

        [HttpGet("ValidarCorreo")]
        public async Task<IActionResult> ValidarCorreo([FromQuery] string codigo)
        {
            var validado = await _usuarioRepository.ValidarCorreo(codigo);
            if (!validado)
                return BadRequest("Código de validación inválido o usuario ya validado.");

            return Ok(new { Message = "Correo validado correctamente." });
        }


    }
}
