using PortalGalaxy.Shared.Request;
using PortalGalaxy.Shared.Response;

namespace PortalGalaxy.Client.Proxy.Interfaces;

public interface IUserProxy
{
    Task<LoginDtoResponse> Login(LoginDtoRequest request);

    Task Register(RegistrarUsuarioDto request);
}