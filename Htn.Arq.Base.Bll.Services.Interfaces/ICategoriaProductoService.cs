using Htn.Arq.Base.Bll.Entities;

namespace Htn.Arq.Base.Bll.Services.Interfaces
{
    public interface ICategoriaProductoService
    {
        Task<List<CategoriaProducto>> GetCategoriasProductoAsync();

        Task<Result<int>> InsCategoriaProductoAsync(CategoriaProducto categoria);
    }
}