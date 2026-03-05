using Sivilab.Models.Models;
using System.Net.Http.Json;

namespace Sivilab.Eventos.Services
{
    public class AccesoWebService : IAccesoWebService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AccesoWebService>? _logger;

        public AccesoWebService(
            IHttpClientFactory httpClientFactory,
            ILogger<AccesoWebService>? logger = null)
        {
            _httpClient = httpClientFactory.CreateClient("SivilabAPI");
            _logger = logger;
        }

        public async Task<AccesoWeb?> ValidarAcceso(string correo, string contrasena)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/accesoweb/validar", new { correo, contrasena });
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AccesoWeb>();
                }
                
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ValidarAcceso: {ex.Message}");
                return null;
            }
        }

        public async Task<int> CrearAcceso(CandidatoCrp candidato)
        {
            try
            {
                var request = new
                {
                    curp = candidato.Curp,
                    nombre = candidato.Nombre,
                    paterno = candidato.Paterno,
                    materno = candidato.Materno ?? "",
                    email = candidato.CorreoAcceso,
                    contrasena = candidato.Contrasena
                };

                var response = await _httpClient.PostAsJsonAsync("api/AccesoWeb/crear", request);
                
                if (response.IsSuccessStatusCode)
                {
                    // Deserializar usando JsonDocument para manejar respuesta mixta
                    using var jsonDoc = await response.Content.ReadFromJsonAsync<System.Text.Json.JsonDocument>();
                    
                    if (jsonDoc?.RootElement.TryGetProperty("id", out var idElement) == true)
                    {
                        return idElement.GetInt32();
                    }
                    
                    return 0;
                }
                else
                {
                    var error = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error HTTP {response.StatusCode}: {error}");
                    return 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en CrearAcceso: {ex}");
                return 0;
            }
        }

        public async Task<AccesoWeb?> ObtenerPorEmail(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/AccesoWeb/email/{email}");
                
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<AccesoWeb>();
                }
                
                // 404 es esperado si no existe
                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                
                var error = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Error en ObtenerPorEmail: {error}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepción en ObtenerPorEmail: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> ValidarCredenciales(string email, string contrasena)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/AccesoWeb/validar-credenciales", 
                    new { email, contrasena });
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ValidarCredenciales: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ValidarEmailDisponible(string email)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/AccesoWeb/email-disponible/{email}");
                
                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<Dictionary<string, bool>>();
                    return resultado?["disponible"] ?? false;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ValidarEmailDisponible: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ValidarUserNameDisponible(string userName)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/AccesoWeb/username-disponible/{userName}");
                
                if (response.IsSuccessStatusCode)
                {
                    var resultado = await response.Content.ReadFromJsonAsync<Dictionary<string, bool>>();
                    return resultado?["disponible"] ?? false;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ValidarUserNameDisponible: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> EnviarCodigoVerificacion(string email)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/AccesoWeb/enviar-codigo", 
                    new { email });
                
                if (response.IsSuccessStatusCode)
                {
                    // Leer la respuesta para obtener el código (solo desarrollo)
                    var contenido = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Respuesta enviar código: {contenido}");
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en EnviarCodigoVerificacion: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ConfirmarEmail(string email, string codigo)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/AccesoWeb/confirmar-email", 
                    new { email, codigo });
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ConfirmarEmail: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> ActualizarContrasena(string email, string nuevaContrasena)
        {
            try
            {
                var response = await _httpClient.PutAsJsonAsync("api/AccesoWeb/actualizar-contrasena", 
                    new { email, nuevaContrasena });
                
                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en ActualizarContrasena: {ex.Message}");
                return false;
            }
        }
    }
}