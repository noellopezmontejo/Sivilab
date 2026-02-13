using System.Threading.Tasks;
using Sivilab.Models.Models;

namespace Sivilab.Eventos.Services
{
    public interface IAccesoWebService
    {
        Task<bool> ValidarEmailDisponible(string email);
        Task<bool> ValidarUserNameDisponible(string userName);
        Task<AccesoWeb?> ObtenerPorEmail(string email);
        Task<bool> ValidarCredenciales(string email, string contrasena);
        Task<int> CrearAcceso(CandidatoCrp candidato);
        Task<bool> ActualizarContrasena(string email, string nuevaContrasena);
        Task<bool> EnviarCodigoVerificacion(string email);
        Task<bool> ConfirmarEmail(string email, string codigo);
    }
}