using AutoMapper;
using FluentValidation;
using Hacienda.Application.Dtos;
using Hacienda.Application.Dtos.Result;
using Hacienda.Domain.Entities;
using Hacienda.Domain.ExternalClients;
using Hacienda.Domain.Repositories;
using Hacienda.Shared.Global.Resources;

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
    public async Task<GetCategoriaProductoResponse> GetAsync(Guid id)
    {
        var categoria = await _categoriaRepository.GetByIdAsync(id);
        var categoriaProductoResponse = _mapper.Map<GetCategoriaProductoResponse>(categoria);
        return categoriaProductoResponse;
    }

    /// <inheritdoc />
    public async Task<ResultRequest<Guid>> InsAsync(InsertCategoriaProductoRequest nuevaCategoriaRequest)
    {
        _validatorInsertCategoria.ValidateAndThrow(nuevaCategoriaRequest);

        var mappedCategoria = _mapper.Map<Categoria>(nuevaCategoriaRequest);
        var categoriaInsertada = await _categoriaRepository.AddAndCommitAsync(mappedCategoria);
        var resultEnvioCorreo = await _correosAdapter.InsAsync();

        var result = new ResultRequest<Guid>(categoriaInsertada.Id);

        if (categoriaInsertada != null && !resultEnvioCorreo.IsSuccess)
        {
            result.Errors.Add("MsgOperacionSinEfecto", GlobalResources.MsgOperacionSinEfecto);
        }

        return result;
    }

    /// <inheritdoc />
    public async Task<ResultRequest<int>> UpdAsync(UpdateCategoriaProductoRequest categoriaRequest)
    {
        _validatorUpdateCategoria.ValidateAndThrow(categoriaRequest);

        var existingCategoria = await _categoriaRepository.GetByIdAsync(categoriaRequest.Id);
        var updatedCategoria = _mapper.Map(categoriaRequest, existingCategoria);
        var result = await _categoriaRepository.UpdateAndCommitAsync(updatedCategoria);
        return new ResultRequest<int>(result);
    }

    /// <inheritdoc />
    public async Task<ResultRequest<int>> DelAsync(Guid id)
    {
        var categoriaAfectada = await _categoriaRepository.DeleteAndSaveAsync(id);
        return new ResultRequest<int>(categoriaAfectada);
    }
}