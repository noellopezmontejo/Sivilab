using Microsoft.AspNetCore.Mvc;
using Sivilab.Data.Repositories;

namespace Sivilab.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VacantesController : ControllerBase
    {
        private readonly IVacanteRepository _repository;
        private readonly ILogger<VacantesController> _logger;

        public VacantesController(
            IVacanteRepository repository,
            ILogger<VacantesController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: api/Vacantes/vigentes
        [HttpGet("vigentes")]
        public async Task<IActionResult> ObtenerVacantesVigentes()
        {
            try
            {
                _logger.LogInformation("Obteniendo vacantes vigentes");
                
                var vacantes = await _repository.ObtenerVacantesVigentes();
                
                _logger.LogInformation("{Count} vacantes encontradas", vacantes.Count());
                
                return Ok(vacantes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener vacantes vigentes");
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // GET: api/Vacantes/5
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerPorId(int id)
        {
            try
            {
                var vacante = await _repository.ObtenerPorId(id);
                
                if (vacante == null)
                    return NotFound(new { mensaje = "Vacante no encontrada" });

                return Ok(vacante);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener vacante {Id}", id);
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }

        // POST: api/Vacantes/postular
        [HttpPost("postular")]
        public async Task<IActionResult> PostularVacante([FromBody] PostulacionRequest request)
        {
            try
            {
                // TODO: Implementar lógica de postulación
                _logger.LogInformation("Postulación a vacante {VacanteId} por CURP {Curp}", 
                    request.VacanteId, request.Curp);
                
                return Ok(new { mensaje = "Postulación registrada exitosamente" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al postular a vacante");
                return StatusCode(500, new { mensaje = $"Error: {ex.Message}" });
            }
        }
    }

    public class PostulacionRequest
    {
        public int VacanteId { get; set; }
        public string Curp { get; set; } = string.Empty;
    }
}