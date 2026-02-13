using System.Threading.Tasks;

namespace Sivilab.Eventos.Services
{
    public interface IEmailService
    {
        Task<bool> EnviarCorreoRecuperacion(string destinatario, string token);
    }
}