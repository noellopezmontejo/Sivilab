using Sivilab.Models.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sivilab.Data.Repositories
{
    public interface ICandidatoCrpRepository
    {
        Task<IEnumerable<CandidatoCrp>> ObtenerTodos();
        Task<CandidatoCrp?> ObtenerPorId(int id);
        Task<CandidatoCrp?> ObtenerPorCurp(string curp); // NUEVO: obtener por CURP
        Task<int> AgregarCandidato(CandidatoCrp candidato);
        Task<bool> ActualizarCandidato(CandidatoCrp candidato);
        Task<bool> EliminarCandidato(int id);
    }
}