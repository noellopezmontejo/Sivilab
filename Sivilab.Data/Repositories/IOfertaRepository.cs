using Sivilab.Models.Models;
using System.Threading.Tasks;

namespace Sivilab.Data.Repositories
{
    public interface IOfertaRepository
    {
        Task<int> CrearOferta(OfertaEmpleo oferta);
    }
}
