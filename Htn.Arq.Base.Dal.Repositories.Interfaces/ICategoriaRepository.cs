using Htn.Arq.Base.Bll.Entities;

namespace Htn.Arq.Base.Dal.Repositories.Interfaces
{
    public interface ICategoriaRepository
    {
        Task<List<CategoriaProducto>> GetAllAsync();

        Task<int> InsAsync(CategoriaProducto categoria);
    }
}