using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivilab.Models.Models
{
    public class CandidatoCrp
    {
        public int CandidatoId { get; set; }
        public int FolioSIISNE { get; set; }
        [Required(ErrorMessage = "La CURP es obligatoria")]
        [StringLength(18, MinimumLength = 18, ErrorMessage = "La CURP debe tener 18 caracteres")]
        [RegularExpression(@"^[A-Z]{4}[0-9]{6}[HM][A-Z]{5}[0-9A-Z]{2}$", ErrorMessage = "Formato de CURP inválido")]
        public string Curp { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombres(s) es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        public string Paterno { get; set; } = string.Empty;

        [Required(ErrorMessage = "El  apellido materno es obligatorio")]
        public string Materno { get; set; } = string.Empty;


        [Required(ErrorMessage = "El género es obligatorio")]
        public string Genero { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        public DateTime? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El lugar de nacimiento es obligatorio")]
        public string LugarNacimiento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string EsEgresadoJovenes { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es obligatorio")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "El municipio es obligatorio")]
        public string Municipio { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código postal es obligatorio")]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "El código postal debe tener 5 dígitos")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "El código postal debe contener solo números")]
        public string CodigoPostal { get; set; } = string.Empty;

        [Required(ErrorMessage = "La colonia es obligatoria")]
        public string Colonia { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nivel de estudios es obligatorio")]
        public string NivelEstudios { get; set; } = string.Empty;

        [Required(ErrorMessage = "La carrera o especialidad es obligatoria")]
        public string CarreraEspecialidad { get; set; } = string.Empty;

        [Required(ErrorMessage = "La experiencia laboral es obligatoria")]
        public string ExperienciaLaboral { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los puestos de trabajo son obligatorios")]
        public string PuestosTrabajo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los años de experiencia son obligatorios")]
        public string AniosExperiencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "El dominio de idioma es obligatorio")]
        public string DominioIdioma { get; set; } = string.Empty;

        [Required(ErrorMessage = "El puesto deseado es obligatorio")]
        public string PuestoDeseado { get; set; } = string.Empty;

        [Required(ErrorMessage = "El salario pretendido es obligatorio")]
        [Range(1, double.MaxValue, ErrorMessage = "Ingrese un salario válido mayor a 0")]
        public decimal? SalarioPretendido { get; set; }

        [Required(ErrorMessage = "Debe indicar si desea usar el mismo correo")]
        public string UsarMismoCorreo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo de acceso es obligatorio")]
        [EmailAddress(ErrorMessage = "Ingrese un correo válido")]
        public string CorreoAcceso { get; set; } = string.Empty;

        [Required(ErrorMessage = "La contraseña es obligatoria")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        public string Contrasena { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe confirmar la contraseña")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
        [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas no coinciden")]
        public string ConfirmarContrasena { get; set; } = string.Empty;

        [Range(typeof(bool), "true", "true", ErrorMessage = "Debe aceptar los términos y condiciones")]
        public bool AceptaTerminos { get; set; }

        [Range(typeof(bool), "true", "true", ErrorMessage = "Debe aceptar el tratamiento de datos personales")]
        public bool AceptaPrivacidad { get; set; }


        // Propiedad calculada que concatena ApellidoPaterno, ApellidoMaterno y Nombre.
        // Es de solo lectura para que Dapper/ORMs no intenten mapearla contra una columna de la BD.
        public string NombreCompleto
        {
            get
            {
                var partes = new[] { Paterno, Materno, Nombre }
                             .Where(p => !string.IsNullOrWhiteSpace(p));
                return string.Join(" ", partes);
            }
        }
    }


}
