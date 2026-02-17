using System;
using System.ComponentModel.DataAnnotations;

namespace Sivilab.Models.Models
{
    public class AccesoWeb
    {
        public int CveAccesoWeb { get; set; }

        [Required(ErrorMessage = "la Curp es obligatorio")]
        public string? Curp { get; set; }
        public string? Nombre { get; set; }
        
        public string? Paterrno { get; set; }
        
        public string? Materno { get; set; }
        
        [Required(ErrorMessage = "El nombre de usuario es obligatorio")]
        public string UserName { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Formato de correo inválido")]
        public string Email { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string PasswordHash { get; set; } = string.Empty;
        
        public bool IsEmailConfirmed { get; set; } = false;
        
        public string? ConfirmationCode { get; set; }
        
        public string Role { get; set; } = "Candidato";
        
        // Nombre completo calculado
        public string NombreCompleto
        {
            get
            {
                var partes = new[] { Paterrno, Materno, Nombre }
                    .Where(p => !string.IsNullOrWhiteSpace(p));
                return string.Join(" ", partes);
            }
        }
    }
}