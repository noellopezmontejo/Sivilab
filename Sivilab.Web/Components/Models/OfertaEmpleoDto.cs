using System.ComponentModel.DataAnnotations;

namespace Sivilab.Web.Components.Models;

public class OfertaEmpleoDto
{
    // Anuncio de tu empleo
    [Required(ErrorMessage = "El título de la oferta es requerido")]
    public string Titulo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La experiencia en el puesto es requerida")]
    public string Experiencia { get; set; } = string.Empty;

    public decimal? SalarioMinimo { get; set; }
    public decimal? SalarioMaximo { get; set; }
    public bool HabilitarRangoSalario { get; set; }

    [Required(ErrorMessage = "El nivel académico es requerido")]
    public string NivelAcademico { get; set; } = string.Empty;

    public bool EmpleaPersonasConDiscapacidad { get; set; }

    [Required(ErrorMessage = "La descripción de la oferta es requerida")]
    public string Descripcion { get; set; } = string.Empty;

    // Ubicación del empleo
    public bool UsarDomicilioRegistrado { get; set; } = true;

    [Required(ErrorMessage = "El código postal es requerido")]
    public string CodigoPostal { get; set; } = string.Empty;

    [Required(ErrorMessage = "El estado es requerido")]
    public string Estado { get; set; } = string.Empty;

    [Required(ErrorMessage = "El municipio es requerido")]
    public string Municipio { get; set; } = string.Empty;

    [Required(ErrorMessage = "La colonia es requerida")]
    public string Colonia { get; set; } = string.Empty;

    [Required(ErrorMessage = "La calle es requerida")]
    public string Calle { get; set; } = string.Empty;

    [Required(ErrorMessage = "El número exterior es requerido")]
    public string NumeroExterior { get; set; } = string.Empty;

    public string NumeroInterior { get; set; } = string.Empty;
    public string Referencia { get; set; } = string.Empty;

    // Requisitos
    public List<ConocimientoDto> Conocimientos { get; set; } = new();
    public List<IdiomaDto> Idiomas { get; set; } = new();
    public List<string> Habilidades { get; set; } = new();
    public bool DisponibilidadViajar { get; set; }

    // Actividades
    public List<string> Actividades { get; set; } = new();

    // Video
    [Url(ErrorMessage = "La URL del video no es válida")]
    public string VideoUrl { get; set; } = string.Empty;

    // Beneficios y Horario
    [Required(ErrorMessage = "El tipo de contratación es requerido")]
    public string TipoContratacion { get; set; } = string.Empty;

    public string OtroTipoContratacion { get; set; } = string.Empty;

    public List<string> Beneficios { get; set; } = new();

    [Required(ErrorMessage = "La jornada laboral es requerida")]
    public string JornadaLaboral { get; set; } = string.Empty;

    [Required(ErrorMessage = "Los días laborales son requeridos")]
    public string DiasLaborales { get; set; } = string.Empty;

    public List<string> DiasSeleccionados { get; set; } = new();

    [Required(ErrorMessage = "El horario de entrada es requerido")]
    public string HorarioEntrada { get; set; } = string.Empty;

    [Required(ErrorMessage = "El horario de salida es requerido")]
    public string HorarioSalida { get; set; } = string.Empty;

    public string AclaracionesHorario { get; set; } = string.Empty;

    // Datos de reclutamiento
    public bool PublicarEnPortal { get; set; } = true;

    [Range(1, int.MaxValue, ErrorMessage = "El número de vacantes debe ser mayor a 0")]
    public int NumeroVacantes { get; set; } = 1;

    [Range(1, int.MaxValue, ErrorMessage = "El límite de postulaciones debe ser positivo")]
    public int LimitePostulaciones { get; set; }

    [Required(ErrorMessage = "La fecha de inicio de vigencia es requerida")]
    public DateTime? FechaInicioVigencia { get; set; }

    [Required(ErrorMessage = "La fecha de fin de vigencia es requerida")]
    public DateTime? FechaFinVigencia { get; set; }

    [Required(ErrorMessage = "La fecha de inicio de reclutamiento es requerida")]
    public DateTime? FechaInicioReclutamiento { get; set; }

    public bool ContrataExtranjeros { get; set; }
    
    public int NumeroVacantesExtranjeros { get; set; }

    public bool TieneConstanciaMigracion { get; set; }
    
    public bool CIEVigente { get; set; }

    public List<EtapaReclutamientoDto> EtapasReclutamiento { get; set; } = new();

    [Required(ErrorMessage = "La duración del proceso es requerida")]
    public string DuracionProcesoReclutamiento { get; set; } = string.Empty;

    // Contacto y Feria
    public bool UsarContactoRegistrado { get; set; } = true;
    public string ContactoCargo { get; set; } = string.Empty;
    public string ContactoCorreo { get; set; } = string.Empty;
    public string ContactoTelefono { get; set; } = string.Empty;

    // Si elige "Otro contacto", se pueden usar estos mismos campos o añadir nuevos si se requiere separar 
    // la info del usuario registrado vs la del contacto específico de esta oferta.
    // Asumiremos que si UsarContactoRegistrado es false, se capturarán datos en variables adicionales o se sobreescriben.
    
    public string NombreContactoAlterno { get; set; } = string.Empty;
    public string CargoContactoAlterno { get; set; } = string.Empty;
    public string CorreoContactoAlterno { get; set; } = string.Empty;
    public string TelefonoContactoAlterno { get; set; } = string.Empty;

    public string FeriaEmpleoId { get; set; } = string.Empty; // ID de la feria seleccionada
}

public class EtapaReclutamientoDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
}

public class ConocimientoDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Experiencia { get; set; } = string.Empty;
}

public class IdiomaDto
{
    public string Nombre { get; set; } = string.Empty;
    public string Nivel { get; set; } = string.Empty;
    public string Certificacion { get; set; } = string.Empty;
}
