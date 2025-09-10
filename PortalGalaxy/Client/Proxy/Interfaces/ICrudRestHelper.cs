using PortalGalaxy.Shared.Response;

namespace PortalGalaxy.Client.Proxy.Interfaces;

public interface ICrudRestHelper<TRequest, TResponse>
    where TRequest : class
    where TResponse : class
{
    Task<ICollection<TResponse>> ListAsync();

    Task<TRequest> FindByIdAsync(int id);

    Task CreateAsync<TOutputResponse>(TRequest request) where TOutputResponse : BaseResponse;

    Task UpdateAsync(int id, TRequest request);

    Task DeleteAsync(int id);
}