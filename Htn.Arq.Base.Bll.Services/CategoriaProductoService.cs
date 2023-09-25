using Htn.Arq.Base.Bll.Entities;
using Htn.Arq.Base.Bll.Services.Interfaces;
using Htn.Arq.Base.Dal.Adapters.Interfaces;
using Htn.Arq.Base.Dal.Repositories.Interfaces;
using Htn.Infrastructure.Core.Exceptions.Entities;
using Htn.Infrastructure.Global.Resources;

namespace Htn.Arq.Base.Bll.Services
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