using System.Text.Json;
using System.Text;
using Sivilab.Portal.Models;

namespace Sivilab.Portal.Services
{
    public class EventoService
    {
        private readonly string _jsonPath;

        public EventoService(IWebHostEnvironment env)
        {
            _jsonPath = Path.Combine(env.WebRootPath, "Data", "eventos.json");
        }

        public List<Evento> ObtenerTodos()
        {
            if (!File.Exists(_jsonPath))
                return new List<Evento>();

            var jsonString = File.ReadAllText(_jsonPath, Encoding.UTF8);
            return JsonSerializer.Deserialize<List<Evento>>(jsonString) ?? new List<Evento>();
        }

        public void GuardarTodos(List<Evento> eventos)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var jsonString = JsonSerializer.Serialize(eventos, options);
            File.WriteAllText(_jsonPath, jsonString, Encoding.UTF8);
        }

        public Evento? ObtenerPorIndice(int indice)
        {
            var eventos = ObtenerTodos();
            if (indice >= 0 && indice < eventos.Count)
                return eventos[indice];
            return null;
        }

        public void Agregar(Evento evento)
        {
            var eventos = ObtenerTodos();
            eventos.Add(evento);
            GuardarTodos(eventos);
        }

        public bool Actualizar(int indice, Evento eventoActualizado)
        {
            var eventos = ObtenerTodos();
            if (indice >= 0 && indice < eventos.Count)
            {
                eventos[indice] = eventoActualizado;
                GuardarTodos(eventos);
                return true;
            }
            return false;
        }

        public bool Eliminar(int indice)
        {
            var eventos = ObtenerTodos();
            if (indice >= 0 && indice < eventos.Count)
            {
                eventos.RemoveAt(indice);
                GuardarTodos(eventos);
                return true;
            }
            return false;
        }
    }
}