using Sivilab.Web.Components.Models;
using System.Net.Http.Json;

namespace Sivilab.Web.Services
{
    public class OfertaService : IOfertaService
    {
        private readonly HttpClient _http;

        public OfertaService(HttpClient http)
        {
            _http = http;
        }

        public async Task<bool> CrearOferta(OfertaEmpleoDto oferta)
        {
            var response = await _http.PostAsJsonAsync("api/Oferta", oferta);
            return response.IsSuccessStatusCode;
        }
    }
}
