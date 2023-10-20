using Hacienda.Application.Dtos;
using Hacienda.Application.Dtos.Result;

namespace Hacienda.Application.Services
{
    public interface ICategoriaProductoService
    {
        Task<IList<GetCategoriaProductoResponse>> GetCategoriasProductoAsync();

        Task<ResultRequest<int>> InsCategoriaProductoAsync(InsertCategoriaProductoRequest categoria);
    }
}