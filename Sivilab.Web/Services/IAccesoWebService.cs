using System.Collections.Generic;
using System.Threading.Tasks;
using Sivilab.Models.Models;

namespace Sivilab.Web.Services
{
    public interface IAccesoWebService
    {
        Task<IEnumerable<AccesoWeb>> ObtenerTodos();
        Task<AccesoWeb?> ObtenerPorId(int id);
        Task<bool> Eliminar(int id);
        Task<bool> ValidarCredenciales(string email, string passwordHash);
        Task<AccesoWeb?> ObtenerPorEmail(string email);
    }
}
