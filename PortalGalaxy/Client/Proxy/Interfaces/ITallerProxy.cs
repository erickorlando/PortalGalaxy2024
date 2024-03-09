using PortalGalaxy.Shared.Request;
using PortalGalaxy.Shared.Response;

namespace PortalGalaxy.Client.Proxy.Interfaces;

public interface ITallerProxy
{
    Task<PaginationResponse<TallerDtoResponse>> ListAsync(BusquedaTallerRequest request);
}