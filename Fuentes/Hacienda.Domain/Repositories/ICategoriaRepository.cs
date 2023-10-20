using Hacienda.Domain.Entities;

namespace Hacienda.Domain.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IList<CategoriaProducto>> GetAllAsync();

        Task<Result<int>> InsAsync(CategoriaProducto categoria);
    }
}