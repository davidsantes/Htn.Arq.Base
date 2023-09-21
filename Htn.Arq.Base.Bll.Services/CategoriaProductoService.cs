using Htn.Arq.Base.Bll.Entities;
using Htn.Arq.Base.Bll.Services.Interfaces;
using Htn.Arq.Base.Dal.Repositories.Interfaces;

namespace Htn.Arq.Base.Bll.Services
{
    public class CategoriaProductoService : ICategoriaProductoService
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public CategoriaProductoService(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        public async Task<IList<CategoriaProducto>> GetCategoriasProductoAsync()
        {
            var categorias = await _categoriaRepository.GetAllAsync();
            return categorias;
        }

        public async Task<Result<int>> InsCategoriaProductoAsync(CategoriaProducto categoria)
        {
            var insResult = await _categoriaRepository.InsAsync(categoria);
            return insResult;
        }
    }
}