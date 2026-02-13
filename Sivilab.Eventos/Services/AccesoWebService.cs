using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Sivilab.Data.Repositories;
using Sivilab.Models.Models;

namespace Sivilab.Eventos.Services
{
    public class AccesoWebService : IAccesoWebService
    {
        private readonly IAccesoWebRepository _repository;

        public AccesoWebService(IAccesoWebRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> ValidarEmailDisponible(string email)
        {
            return !await _repository.ExisteEmail(email);
        }

        public async Task<bool> ValidarUserNameDisponible(string userName)
        {
            return !await _repository.ExisteUserName(userName);
        }

        public async Task<AccesoWeb?> ObtenerPorEmail(string email)
        {
            return await _repository.ObtenerPorEmail(email);
        }

        public async Task<bool> ValidarCredenciales(string email, string contrasena)
        {
            var passwordHash = HashPassword(contrasena);
            return await _repository.ValidarCredenciales(email, passwordHash);
        }

        public async Task<int> CrearAcceso(CandidatoCrp candidato)
        {
            var acceso = new AccesoWeb
            {
                Nombre = candidato.Nombre,
                Paterrno = candidato.Paterno,
                Materno = candidato.Materno,
                UserName = candidato.CorreoAcceso, // O generar uno único
                Email = candidato.CorreoAcceso,
                PasswordHash = HashPassword(candidato.Contrasena),
                IsEmailConfirmed = false,
                ConfirmationCode = GenerarCodigoVerificacion(),
                Role = "Candidato"
            };

            return await _repository.Crear(acceso);
        }

        public async Task<bool> ActualizarContrasena(string email, string nuevaContrasena)
        {
            var passwordHash = HashPassword(nuevaContrasena);
            return await _repository.ActualizarContrasena(email, passwordHash);
        }

        public async Task<bool> EnviarCodigoVerificacion(string email)
        {
            var acceso = await _repository.ObtenerPorEmail(email);
            if (acceso == null) return false;

            acceso.ConfirmationCode = GenerarCodigoVerificacion();
            await _repository.Actualizar(acceso);

            // TODO: Enviar correo con el código
            Console.WriteLine($"Código de verificación para {email}: {acceso.ConfirmationCode}");
            
            return true;
        }

        public async Task<bool> ConfirmarEmail(string email, string codigo)
        {
            return await _repository.ConfirmarEmail(email, codigo);
        }

        // Métodos auxiliares
        private string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha256.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        private string GenerarCodigoVerificacion()
        {
            return new Random().Next(100000, 999999).ToString();
        }
    }
}