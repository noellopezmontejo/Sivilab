using System.Collections.Generic;
using System.Threading.Tasks;
using Sivilab.Models.Models;

namespace Sivilab.Web.Services
{
    public interface ICandidatoCrpService
    {
        Task<IEnumerable<CandidatoCrp>> ObtenerTodos();
        Task<CandidatoCrp?> ObtenerPorId(int id);
        Task<CandidatoCrp?> ObtenerPorCurp(string curp);
        Task<int> Crear(CandidatoCrp model);
        Task<bool> Actualizar(int id, CandidatoCrp model);
        Task<bool> Eliminar(int id);
    }
}