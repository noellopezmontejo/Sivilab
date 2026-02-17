using System.Collections.Generic;
using System.Threading.Tasks;
using Sivilab.Models.Models;

namespace Sivilab.Eventos.Services
{
    public interface ICandidatoCrpService
    {
        Task<IEnumerable<CandidatoCrp>> ObtenerTodos();
        Task<CandidatoCrp?> ObtenerPorId(int id);
        Task<CandidatoCrp?> ObtenerPorCurp(string curp);
      
        Task<int> AgregarCandidato(CandidatoCrp candidato);
        Task<bool> ActualizarCandidato(CandidatoCrp candidato);
    }
}