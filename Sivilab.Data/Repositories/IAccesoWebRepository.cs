using System.Threading.Tasks;
using Sivilab.Models.Models;

namespace Sivilab.Data.Repositories
{
    public interface IAccesoWebRepository
    {
        Task<AccesoWeb?> ObtenerPorEmail(string email);
        Task<AccesoWeb?> ObtenerPorUserName(string userName);
        Task<bool> ExisteEmail(string email);
        Task<bool> ExisteUserName(string userName);
        Task<int> Crear(AccesoWeb acceso);
        Task<bool> Actualizar(AccesoWeb acceso);
        Task<bool> ValidarCredenciales(string email, string passwordHash);
        Task<bool> ConfirmarEmail(string email, string confirmationCode);
        Task<bool> ActualizarContrasena(string email, string nuevaPasswordHash);
        Task<IEnumerable<AccesoWeb>> ObtenerTodos();
        Task<AccesoWeb?> ObtenerPorId(int id);
        Task<bool> Eliminar(int id);
    }
}