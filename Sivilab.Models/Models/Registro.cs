using System;

namespace Registro_Evento.Models
{
    public class Registro
    {
        // CLAVE PRIMARIA
        public int Id { get; set; }

        // DATOS PERSONALES BÁSICOS
        public string Folio { get; set; } = string.Empty;
        public string CURP { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public DateTime FechaNacimiento { get; set; }
        public string LugarNacimiento { get; set; } = string.Empty;
        public bool EsEgresadoJovenes { get; set; }

        // DATOS DE CONTACTO
        public string Telefono { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;

        // DIRECCIÓN
        public string MunicipioEstado { get; set; } = string.Empty;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Colonia { get; set; } = string.Empty;

        // FORMACIÓN ACADÉMICA
        public string NivelEstudios { get; set; } = string.Empty;
        public string CarreraEspecialidad { get; set; } = string.Empty;

        // EXPERIENCIA LABORAL
        public string ExperienciaLaboral { get; set; } = string.Empty;
        public string PuestosTrabajo { get; set; } = string.Empty;
        public string AniosExperiencia { get; set; } = string.Empty;
        public string DominioIdioma { get; set; } = string.Empty;

        // ASPIRACIONES LABORALES
        public string PuestoDeseado { get; set; } = string.Empty;
        public decimal SalarioPretendido { get; set; }

        // TÉRMINOS Y CONDICIONES
        public bool AceptaTerminos { get; set; }
        public bool AceptaPrivacidad { get; set; }

        // CAMPOS DE AUDITORÍA
        public DateTime FechaRegistro { get; set; } = DateTime.Now;
        public string? IpRegistro { get; set; }
    }
}
