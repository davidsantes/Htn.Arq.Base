using Hacienda.Domain.Entities;

namespace Hacienda.Domain.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IList<CategoriaProducto>> GetAllAsync();

        Task<CategoriaProducto> GetAsync(int id);

        Task<Result<int>> InsAsync(CategoriaProducto categoria);
    }
}