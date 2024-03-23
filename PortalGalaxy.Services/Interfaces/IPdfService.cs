using PortalGalaxy.Shared.Request;
using PortalGalaxy.Shared.Response;
using QuestPDF.Fluent;

namespace PortalGalaxy.Services.Interfaces;

public interface IPdfService
{
    Task<BaseResponseGeneric<Document>> Generar(BusquedaTallerRequest request);
}