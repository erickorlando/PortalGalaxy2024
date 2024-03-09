using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalGalaxy.Entities;
using PortalGalaxy.Entities.Infos;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Services.Utils;
using PortalGalaxy.Shared.Request;
using PortalGalaxy.Shared.Response;

namespace PortalGalaxy.Services.Implementaciones;

public class TallerService : ITallerService
{
    private readonly ITallerRepository _repository;
    private readonly ILogger<TallerService> _logger;
    private readonly IMapper _mapper;

    public TallerService(ITallerRepository repository, ILogger<TallerService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<TallerDtoResponse>> ListAsync(BusquedaTallerRequest request)
    {
        var response = new PaginationResponse<TallerDtoResponse>();
        try
        {
            var tupla = await _repository.ListAsync(predicado: p => p.Nombre.Contains(request.Nombre ?? string.Empty)
                && (request.CategoriaId == null || p.CategoriaId == request.CategoriaId)
                && (request.Situacion == null || p.Situacion == (SituacionTaller)request.Situacion),
                selector: p => new TallerInfo
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Categoria = p.Categoria.Nombre,
                    Fecha = p.FechaInicio,
                    Instructor = p.Instructor.Nombres,
                    Situacion = p.Situacion.ToString().Replace('_', ' ')
                },
                orderBy: x => x.Nombre,
                relaciones: "Categoria,Instructor",
                pagina: request.Pagina,
                filas: request.Filas);

            response.Data = _mapper.Map<ICollection<TallerDtoResponse>>(tupla.Collection);
            response.TotalPages = Helper.GetTotalPages(tupla.Total, request.Filas);

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al listar los talleres";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }
}