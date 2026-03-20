using System.ComponentModel.DataAnnotations;

namespace Sivilab.Models.Models
{
    public class OfertaEmpleo
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Experiencia { get; set; } = string.Empty;
        public decimal? SalarioMinimo { get; set; }
        public decimal? SalarioMaximo { get; set; }
        public bool HabilitarRangoSalario { get; set; }
        public string NivelAcademico { get; set; } = string.Empty;
        public bool EmpleaPersonasConDiscapacidad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        // Ubicación
        public bool UsarDomicilioRegistrado { get; set; } = true;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string Colonia { get; set; } = string.Empty;
        public string Calle { get; set; } = string.Empty;
        public string NumeroExterior { get; set; } = string.Empty;
        public string NumeroInterior { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        // Requisitos
        public string Conocimientos { get; set; } = string.Empty; // JSON or serialized
        public string Idiomas { get; set; } = string.Empty; // JSON or serialized
        public string Habilidades { get; set; } = string.Empty; // Comma separated?
        public bool DisponibilidadViajar { get; set; }
        // Actividades
        public string Actividades { get; set; } = string.Empty; // JSON or newline separated?
        // Video
        public string VideoUrl { get; set; } = string.Empty;
        // Beneficios y Horario
        public string TipoContratacion { get; set; } = string.Empty;
        public string OtroTipoContratacion { get; set; } = string.Empty;
        public string Beneficios { get; set; } = string.Empty; // Comma separated?
        public string JornadaLaboral { get; set; } = string.Empty;
        public string DiasLaborales { get; set; } = string.Empty;
        public string DiasSeleccionados { get; set; } = string.Empty;
        public string HorarioEntrada { get; set; } = string.Empty;
        public string HorarioSalida { get; set; } = string.Empty;
        public string AclaracionesHorario { get; set; } = string.Empty;
        // Reclutamiento
        public bool PublicarEnPortal { get; set; }
        public int NumeroVacantes { get; set; }
        public int LimitePostulaciones { get; set; }
        public DateTime? FechaInicioVigencia { get; set; }
        public DateTime? FechaFinVigencia { get; set; }
        public DateTime? FechaInicioReclutamiento { get; set; }
        public bool ContrataExtranjeros { get; set; }
        public int NumeroVacantesExtranjeros { get; set; }
        public bool TieneConstanciaMigracion { get; set; }
        public bool CIEVigente { get; set; }
        public string EtapasReclutamiento { get; set; } = string.Empty; // JSON
        public string DuracionProcesoReclutamiento { get; set; } = string.Empty;
        // Contacto
        public bool UsarContactoRegistrado { get; set; }
        public string ContactoCargo { get; set; } = string.Empty;
        public string ContactoCorreo { get; set; } = string.Empty;
        public string ContactoTelefono { get; set; } = string.Empty;
        public string NombreContactoAlterno { get; set; } = string.Empty;
        public string CargoContactoAlterno { get; set; } = string.Empty;
        public string CorreoContactoAlterno { get; set; } = string.Empty;
        public string TelefonoContactoAlterno { get; set; } = string.Empty;
        public string FeriaEmpleoId { get; set; } = string.Empty;
    }
}
