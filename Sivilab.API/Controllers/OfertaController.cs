using Microsoft.AspNetCore.Mvc;
using Sivilab.Models.Models;
using System.Text.Json;

namespace Sivilab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OfertaController : ControllerBase
    {
        // private readonly IOfertaRepository _repo;

        // public OfertaController(IOfertaRepository repo)
        // {
        //     _repo = repo;
        // }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] OfertaCreateDto dto)
        {
            // Map DTO to Model
            var model = new OfertaEmpleo
            {
                Titulo = dto.Titulo,
                Experiencia = dto.Experiencia,
                SalarioMinimo = dto.SalarioMinimo,
                SalarioMaximo = dto.SalarioMaximo,
                HabilitarRangoSalario = dto.HabilitarRangoSalario,
                NivelAcademico = dto.NivelAcademico,
                EmpleaPersonasConDiscapacidad = dto.EmpleaPersonasConDiscapacidad,
                Descripcion = dto.Descripcion,
                UsarDomicilioRegistrado = dto.UsarDomicilioRegistrado,
                CodigoPostal = dto.CodigoPostal,
                Estado = dto.Estado,
                Municipio = dto.Municipio,
                Colonia = dto.Colonia,
                Calle = dto.Calle,
                NumeroExterior = dto.NumeroExterior,
                NumeroInterior = dto.NumeroInterior,
                Referencia = dto.Referencia,
                // Serialize lists
                Conocimientos = JsonSerializer.Serialize(dto.Conocimientos),
                Idiomas = JsonSerializer.Serialize(dto.Idiomas),
                Habilidades = string.Join(",", dto.Habilidades),
                DisponibilidadViajar = dto.DisponibilidadViajar,
                Actividades = string.Join("\n", dto.Actividades),
                VideoUrl = dto.VideoUrl,
                TipoContratacion = dto.TipoContratacion,
                OtroTipoContratacion = dto.OtroTipoContratacion,
                Beneficios = string.Join(",", dto.Beneficios),
                JornadaLaboral = dto.JornadaLaboral,
                DiasLaborales = dto.DiasLaborales,
                DiasSeleccionados = string.Join(",", dto.DiasSeleccionados),
                HorarioEntrada = dto.HorarioEntrada,
                HorarioSalida = dto.HorarioSalida,
                AclaracionesHorario = dto.AclaracionesHorario,
                PublicarEnPortal = dto.PublicarEnPortal,
                NumeroVacantes = dto.NumeroVacantes,
                LimitePostulaciones = dto.LimitePostulaciones,
                FechaInicioVigencia = dto.FechaInicioVigencia,
                FechaFinVigencia = dto.FechaFinVigencia,
                FechaInicioReclutamiento = dto.FechaInicioReclutamiento,
                ContrataExtranjeros = dto.ContrataExtranjeros,
                NumeroVacantesExtranjeros = dto.NumeroVacantesExtranjeros,
                TieneConstanciaMigracion = dto.TieneConstanciaMigracion,
                CIEVigente = dto.CIEVigente,
                EtapasReclutamiento = JsonSerializer.Serialize(dto.EtapasReclutamiento),
                DuracionProcesoReclutamiento = dto.DuracionProcesoReclutamiento,
                UsarContactoRegistrado = dto.UsarContactoRegistrado,
                ContactoCargo = dto.ContactoCargo,
                ContactoCorreo = dto.ContactoCorreo,
                ContactoTelefono = dto.ContactoTelefono,
                NombreContactoAlterno = dto.NombreContactoAlterno,
                CargoContactoAlterno = dto.CargoContactoAlterno,
                CorreoContactoAlterno = dto.CorreoContactoAlterno,
                TelefonoContactoAlterno = dto.TelefonoContactoAlterno,
                FeriaEmpleoId = dto.FeriaEmpleoId
            };

            // Call repository (commented out as repo/SP might not exist)
            // await _repo.CrearOferta(model);

            return Ok(new { Message = "Oferta creada exitosamente (Mock)" });
        }
    }

    // Mirror DTO to avoid project reference issues for now
    public class OfertaCreateDto
    {
        public string Titulo { get; set; } = string.Empty;
        public string Experiencia { get; set; } = string.Empty;
        public decimal? SalarioMinimo { get; set; }
        public decimal? SalarioMaximo { get; set; }
        public bool HabilitarRangoSalario { get; set; }
        public string NivelAcademico { get; set; } = string.Empty;
        public bool EmpleaPersonasConDiscapacidad { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public bool UsarDomicilioRegistrado { get; set; } = true;
        public string CodigoPostal { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string Colonia { get; set; } = string.Empty;
        public string Calle { get; set; } = string.Empty;
        public string NumeroExterior { get; set; } = string.Empty;
        public string NumeroInterior { get; set; } = string.Empty;
        public string Referencia { get; set; } = string.Empty;
        public List<object> Conocimientos { get; set; } = new();
        public List<object> Idiomas { get; set; } = new();
        public List<string> Habilidades { get; set; } = new();
        public bool DisponibilidadViajar { get; set; }
        public List<string> Actividades { get; set; } = new();
        public string VideoUrl { get; set; } = string.Empty;
        public string TipoContratacion { get; set; } = string.Empty;
        public string OtroTipoContratacion { get; set; } = string.Empty;
        public List<string> Beneficios { get; set; } = new();
        public string JornadaLaboral { get; set; } = string.Empty;
        public string DiasLaborales { get; set; } = string.Empty;
        public List<string> DiasSeleccionados { get; set; } = new();
        public string HorarioEntrada { get; set; } = string.Empty;
        public string HorarioSalida { get; set; } = string.Empty;
        public string AclaracionesHorario { get; set; } = string.Empty;
        public bool PublicarEnPortal { get; set; } = true;
        public int NumeroVacantes { get; set; } = 1;
        public int LimitePostulaciones { get; set; }
        public DateTime? FechaInicioVigencia { get; set; }
        public DateTime? FechaFinVigencia { get; set; }
        public DateTime? FechaInicioReclutamiento { get; set; }
        public bool ContrataExtranjeros { get; set; }
        public int NumeroVacantesExtranjeros { get; set; }
        public bool TieneConstanciaMigracion { get; set; }
        public bool CIEVigente { get; set; }
        public List<object> EtapasReclutamiento { get; set; } = new();
        public string DuracionProcesoReclutamiento { get; set; } = string.Empty;
        public bool UsarContactoRegistrado { get; set; } = true;
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
