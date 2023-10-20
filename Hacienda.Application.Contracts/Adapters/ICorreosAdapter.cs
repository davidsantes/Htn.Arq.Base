using Hacienda.Domain.Entities;

namespace Hacienda.Application.Contracts.Adapters
{
    public interface ICorreosAdapter
    {
        Task<Result<bool>> InsAsync();
    }
}