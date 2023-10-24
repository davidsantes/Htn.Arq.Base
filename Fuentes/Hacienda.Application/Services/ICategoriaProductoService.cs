using Hacienda.Application.Dtos;
using Hacienda.Application.Dtos.Result;

namespace Hacienda.Application.Services
{
    public interface ICategoriaProductoService
    {
        Task<IList<GetCategoriaProductoResponse>> GetAllAsync();
        Task<GetCategoriaProductoResponse> GetAsync(int id);
        Task<ResultRequest<int>> InsAsync(InsertCategoriaProductoRequest categoria);
    }
}