using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Shared.Configuracion;

namespace PortalGalaxy.Services.Implementaciones;

public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly SmtpConfiguration _configuracion;
    
    public EmailService(IOptions<AppSettings> configuracion, ILogger<EmailService> logger)
    {
        _logger = logger;
        _configuracion = configuracion.Value.SmtpConfiguration;
    }

    public async Task SendEmailAsync(string email, string asunto, string mensaje)
    {
        try
        {
            var mailMessage =
                new MailMessage(
                    new MailAddress(_configuracion.UserName,
                        _configuracion.FromName), new MailAddress(email));

            mailMessage.Subject = asunto;
            mailMessage.Body = mensaje;
            mailMessage.IsBodyHtml = true;

            using var smtpClient = new SmtpClient(_configuracion.Server, _configuracion.Port);
            smtpClient.Credentials = new NetworkCredential(_configuracion.UserName, _configuracion.Password);
            smtpClient.EnableSsl = _configuracion.EnableSsl;

            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (SmtpException ex)
        {
            _logger.LogWarning(ex, "No se puede enviar el correo a {email}", email);
        }
        catch (Exception ex)
        {
            _logger.LogCritical(ex, "Error no controlado {Message} - {email}", ex.Message, email);
        }
    }
}