#nullable disable

namespace PortalGalaxy.Shared.Response;

public class BadRequestResponse
{
    public string Type { get; set; }
    public string Title { get; set; }
    public int Status { get; set; }
    public string TraceId { get; set; }
}