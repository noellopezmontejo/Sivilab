using System.Net.Mail;
using System.Net;

namespace Sivilab.API.Services
{
    public interface IEmailService
    {
        Task EnviarCorreo(string destinatario, string asunto, string mensaje);
    }

    public class EmailService : IEmailService
    {
        public async Task EnviarCorreo(string destinatario, string asunto, string mensaje)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("noellopez1307@gmail.com", "ihqf blve nnen pvgs"),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("noellopez1307@gmail.com"),
                Subject = asunto,
                Body = mensaje,
                IsBodyHtml = true,
            };

            mailMessage.To.Add(destinatario);

            await smtpClient.SendMailAsync(mailMessage);
        }
    }
}
