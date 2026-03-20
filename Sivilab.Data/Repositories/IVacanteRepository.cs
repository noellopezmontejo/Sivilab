using Sivilab.Models.Models;

namespace Sivilab.Data.Repositories
{
    public interface IVacanteRepository
    {
        Task<IEnumerable<Vacante>> ObtenerVacantesVigentes();
        Task<Vacante?> ObtenerPorId(int id);
    }
}