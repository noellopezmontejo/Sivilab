using Sivilab.Models.Models;

namespace Sivilab.Eventos.Services
{
    public interface IVacanteService
    {
        Task<IEnumerable<Vacante>> ObtenerVacantesVigentes();
        Task<Vacante?> ObtenerPorId(int id);
        Task<bool> PostularVacante(int vacanteId, string curp);
    }
}