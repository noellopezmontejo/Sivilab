using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sivilab.Models.Models
{
    public class FormularioEmpleo
    {
        [Required(ErrorMessage = "El CURP es requerido")]
        public string Curp { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido paterno es requerido")]
        public string Materno { get; set; } = string.Empty;

        [Required(ErrorMessage = "El apellido materno es requerido")]
        public string Paterno { get; set; } = string.Empty;

        [Required(ErrorMessage = "El género es requerido")]
        public string Genero { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha de nacimiento es requerida")]
        public DateTime? FechaNacimiento { get; set; }

        [Required(ErrorMessage = "El lugar de nacimiento es requerido")]
        public string LugarNacimiento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe especificar si es egresado del programa")]
        public string EgresadoPrograma { get; set; } = string.Empty;

        [Required(ErrorMessage = "El teléfono es requerido")]
        [Phone(ErrorMessage = "Formato de teléfono inválido")]
        public string Telefono { get; set; } = string.Empty;

        [Required(ErrorMessage = "El correo electrónico es requerido")]
        [EmailAddress(ErrorMessage = "Formato de correo electrónico inválido")]
        public string Correo { get; set; } = string.Empty;

        [Required(ErrorMessage = "El municipio es requerido")]
        public string Municipio { get; set; } = string.Empty;

        [Required(ErrorMessage = "El estado es requerido")]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "El pais es requerido")]
        public string Pais { get; set; } = string.Empty;

        [Required(ErrorMessage = "El código postal es requerido")]
        public string CodigoPostal { get; set; } = string.Empty;

        [Required(ErrorMessage = "La colonia es requerida")]
        public string Colonia { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nivel de estudios es requerido")]
        public string NivelEstudios { get; set; } = string.Empty;

        [Required(ErrorMessage = "La carrera o especialidad es requerida")]
        public string Carrera { get; set; } = string.Empty;

        [Required(ErrorMessage = "La experiencia laboral es requerida")]
        public string ExperienciaLaboral { get; set; } = string.Empty;

        [Required(ErrorMessage = "El puesto de trabajo es requerido")]
        public string PuestoTrabajo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Los años de experiencia son requeridos")]
        public string AnosExperiencia { get; set; } = string.Empty;

        [Required(ErrorMessage = "El dominio de idioma es requerido")]
        public string DominioIdioma { get; set; } = string.Empty;

        [Required(ErrorMessage = "El puesto deseado es requerido")]
        public string PuestoDeseado { get; set; } = string.Empty;

        [Required(ErrorMessage = "El salario pretendido es requerido")]
        public string SalarioPretendido { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe aceptar la declaración")]
        public string Declaracion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe aceptar la recolección de datos")]
        public string RecoleccionDatos { get; set; } = string.Empty;
    }
}
