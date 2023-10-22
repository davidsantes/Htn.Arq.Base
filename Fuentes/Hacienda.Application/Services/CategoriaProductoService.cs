using AutoMapper;
using FluentValidation;
using Hacienda.Application.Clients;
using Hacienda.Application.Dtos;
using Hacienda.Application.Dtos.Result;
using Hacienda.Domain.Entities;
using Hacienda.Domain.Repositories;
using Hacienda.Shared.Core.Exceptions.Entities;
using Hacienda.Shared.Global.Resources;

namespace Hacienda.Application.Services
{
    public class CategoriaProductoService : ICategoriaProductoService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICorreosClientAdapter _correosAdapter;
        public readonly IMapper _mapper;
        private readonly IValidator<InsertCategoriaProductoRequest> _validatorInsertCategoria;

        public CategoriaProductoService(ICategoriaRepository categoriaRepository,
            ICorreosClientAdapter correosAdapter,
            IMapper mapper,
            IValidator<InsertCategoriaProductoRequest> validator)
        {
            _categoriaRepository = categoriaRepository;
            _correosAdapter = correosAdapter;
            _mapper = mapper;
            _validatorInsertCategoria = validator;
        }

        public async Task<IList<GetCategoriaProductoResponse>> GetCategoriasProductoAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            var listaCategoriasProductoResponse = _mapper.Map<List<GetCategoriaProductoResponse>>(categorias);
            return listaCategoriasProductoResponse;
        }

        public async Task<ResultRequest<int>> InsCategoriaProductoAsync(InsertCategoriaProductoRequest nuevaCategoriaRequest)
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
                throw new CustomException(Global_Resources.MsgOperacionKo);
            }
        }
    }
}