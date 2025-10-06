using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Registro_Evento.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        [Display(Name = "Nombre")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(100, ErrorMessage = "El apellido no puede exceder 100 caracteres")]
        [Display(Name = "Apellido")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "El CURP es obligatorio")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "El CURP debe tener exactamente 18 caracteres")]
        [RegularExpression(@"^[A-Z]{4}[0-9]{6}[HM][A-Z]{5}[0-9]{2}$", ErrorMessage = "Formato de CURP inválido")]
        [Display(Name = "CURP")]
        public string Curp { get; set; }

        [Range(0, 120, ErrorMessage = "La edad debe estar entre 0 y 120 años")]
        [Display(Name = "Edad")]
        public int? Edad { get; set; }

        [StringLength(100, ErrorMessage = "La escolaridad no puede exceder 100 caracteres")]
        [Display(Name = "Escolaridad")]
        public string? Escolaridad { get; set; }

        [EmailAddress(ErrorMessage = "Formato de email inválido")]
        [StringLength(150, ErrorMessage = "El email no puede exceder 150 caracteres")]
        [Display(Name = "Correo Electrónico")]
        public string? Email { get; set; }

        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        [StringLength(20, ErrorMessage = "El teléfono no puede exceder 20 caracteres")]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }

        // Propiedad de solo lectura para mostrar nombre completo
        [Display(Name = "Nombre Completo")]
        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}