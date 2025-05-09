using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sivilab.API.Services
{
    public interface IJwtService
    {
        string GenerarToken(int usuarioId, string nombre);
    }

    public class JwtService : IJwtService
    {
        private readonly string _claveSecreta;

        public JwtService(string claveSecreta)
        {
            _claveSecreta = claveSecreta;
        }

        public string GenerarToken(int usuarioId, string nombre)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, usuarioId.ToString()),
                new Claim(JwtRegisteredClaimNames.Name, nombre),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_claveSecreta));
            var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "Sivilab",
                audience: "Sivilab",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
