using Hacienda.Bll.Entities;

namespace Hacienda.Bll.Services.Interfaces
{
    public interface ICategoriaProductoService
    {
        Task<IList<CategoriaProducto>> GetCategoriasProductoAsync();

        Task<Result<int>> InsCategoriaProductoAsync(CategoriaProducto categoria);
    }
}