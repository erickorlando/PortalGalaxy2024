using AutoMapper;
using Microsoft.Extensions.Logging;
using PortalGalaxy.Entities;
using PortalGalaxy.Repositories.Interfaces;
using PortalGalaxy.Services.Interfaces;
using PortalGalaxy.Shared.Request;
using PortalGalaxy.Shared.Response;

namespace PortalGalaxy.Services.Implementaciones;

public class CategoriaService : ICategoriaService
{
    private readonly ICategoriaRepository _repository;
    private readonly ILogger<CategoriaService> _logger;
    private readonly IMapper _mapper;

    public CategoriaService(ICategoriaRepository repository, ILogger<CategoriaService> logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<PaginationResponse<CategoriaDtoResponse>> ListAsync()
    {
        var response = new PaginationResponse<CategoriaDtoResponse>();
        try
        {
            var collection = await _repository.ListAsync();

            response.Data = _mapper.Map<ICollection<CategoriaDtoResponse>>(collection);
            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al listar las categorias";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;
    }

    public async Task<BaseResponseGeneric<CategoriaDtoRequest>> FindByIdAsync(int id)
    {
        var response = new BaseResponseGeneric<CategoriaDtoRequest>();
        try
        {
            var entidad = await _repository.FindAsync(id);

            response.Data = _mapper.Map<CategoriaDtoRequest>(entidad);
            response.Success = true;
        }
        catch (Exception ex)
        {
             response.ErrorMessage = "Error al otener la categoria";
            _logger.LogCritical(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }
        return response;
    }

    public async Task<BaseResponse> CreateAsync(CategoriaDtoRequest request)
    {
        var response = new BaseResponse();

        try
        {
            // Codigo
            await _repository.AddAsync(_mapper.Map<Categoria>(request));

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al crear la categoria";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }

    public async Task<BaseResponse> UpdateAsync(int id, CategoriaDtoRequest request)
    {
        var response = new BaseResponse();

        try
        {
            // Codigo
            var registro = await _repository.FindAsync(id);
            if (registro is not null)
            {
                _mapper.Map(request, registro);

                await _repository.UpdateAsync();
            }

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al actualizar la categoria";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }

    public async Task<BaseResponse> DeleteAsync(int id)
    {
        var response = new BaseResponse();

        try
        {
            // Codigo

            await _repository.DeleteAsync(id);

            response.Success = true;
        }
        catch (Exception ex)
        {
            response.ErrorMessage = "Error al eliminar la categoria";
            _logger.LogError(ex, "{ErrorMessage} {Message}", response.ErrorMessage, ex.Message);
        }

        return response;

    }
}