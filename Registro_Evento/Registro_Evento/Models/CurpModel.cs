using System.ComponentModel.DataAnnotations;

namespace Registro_Evento.Models
{
    public class CurpModel
    {
        [Required(ErrorMessage = "La CURP es obligatoria")]
        [Display(Name = "CURP")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "La CURP debe tener exactamente 18 caracteres")]
        public string Curp { get; set; } = string.Empty;

        [Display(Name = "Nombre Completo")]
        public string? NombreCompleto { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        [Display(Name = "Sexo")]
        public string? Sexo { get; set; }

        [Display(Name = "Estado de Nacimiento")]
        public string? EstadoNacimiento { get; set; }

        public bool EsValida { get; set; }
        public string? MensajeError { get; set; }
    }
}