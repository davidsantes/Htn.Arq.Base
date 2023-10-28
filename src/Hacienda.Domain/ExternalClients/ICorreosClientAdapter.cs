using Hacienda.Domain.Results;

namespace Hacienda.Domain.ExternalClients;

public interface ICorreosClientAdapter
{
    Task<ResultToReturnWithoutObject> InsAsync();
}