using System.Net.Http.Json;
using PortalGalaxy.Client.Proxy.Interfaces;
using PortalGalaxy.Shared.Response;

namespace PortalGalaxy.Client.Proxy.Services;

public class CrudRestHelperBase<TRequest, TResponse> : RestBase, ICrudRestHelper<TRequest, TResponse>
where TRequest : class
where TResponse : class
{
    protected CrudRestHelperBase(string baseUrl, HttpClient httpClient) 
        : base(baseUrl, httpClient)
    {
    }

    public async Task<PaginationResponse<TResponse>> ListAsync(string? filter, int page = 1, int pageSize = 5)
    {
        var response = await HttpClient.GetFromJsonAsync<PaginationResponse<TResponse>>($"{BaseUrl}{filter}");
        if (response!.Success)
        {
            return response;
        }

        throw new InvalidOperationException(response.ErrorMessage);
    }

    public virtual async Task<ICollection<TResponse>> ListAsync()
    {
        var response = await HttpClient.GetFromJsonAsync<PaginationResponse<TResponse>>($"{BaseUrl}");
        if (response!.Success)
        {
            return response.Data!;
        }

        throw new InvalidOperationException(response.ErrorMessage);
    }

    public async Task<TRequest> FindByIdAsync(int id)
    {
        var response = await HttpClient.GetFromJsonAsync<BaseResponseGeneric<TRequest>>($"{BaseUrl}/{id}");
        if (response!.Success)
        {
            return response.Data!;
        }

        throw new InvalidOperationException(response.ErrorMessage);
    }

    public async Task CreateAsync<TResponseOutput>(TRequest request)
        where TResponseOutput : BaseResponse
    {
        var response = await SendAsync<TRequest, TResponseOutput>(request, HttpMethod.Post, string.Empty);

        if (response.Success == false)
        {
            throw new InvalidOperationException(response.ErrorMessage);
        }
    }

    public async Task UpdateAsync(int id, TRequest request)
    {
        var response = await HttpClient.PutAsJsonAsync<TRequest>($"{BaseUrl}/{id}", request);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(int id)
    {
        var response = await HttpClient.DeleteAsync($"{BaseUrl}/{id}");
        response.EnsureSuccessStatusCode();
    }
}