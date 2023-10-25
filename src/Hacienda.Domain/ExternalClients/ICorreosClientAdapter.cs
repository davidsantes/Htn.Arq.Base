using Hacienda.Domain.ResultErrors;

namespace Hacienda.Domain.ExternalClients;

public interface ICorreosClientAdapter
{
    Task<Result<bool>> InsAsync();
}