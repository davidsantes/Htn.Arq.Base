using Hacienda.Domain.Entities;

namespace Hacienda.Application.Adapters
{
    public interface ICorreosAdapter
    {
        Task<Result<bool>> InsAsync();
    }
}