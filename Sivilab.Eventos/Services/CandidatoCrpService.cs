using Sivilab.Models.Models;
using System.Net.Http.Json;

namespace Sivilab.Eventos.Services
{
    public class CandidatoCrpService : ICandidatoCrpService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<CandidatoCrpService>? _logger;

        public CandidatoCrpService(
            IHttpClientFactory httpClientFactory,
            ILogger<CandidatoCrpService>? logger = null)
        {
            _httpClient = httpClientFactory.CreateClient("SivilabAPI");
            _logger = logger;
        }

        public async Task<CandidatoCrp?> ObtenerPorCurp(string curp)
        {
            try
            {
                _logger?.LogInformation("🔍 Obteniendo candidato por CURP: {Curp}", curp);
                
                var response = await _httpClient.GetAsync($"api/CandidatoCrp/curp/{curp}");
                
                if (response.IsSuccessStatusCode)
                {
                    var candidato = await response.Content.ReadFromJsonAsync<CandidatoCrp>();
                    _logger?.LogInformation("✅ Candidato encontrado: {Nombre}", candidato?.NombreCompleto);
                    return candidato;
                }
                
                _logger?.LogWarning("❌ Candidato no encontrado para CURP: {Curp} - StatusCode: {StatusCode}", 
                    curp, response.StatusCode);
                return null;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "❌ Error al obtener candidato por CURP: {Curp}", curp);
                Console.WriteLine($"ERROR CandidatoCrpService.ObtenerPorCurp: {ex.Message}");
                return null;
            }
        }

        public async Task<int> AgregarCandidato(CandidatoCrp candidato)
        {
            try
            {
                _logger?.LogInformation("➕ Agregando candidato: {Nombre}", candidato.NombreCompleto);
                
                var response = await _httpClient.PostAsJsonAsync("api/CandidatoCrp", candidato);
                
                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<CandidatoCrp>();
                    _logger?.LogInformation("✅ Candidato agregado con ID: {Id}", resultado?.CandidatoId);
                    return resultado?.CandidatoId ?? 0;
                }
                
                _logger?.LogWarning("❌ No se pudo agregar candidato - StatusCode: {StatusCode}", response.StatusCode);
                return 0;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "❌ Error al agregar candidato");
                Console.WriteLine($"ERROR CandidatoCrpService.AgregarCandidato: {ex.Message}");
                return 0;
            }
        }

        public async Task<bool> ActualizarCandidato(CandidatoCrp candidato)
        {
            try
            {
                _logger?.LogInformation("🔄 Actualizando candidato ID: {Id}", candidato.CandidatoId);
                
                var response = await _httpClient.PutAsJsonAsync($"api/CandidatoCrp/{candidato.CandidatoId}", candidato);
                
                if (response.IsSuccessStatusCode)
                {
                    _logger?.LogInformation("✅ Candidato actualizado exitosamente");
                    return true;
                }
                
                _logger?.LogWarning("❌ No se pudo actualizar candidato - StatusCode: {StatusCode}", response.StatusCode);
                return false;
            }
            catch (Exception ex)
            {
                _logger?.LogError(ex, "❌ Error al actualizar candidato");
                Console.WriteLine($"ERROR CandidatoCrpService.ActualizarCandidato: {ex.Message}");
                return false;
            }
        }

        public Task<IEnumerable<CandidatoCrp>> ObtenerTodos()
        {
            throw new NotImplementedException("ObtenerTodos no está implementado");
        }

        public Task<CandidatoCrp?> ObtenerPorId(int id)
        {
            throw new NotImplementedException("ObtenerPorId no está implementado");
        }
    }
}