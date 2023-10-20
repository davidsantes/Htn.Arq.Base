using Hacienda.Bll.Entities;

namespace Hacienda.Dal.Interfaces.Repositories
{
    public interface ICategoriaRepository
    {
        Task<IList<CategoriaProducto>> GetAllAsync();

        Task<Result<int>> InsAsync(CategoriaProducto categoria);
    }
}