using AutoMapper;
using FluentValidation;
using Hacienda.Application.Dtos;
using Hacienda.Application.Dtos.Result;
using Hacienda.Domain.Entities;
using Hacienda.Domain.Exceptions.Specific;
using Hacienda.Domain.ExternalClients;
using Hacienda.Domain.Repositories;

namespace Hacienda.Application.Services;

public class CategoriaProductoService : ICategoriaProductoService
{
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly ICorreosClientAdapter _correosAdapter;
    private readonly IMapper _mapper;
    private readonly IValidator<InsertCategoriaProductoRequest> _validatorInsertCategoria;
    private readonly IValidator<UpdateCategoriaProductoRequest> _validatorUpdateCategoria;

    public CategoriaProductoService(ICategoriaRepository categoriaRepository,
        ICorreosClientAdapter correosAdapter,
        IMapper mapper,
        IValidator<InsertCategoriaProductoRequest> validatorInsertCategoria,
        IValidator<UpdateCategoriaProductoRequest> validatorUpdateCategoria)
    {
        _categoriaRepository = categoriaRepository;
        _correosAdapter = correosAdapter;
        _mapper = mapper;
        _validatorInsertCategoria = validatorInsertCategoria;
        _validatorUpdateCategoria = validatorUpdateCategoria;
    }

    /// <inheritdoc />
    public async Task<IList<GetCategoriaProductoResponse>> GetAllAsync()
    {
        var categorias = await _categoriaRepository.GetAllAsync();
        var listaCategoriasProductoResponse = _mapper.Map<List<GetCategoriaProductoResponse>>(categorias);
        return listaCategoriasProductoResponse;
    }

    /// <inheritdoc />
    public async Task<GetCategoriaProductoResponse> GetAsync(int id)
    {
        var categoria = await _categoriaRepository.GetAsync(id);

        if (categoria == null)
        {
            throw new CategoriaNotFoundException(id);
        }

        var categoriaProductoResponse = _mapper.Map<GetCategoriaProductoResponse>(categoria);
        return categoriaProductoResponse;
    }

    /// <inheritdoc />
    public async Task<ResultRequest<int>> InsAsync(InsertCategoriaProductoRequest nuevaCategoriaRequest)
    {
        _validatorInsertCategoria.ValidateAndThrow(nuevaCategoriaRequest);

        var mappedCategoria = _mapper.Map<CategoriaProducto>(nuevaCategoriaRequest);
        var insResult = await _categoriaRepository.InsAsync(mappedCategoria);
        var resultEnvioCorreo = await _correosAdapter.InsAsync();

        if (resultEnvioCorreo.IsSuccess)
        {
            return new ResultRequest<int>(insResult);
        }
        else
        {
            throw new CategoriaOperationException();
        }
    }

    /// <inheritdoc />
    public async Task<ResultRequest<int>> UpdAsync(UpdateCategoriaProductoRequest categoriaRequest)
    {
        _validatorUpdateCategoria.ValidateAndThrow(categoriaRequest);

        var existingCategoria = await _categoriaRepository.GetAsync(categoriaRequest.Id);
        if (existingCategoria == null)
        {
            throw new CategoriaNotFoundException(categoriaRequest.Id);
        }

        var updatedCategoria = _mapper.Map(categoriaRequest, existingCategoria);
        var result = await _categoriaRepository.UpdAsync(updatedCategoria);
        return new ResultRequest<int>(result);
    }

    /// <inheritdoc />
    public async Task<ResultRequest<int>> DelAsync(int id)
    {
        var existingCategoria = await _categoriaRepository.GetAsync(id);
        if (existingCategoria == null)
        {
            throw new CategoriaNotFoundException(id);
        }

        var result = await _categoriaRepository.DelAsync(id);
        return new ResultRequest<int>(result);
    }
}