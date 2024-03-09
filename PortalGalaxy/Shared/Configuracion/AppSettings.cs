#nullable disable

namespace PortalGalaxy.Shared.Configuracion
{
    public class AppSettings
    {
        public string UrlAplicacion { get; set; }

        public Jwt Jwt { get; set; }

        public SmtpConfiguration SmtpConfiguration { get; set; }
    }

    public class SmtpConfiguration
    {
        public string Server { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string FromName { get; set; }
    }

    public class Jwt
    {
        public string SecretKey { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }

}
