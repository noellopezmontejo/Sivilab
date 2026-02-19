using System.Net.Mail;
using System.Net;

namespace Sivilab.API.Services
{
    public interface IEmailService
    {
        Task<bool> EnviarCodigoVerificacion(string destinatario, string nombreCompleto, string codigo);
        Task<bool> EnviarCodigoRecuperacion(string destinatario, string nombreCompleto, string codigo);
        Task<bool> EnviarCorreoBienvenida(string destinatario, string nombreCompleto);
    }

    
}
