using System.Net.Http.Json;
using PortalGalaxy.Client.Proxy.Interfaces;
using PortalGalaxy.Shared.Response;

namespace PortalGalaxy.Client.Proxy.Services;

public class JsonProxy : RestBase, IJsonProxy
{
    public JsonProxy(HttpClient httpClient)
        : base("data", httpClient)
    {

    }

    public async Task<ICollection<DepartamentoModel>> ListDepartamentos()
    {
        var departamentos = await HttpClient.GetFromJsonAsync<List<DepartamentoModel>>("data/departamentos.json") ??
                            new List<DepartamentoModel>();

        return departamentos;
    }

    public async Task<ICollection<ProvinciaModel>> ListProvincias(string codDepartamento)
    {
        var provincias = await HttpClient.GetFromJsonAsync<List<ProvinciaModel>>("data/provincias.json") ?? new List<ProvinciaModel>();

        return provincias.Where(p => p.CodigoDpto == codDepartamento).ToList();
    }

    public async Task<ICollection<DistritoModel>> ListDistritos(string codProvincia)
    {
        var provincias = await HttpClient.GetFromJsonAsync<List<DistritoModel>>("data/distritos.json") ?? new List<DistritoModel>();

        return provincias.Where(p => p.CodProvincia == codProvincia).ToList();
    }
}