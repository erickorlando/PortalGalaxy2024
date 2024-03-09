using PortalGalaxy.Client.Proxy.Interfaces;
using PortalGalaxy.Shared.Request;
using PortalGalaxy.Shared.Response;
using System.Net.Http.Json;

namespace PortalGalaxy.Client.Proxy.Services;

public class TallerProxy : CrudRestHelperBase<TallerDtoRequest, TallerDtoResponse>, ITallerProxy
{
    public TallerProxy(HttpClient httpClient) 
        : base("api/Talleres", httpClient)
    {
    }

    public async Task<PaginationResponse<TallerDtoResponse>> ListAsync(BusquedaTallerRequest request)
    {
        var response = await HttpClient.GetFromJsonAsync<PaginationResponse<TallerDtoResponse>>(
            $"{BaseUrl}?nombre={request.Nombre}&categoriaid={request.CategoriaId}&situacion={request.Situacion}&pagina={request.Pagina}&filas={request.Filas}");

        if (response is { Success: true })
        {
            return response;
        }

        return await Task.FromResult(new PaginationResponse<TallerDtoResponse>());
    }
}