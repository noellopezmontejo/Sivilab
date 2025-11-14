using System.Collections.Generic;
using System.Threading.Tasks;
using Sivilab.Models.Models;

namespace Sivilab.Web.Services
{
    public interface ICandidatoCrpService
    {
        Task<IEnumerable<CandidatoCrp>> ObtenerTodosAsync();
        Task<CandidatoCrp?> ObtenerPorIdAsync(int id);
        Task<CandidatoCrp?> ObtenerPorCurpAsync(string curp);
        Task<int> CrearAsync(CandidatoCrp model);
        Task<bool> ActualizarAsync(int id, CandidatoCrp model);
        Task<bool> EliminarAsync(int id);
    }
}