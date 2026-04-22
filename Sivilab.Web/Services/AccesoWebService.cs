using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Sivilab.Models.Models;

namespace Sivilab.Web.Services
{
    public class AccesoWebService : IAccesoWebService
    {
        private readonly HttpClient _http;

        public AccesoWebService(HttpClient http)
        {
            _http = http;
        }

        public async Task<IEnumerable<AccesoWeb>> ObtenerTodos()
        {
            return await _http.GetFromJsonAsync<IEnumerable<AccesoWeb>>("api/AccesoWeb") ?? new List<AccesoWeb>();
        }

        public async Task<AccesoWeb?> ObtenerPorId(int id)
        {
            return await _http.GetFromJsonAsync<AccesoWeb>($"api/AccesoWeb/{id}");
        }

        public async Task<bool> Eliminar(int id)
        {
            var resp = await _http.DeleteAsync($"api/AccesoWeb/{id}");
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> ValidarCredenciales(string email, string passwordHash)
        {
            var model = new { Email = email, Contrasena = passwordHash };
            var resp = await _http.PostAsJsonAsync("api/AccesoWeb/validar-credenciales-web", model);
            return resp.IsSuccessStatusCode;
        }

        public async Task<AccesoWeb?> ObtenerPorEmail(string email)
        {
            return await _http.GetFromJsonAsync<AccesoWeb>($"api/AccesoWeb/email/{email}");
        }
    }
}
