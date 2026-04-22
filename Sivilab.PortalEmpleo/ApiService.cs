using Sivilab.Models.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sivilab.PortalEmpleo
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
            // Configurar Url base de la API local (asegúrate de que Sivilab.API esté corriendo)
            if (_http.BaseAddress == null)
            {
                _http.BaseAddress = new System.Uri("https://localhost:7264/");
            }
        }

        public async Task<FormularioEmpleo?> BuscarPorCurp(string curp)
        {
            return await _http.GetFromJsonAsync<FormularioEmpleo>($"/api/user/{curp}");
        }

        public async Task<IEnumerable<Vacante>> GetVacantesVigentesAsync()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<IEnumerable<Vacante>>("api/Vacantes/vigentes");
                return response ?? new List<Vacante>();
            }
            catch (System.Exception ex)
            {
                // Manejo de errores de conexión
                System.Console.WriteLine($"Error al consultar API: {ex.Message}");
                return new List<Vacante>();
            }
        }

        public async Task<IEnumerable<Categoria>> GetCategoriasAsync()
        {
            try
            {
                var response = await _http.GetFromJsonAsync<IEnumerable<Categoria>>("api/Vacantes/categorias");
                return response ?? new List<Categoria>();
            }
            catch (System.Exception ex)
            {
                System.Console.WriteLine($"Error al consultar categorías de la API: {ex.Message}");
                return new List<Categoria>();
            }
        }
    }
}
