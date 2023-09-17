using Htn.Arq.Base.Bll.Entities;

namespace Htn.Arq.Base.Bll.Services.Interfaces
{
    public interface ICategoriaProductoService
    {
        Task<List<CategoriaProducto>> GetCategoriasProductoAsync();

        Task<int> InsCategoriaProductoAsync(CategoriaProducto categoria);
    }
}