using Hacienda.Domain.Entities;

namespace Hacienda.Application.Clients
{
    public interface ICorreosClientAdapter
    {
        Task<Result<bool>> InsAsync();
    }
}