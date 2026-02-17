using Sivilab.Models.Models;
using System.Net.Http.Json;

namespace Sivilab.Eventos.Services
{
    public class CandidatoCrpService : ICandidatoCrpService
    {
        private readonly HttpClient _httpClient;

        public CandidatoCrpService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("SivilabAPI");
        }

        public async Task<CandidatoCrp?> ObtenerPorCurp(string curp)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/CandidatoCrp/curp/{curp}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CandidatoCrp>();
                }
                
                return null;
            }
            catch
            {
                return null;
            }
        }

        public async Task<int> AgregarCandidato(CandidatoCrp candidato)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/CandidatoCrp", candidato);
                
                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<CandidatoCrp>();
                    return resultado?.CandidatoId ?? 0;
                }
                
                return 0;
            }
            catch
            {
                return 0;
            }
        }

        public async Task<bool> ActualizarCandidato(CandidatoCrp candidato)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync($"api/CandidatoCrp/{candidato.CandidatoId}", candidato);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

        Task<IEnumerable<CandidatoCrp>> ICandidatoCrpService.ObtenerTodos()
        {
            throw new NotImplementedException();
        }

        Task<CandidatoCrp?> ICandidatoCrpService.ObtenerPorId(int id)
        {
            throw new NotImplementedException();
        }
    }
}