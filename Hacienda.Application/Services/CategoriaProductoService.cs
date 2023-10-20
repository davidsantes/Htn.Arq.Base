using Hacienda.Application.Contracts.Adapters;
using Hacienda.Application.Contracts.Repositories;
using Hacienda.Application.Contracts.Services;
using Hacienda.Domain.Entities;
using Hacienda.Shared.Core.Exceptions.Entities;
using Hacienda.Shared.Global.Resources;

namespace Hacienda.Application.Services
{
    public class CategoriaProductoService : ICategoriaProductoService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ICorreosAdapter _correosAdapter;

        public CategoriaProductoService(ICategoriaRepository categoriaRepository,
            ICorreosAdapter correosAdapter)
        {
            _categoriaRepository = categoriaRepository;
            _correosAdapter = correosAdapter;
        }

        public async Task<IList<CategoriaProducto>> GetCategoriasProductoAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return categorias;
        }

        public async Task<Result<int>> InsCategoriaProductoAsync(CategoriaProducto categoria)
        {
            var insResult = await _categoriaRepository.InsAsync(categoria);
            var resultEnvioCorreo = await _correosAdapter.InsAsync();

            if (resultEnvioCorreo.IsSuccess)
            {
                return insResult;
            }
            else
            {
                throw new CustomException(Global_Resources.MsgOperacionKo);
            }
        }
    }
}