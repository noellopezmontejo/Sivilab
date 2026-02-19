using System.Net;
using System.Net.Mail;

namespace Sivilab.API.Services
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IConfiguration configuration, ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<bool> EnviarCodigoVerificacion(string destinatario, string nombreCompleto, string codigo)
        {
            try
            {
                var asunto = "Código de Verificación - EMPLEOTÓN CHIAPAS 2025";
                var cuerpo = GenerarHtmlCodigoVerificacion(nombreCompleto, codigo);
                
                return await EnviarCorreo(destinatario, asunto, cuerpo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar código de verificación a {Email}", destinatario);
                return false;
            }
        }

        public async Task<bool> EnviarCodigoRecuperacion(string destinatario, string nombreCompleto, string codigo)
        {
            try
            {
                var asunto = "Recuperación de Contraseña - EMPLEOTÓN CHIAPAS 2025";
                var cuerpo = GenerarHtmlCodigoRecuperacion(nombreCompleto, codigo);
                
                return await EnviarCorreo(destinatario, asunto, cuerpo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar código de recuperación a {Email}", destinatario);
                return false;
            }
        }

        public async Task<bool> EnviarCorreoBienvenida(string destinatario, string nombreCompleto)
        {
            try
            {
                var asunto = "¡Bienvenido a EMPLEOTÓN CHIAPAS 2025!";
                var cuerpo = GenerarHtmlBienvenida(nombreCompleto);
                
                return await EnviarCorreo(destinatario, asunto, cuerpo);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al enviar correo de bienvenida a {Email}", destinatario);
                return false;
            }
        }

        private async Task<bool> EnviarCorreo(string destinatario, string asunto, string cuerpoHtml)
        {
            try
            {
                // Validar configuración
                var smtpServer = _configuration["Email:SmtpServer"];
                var smtpPortStr = _configuration["Email:SmtpPort"];
                var emailFrom = _configuration["Email:From"];
                var emailPassword = _configuration["Email:Password"];

                if (string.IsNullOrEmpty(smtpServer) || 
                    string.IsNullOrEmpty(smtpPortStr) ||
                    string.IsNullOrEmpty(emailFrom) || 
                    string.IsNullOrEmpty(emailPassword))
                {
                    _logger.LogError("Configuración de email incompleta en appsettings.json");
                    return false;
                }

                var smtpPort = int.Parse(smtpPortStr);
                var displayName = _configuration["Email:DisplayName"] ?? "SNE Chiapas";

                _logger.LogInformation("Conectando a SMTP: {Server}:{Port}", smtpServer, smtpPort);

               
                using var client = new SmtpClient(smtpServer, smtpPort)
                {
                    EnableSsl = true,
                    UseDefaultCredentials = false, // IMPORTANTE
                    Credentials = new NetworkCredential(emailFrom, emailPassword),
                    Timeout = 30000 // 30 segundos
                };

                using var mailMessage = new MailMessage
                {
                    From = new MailAddress(emailFrom, displayName),
                    Subject = asunto,
                    Body = cuerpoHtml,
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };

                mailMessage.To.Add(destinatario);

                _logger.LogInformation("Enviando correo a {Email}", destinatario);

                await client.SendMailAsync(mailMessage);

                _logger.LogInformation("✅ Correo enviado exitosamente a {Email}", destinatario);
                return true;
            }
            catch (SmtpException ex)
            {
                _logger.LogError(ex, "❌ Error SMTP al enviar a {Email}: {Message}", destinatario, ex.Message);
                _logger.LogError("StatusCode: {StatusCode}", ex.StatusCode);
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ Error general al enviar correo a {Email}", destinatario);
                return false;
            }
        }

        private string GenerarHtmlCodigoVerificacion(string nombreCompleto, string codigo)
        {
            return $@"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Código de Verificación</title>
</head>
<body style='margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f4f4f4;'>
    <table width='100%' cellpadding='0' cellspacing='0' style='background-color: #f4f4f4; padding: 20px;'>
        <tr>
            <td align='center'>
                <table width='600' cellpadding='0' cellspacing='0' style='background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0,0,0,0.1);'>
                    <!-- Header -->
                    <tr>
                        <td style='background: linear-gradient(135deg, #1e3c72 0%, #2a5298 100%); padding: 30px; text-align: center;'>
                            <h1 style='color: #ffffff; margin: 0; font-size: 28px;'>
                                🔐 Código de Verificación
                            </h1>
                            <p style='color: #ffffff; margin: 10px 0 0 0; font-size: 14px; opacity: 0.9;'>
                                EMPLEOTÓN CHIAPAS 2025
                            </p>
                        </td>
                    </tr>
                    
                    <!-- Content -->
                    <tr>
                        <td style='padding: 40px 30px;'>
                            <h2 style='color: #1e3c72; margin: 0 0 20px 0; font-size: 22px;'>
                                Hola {nombreCompleto},
                            </h2>
                            
                            <p style='color: #333333; line-height: 1.6; margin: 0 0 20px 0;'>
                                Has solicitado crear acceso al Sistema de Empleotón del <strong>Servicio Nacional de Empleo Chiapas</strong>.
                            </p>
                            
                            <p style='color: #333333; line-height: 1.6; margin: 0 0 30px 0;'>
                                Tu código de verificación es:
                            </p>
                            
                            <!-- Código -->
                            <table width='100%' cellpadding='0' cellspacing='0'>
                                <tr>
                                    <td align='center' style='padding: 20px; background-color: #f8f9fa; border: 3px dashed #2a5298; border-radius: 8px;'>
                                        <div style='font-size: 48px; font-weight: bold; color: #1e3c72; letter-spacing: 12px; font-family: Courier New, monospace;'>
                                            {codigo}
                                        </div>
                                        <p style='margin: 15px 0 0 0; font-size: 14px; color: #6c757d;'>
                                            Este código expira en 15 minutos
                                        </p>
                                    </td>
                                </tr>
                            </table>
                            
                            <!-- Advertencia -->
                            <div style='margin-top: 30px; padding: 20px; background-color: #fff3cd; border-left: 4px solid #ffc107; border-radius: 4px;'>
                                <p style='margin: 0 0 10px 0; color: #856404; font-weight: bold;'>
                                    ⚠️ Importante:
                                </p>
                                <ul style='margin: 0; padding-left: 20px; color: #856404;'>
                                    <li style='margin: 5px 0;'>No compartas este código con nadie</li>
                                    <li style='margin: 5px 0;'>El equipo de SNE Chiapas nunca te pedirá este código por teléfono</li>
                                    <li style='margin: 5px 0;'>Si no solicitaste este código, ignora este mensaje</li>
                                </ul>
                            </div>
                        </td>
                    </tr>
                    
                    <!-- Footer -->
                    <tr>
                        <td style='background-color: #f8f9fa; padding: 20px 30px; text-align: center; border-top: 1px solid #e9ecef;'>
                            <p style='margin: 0 0 10px 0; color: #1e3c72; font-weight: bold;'>
                                Servicio Nacional de Empleo Chiapas
                            </p>
                            <p style='margin: 0 0 10px 0; color: #6c757d; font-size: 14px;'>
                                Secretaría de Economía y del Trabajo
                            </p>
                            <p style='margin: 10px 0 0 0; color: #6c757d; font-size: 12px;'>
                                Este es un correo automático, por favor no responder.
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
        }

        private string GenerarHtmlCodigoRecuperacion(string nombreCompleto, string codigo)
        {
            return $@"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <meta name='viewport' content='width=device-width, initial-scale=1.0'>
    <title>Recuperación de Contraseña</title>
</head>
<body style='margin: 0; padding: 0; font-family: Arial, sans-serif; background-color: #f4f4f4;'>
    <table width='100%' cellpadding='0' cellspacing='0' style='background-color: #f4f4f4; padding: 20px;'>
        <tr>
            <td align='center'>
                <table width='600' cellpadding='0' cellspacing='0' style='background-color: #ffffff; border-radius: 8px; overflow: hidden; box-shadow: 0 4px 6px rgba(0,0,0,0.1);'>
                    <tr>
                        <td style='background: linear-gradient(135deg, #dc3545 0%, #c82333 100%); padding: 30px; text-align: center;'>
                            <h1 style='color: #ffffff; margin: 0; font-size: 28px;'>
                                🔑 Recuperación de Contraseña
                            </h1>
                            <p style='color: #ffffff; margin: 10px 0 0 0; font-size: 14px; opacity: 0.9;'>
                                EMPLEOTÓN CHIAPAS 2025
                            </p>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style='padding: 40px 30px;'>
                            <h2 style='color: #dc3545; margin: 0 0 20px 0; font-size: 22px;'>
                                Hola {nombreCompleto},
                            </h2>
                            
                            <p style='color: #333333; line-height: 1.6; margin: 0 0 20px 0;'>
                                Has solicitado recuperar tu contraseña. Usa el siguiente código para continuar:
                            </p>
                            
                            <table width='100%' cellpadding='0' cellspacing='0'>
                                <tr>
                                    <td align='center' style='padding: 20px; background-color: #f8f9fa; border: 3px dashed #dc3545; border-radius: 8px;'>
                                        <div style='font-size: 48px; font-weight: bold; color: #dc3545; letter-spacing: 12px; font-family: Courier New, monospace;'>
                                            {codigo}
                                        </div>
                                        <p style='margin: 15px 0 0 0; font-size: 14px; color: #6c757d;'>
                                            Este código expira en 15 minutos
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style='background-color: #f8f9fa; padding: 20px 30px; text-align: center; border-top: 1px solid #e9ecef;'>
                            <p style='margin: 0 0 10px 0; color: #1e3c72; font-weight: bold;'>
                                Servicio Nacional de Empleo Chiapas
                            </p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
        }

        private string GenerarHtmlBienvenida(string nombreCompleto)
        {
            return $@"
<!DOCTYPE html>
<html lang='es'>
<head>
    <meta charset='UTF-8'>
    <title>Bienvenido</title>
</head>
<body style='margin: 0; padding: 0; font-family: Arial, sans-serif;'>
    <table width='100%' cellpadding='0' cellspacing='0' style='background-color: #f4f4f4; padding: 20px;'>
        <tr>
            <td align='center'>
                <table width='600' cellpadding='0' cellspacing='0' style='background-color: #ffffff; border-radius: 8px; overflow: hidden;'>
                    <tr>
                        <td style='background: linear-gradient(135deg, #28a745 0%, #218838 100%); padding: 30px; text-align: center;'>
                            <h1 style='color: #ffffff; margin: 0;'>🎉 ¡Bienvenido!</h1>
                            <p style='color: #ffffff; margin: 10px 0 0 0;'>EMPLEOTÓN CHIAPAS 2025</p>
                        </td>
                    </tr>
                    <tr>
                        <td style='padding: 40px 30px;'>
                            <h2 style='color: #28a745;'>¡Hola {nombreCompleto}!</h2>
                            <p>Tu acceso al sistema ha sido creado exitosamente.</p>
                            <p>Ahora puedes:</p>
                            <ul>
                                <li>✅ Actualizar tu información personal</li>
                                <li>✅ Consultar vacantes disponibles</li>
                                <li>✅ Postularte a ofertas laborales</li>
                                <li>✅ Recibir notificaciones de oportunidades</li>
                            </ul>
                            <p><strong>¡Te deseamos mucho éxito en tu búsqueda laboral!</strong></p>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</body>
</html>";
        }
    }
}