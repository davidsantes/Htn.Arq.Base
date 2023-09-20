using Htn.Arq.Base.Bll.Entities;

namespace Htn.Arq.Base.Dal.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<CategoriaProducto>> GetAllAsync();

        Task<Result<int>> InsAsync(CategoriaProducto categoria);
    }
}