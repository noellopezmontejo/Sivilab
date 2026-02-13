using System;
using System.Net.Mail;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Sivilab.Eventos.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpHost;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPassword;

        public EmailService(IConfiguration configuration)
        {
            _smtpHost = configuration["Email:SmtpHost"] ?? "smtp.gmail.com";
            _smtpPort = int.Parse(configuration["Email:SmtpPort"] ?? "587");
            _smtpUser = configuration["Email:SmtpUser"] ?? "";
            _smtpPassword = configuration["Email:SmtpPassword"] ?? "";
        }

        public async Task<bool> EnviarCorreoRecuperacion(string destinatario, string token)
        {
            try
            {
                var mensaje = new MimeMessage();
                mensaje.From.Add(new MailboxAddress("EMPLEOTÓN CHIAPAS 2025", _smtpUser));
                mensaje.To.Add(new MailboxAddress("", destinatario));
                mensaje.Subject = "Código de Recuperación de Contraseña";

                var bodyBuilder = new BodyBuilder
                {
                    HtmlBody = $@"
                        <!DOCTYPE html>
                        <html>
                        <head>
                            <style>
                                body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; }}
                                .container {{ max-width: 600px; margin: 0 auto; padding: 20px; }}
                                .header {{ background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%); color: white; padding: 30px; text-align: center; border-radius: 10px 10px 0 0; }}
                                .content {{ background: #f9f9f9; padding: 30px; border-radius: 0 0 10px 10px; }}
                                .token {{ background: white; color: #1e3c72; font-size: 48px; font-weight: bold; letter-spacing: 10px; padding: 20px; text-align: center; border: 3px dashed #1e3c72; border-radius: 10px; margin: 20px 0; }}
                                .warning {{ background: #fff3cd; border-left: 4px solid #ffc107; padding: 15px; margin: 20px 0; }}
                                .footer {{ text-align: center; margin-top: 20px; padding-top: 20px; border-top: 1px solid #ddd; color: #666; font-size: 12px; }}
                            </style>
                        </head>
                        <body>
                            <div class='container'>
                                <div class='header'>
                                    <h1>?? Recuperación de Contraseña</h1>
                                    <p>EMPLEOTÓN CHIAPAS 2025</p>
                                </div>
                                <div class='content'>
                                    <h2>Código de Verificación</h2>
                                    <p>Ha solicitado restablecer su contraseña. Utilice el siguiente código:</p>
                                    <div class='token'>{token}</div>
                                    <div class='warning'>
                                        <strong>?? Importante:</strong>
                                        <ul>
                                            <li>Este código expirará en <strong>15 minutos</strong></li>
                                            <li>No comparta este código con nadie</li>
                                        </ul>
                                    </div>
                                </div>
                                <div class='footer'>
                                    <p><strong>Servicio Nacional de Empleo Chiapas</strong></p>
                                </div>
                            </div>
                        </body>
                        </html>
                    "
                };

                mensaje.Body = bodyBuilder.ToMessageBody();

                using var smtpClient = new SmtpClient();
                await smtpClient.ConnectAsync(_smtpHost, _smtpPort, SecureSocketOptions.StartTls);
                await smtpClient.AuthenticateAsync(_smtpUser, _smtpPassword);
                await smtpClient.SendAsync(mensaje);
                await smtpClient.DisconnectAsync(true);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al enviar correo: {ex.Message}");
                return false;
            }
        }
    }
}