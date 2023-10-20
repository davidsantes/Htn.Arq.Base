using Hacienda.Domain.Entities;

namespace Hacienda.Application.Contracts.Services
{
    public interface ICategoriaProductoService
    {
        Task<IList<CategoriaProducto>> GetCategoriasProductoAsync();

        Task<Result<int>> InsCategoriaProductoAsync(CategoriaProducto categoria);
    }
}