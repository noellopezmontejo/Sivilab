using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Sivilab.Models.Models;

namespace Sivilab.Eventos.Services
{
    public class CandidatoCrpService : ICandidatoCrpService
    {
        private readonly HttpClient _http;
        private readonly IAccesoWebService _accesoWebService;

        public CandidatoCrpService(HttpClient http, IAccesoWebService accesoWebService)
        {
            _http = http;
            _accesoWebService = accesoWebService;
        }

        public async Task<IEnumerable<CandidatoCrp>> ObtenerTodos()
        {
            return await _http.GetFromJsonAsync<IEnumerable<CandidatoCrp>>("api/CandidatoCrp") ?? new List<CandidatoCrp>();
        }

        public async Task<CandidatoCrp?> ObtenerPorId(int id)
        {
            return await _http.GetFromJsonAsync<CandidatoCrp>($"api/CandidatoCrp/{id}");
        }

        public async Task<CandidatoCrp?> ObtenerPorCurp(string curp)
        {
            return await _http.GetFromJsonAsync<CandidatoCrp>($"api/CandidatoCrp/curp/{curp}");
        }

        public async Task<int> Crear(CandidatoCrp model)
        {
            // 1. Validar que el email no exista en Acceso_Web
            if (!await _accesoWebService.ValidarEmailDisponible(model.CorreoAcceso))
            {
                throw new InvalidOperationException("El correo electrónico ya está registrado.");
            }

            // 2. Crear el candidato
            var candidatoId = await _http.PostAsJsonAsync("api/CandidatoCrp", model);
            candidatoId.EnsureSuccessStatusCode();
            // CreatedAtAction returns location with id but body is the model; asumimos API devuelve Location header con id en route,
            // para simplicidad intentar leer el body (o ajustar según comportamiento del API). Si la API devuelve el id en el body, leerlo.
            // Aquí intentamos leer entero del body si existe:
            var created = await candidatoId.Content.ReadFromJsonAsync<int?>();
            return created ?? 0;
        }

        public async Task<bool> Actualizar(int id, CandidatoCrp model)
        {
            var resp = await _http.PutAsJsonAsync($"api/CandidatoCrp/{id}", model);
            return resp.IsSuccessStatusCode;
        }

        public async Task<bool> Eliminar(int id)
        {
            var resp = await _http.DeleteAsync($"api/CandidatoCrp/{id}");
            return resp.IsSuccessStatusCode;
        }
    }
}