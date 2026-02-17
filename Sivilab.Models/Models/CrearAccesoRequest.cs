using System.ComponentModel.DataAnnotations;

namespace Sivilab.Models.Models
{
    public class CrearAccesoRequest
    {
        [Required]
        [StringLength(18, MinimumLength = 18)]
        public string Curp { get; set; } = string.Empty;

        [Required]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        public string Paterno { get; set; } = string.Empty;

        public string? Materno { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        [MinLength(8)]
        public string Contrasena { get; set; } = string.Empty;
    }
}