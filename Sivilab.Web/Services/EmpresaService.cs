using Sivilab.Web.Components.Models;
using System.Net.Http.Json;

namespace Sivilab.Web.Services;

public class EmpresaService : IEmpresaService
{
    private readonly HttpClient _http;

    public EmpresaService(HttpClient http)
    {
        _http = http;
    }

    public async Task<bool> CrearEmpresa(EmpresaDto empresa)
    {
        // Placeholder for API call
        // var response = await _http.PostAsJsonAsync("api/Empresa", empresa);
        // return response.IsSuccessStatusCode;
        Console.WriteLine($"Simulating API call to create Empresa: {empresa.NombreComercial}");
        return await Task.FromResult(true);
    }
}
