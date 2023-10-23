using Hacienda.Domain.Entities;

namespace Hacienda.Domain.ExternalClients
{
    public interface ICorreosClientAdapter
    {
        Task<Result<bool>> InsAsync();
    }
}