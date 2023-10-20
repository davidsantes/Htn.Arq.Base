using Hacienda.Domain.Entities;

namespace Hacienda.Application.Contracts.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IList<CategoriaProducto>> GetAllAsync();

        Task<Result<int>> InsAsync(CategoriaProducto categoria);
    }
}