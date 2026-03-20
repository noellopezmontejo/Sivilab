using Sivilab.Models.Models;
using System.Net.Http.Json;

namespace Sivilab.Eventos.Services
{
    public class VacanteService : IVacanteService
    {
        private readonly HttpClient _httpClient;

        public VacanteService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SivilabAPI");
        }

        public async Task<IEnumerable<Vacante>> ObtenerVacantesVigentes()
        {
            try
            {
                Console.WriteLine("🔍 Obteniendo vacantes vigentes...");
                
                var response = await _httpClient.GetAsync("api/Vacantes/vigentes");
                
                if (response.IsSuccessStatusCode)
                {
                    var vacantes = await response.Content.ReadFromJsonAsync<List<Vacante>>();
                    Console.WriteLine($"✅ {vacantes?.Count ?? 0} vacantes encontradas");
                    return vacantes ?? new List<Vacante>();
                }
                
                Console.WriteLine($"⚠️ Error al obtener vacantes: {response.StatusCode}");
                return new List<Vacante>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en ObtenerVacantesVigentes: {ex.Message}");
                return new List<Vacante>();
            }
        }

        public async Task<Vacante?> ObtenerPorId(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/Vacantes/{id}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<Vacante>();
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en ObtenerPorId: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> PostularVacante(int vacanteId, string curp)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/Vacantes/postular", 
                    new { vacanteId, curp });
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error en PostularVacante: {ex.Message}");
                return false;
            }
        }
    }
}