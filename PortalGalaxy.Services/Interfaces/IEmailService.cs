namespace PortalGalaxy.Services.Interfaces;

public interface IEmailService
{
    Task SendEmailAsync(string email, string asunto, string mensaje);
}