using Sivilab.Models.Models;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Sivilab.PortalEmpleo
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        public async Task<FormularioEmpleo?> BuscarPorCurp(string curp)
        {
            return await _http.GetFromJsonAsync<FormularioEmpleo>($"/api/user/{curp}");
        }
    }
}
