namespace PortalGalaxy.Shared.Response;

public class LoginDtoResponse : BaseResponse
{
    public string NombreCompleto { get; set; } = default!;

    public string Token { get; set; } = default!;

    public List<string> Roles { get; set; } = default!;
}