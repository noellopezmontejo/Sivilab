﻿using Microsoft.AspNetCore.Mvc;
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
        private readonly IJwtService _jwtService;

        public UsuarioController(IUsuarioRepository usuarioRepository, IEmailService emailService, IJwtService jwtService)
        {
            _usuarioRepository = usuarioRepository;
            _emailService = emailService;
            _jwtService = jwtService;
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

        // NUEVO: Actualizar usuario
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarUsuario(int id, [FromBody] Usuario usuario)
        {
            if (usuario == null || id != usuario.Id)
                return BadRequest("Datos de usuario inválidos.");

            var usuarioExistente = await _usuarioRepository.ObtenerUsuarioPorId(id);
            if (usuarioExistente == null)
                return NotFound();

            await _usuarioRepository.ActualizarUsuario(usuario);
            return NoContent();
        }

        // NUEVO: Eliminar usuario
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var usuarioExistente = await _usuarioRepository.ObtenerUsuarioPorId(id);
            if (usuarioExistente == null)
                return NotFound();

            await _usuarioRepository.EliminarUsuario(id);
            return NoContent();
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Usuario usuario)
        {
            var usuarioValidado = await _usuarioRepository.ValidarCredenciales(usuario.Email, usuario.Contrasena);

            if (usuarioValidado == null)
                return Unauthorized("Credenciales incorrectas.");

            if (!usuarioValidado.Validado)
                return BadRequest("Por favor, valida tu correo electrónico antes de iniciar sesión.");

            var token = _jwtService.GenerarToken(usuarioValidado.Id, usuarioValidado.Nombre, usuarioValidado.Rol);

            return Ok(new { Token = token });
        }

    }
}
