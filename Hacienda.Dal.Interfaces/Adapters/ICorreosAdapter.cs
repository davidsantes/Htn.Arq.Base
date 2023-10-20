using Hacienda.Bll.Entities;

namespace Hacienda.Dal.Interfaces.Adapters
{
    public interface ICorreosAdapter
    {
        Task<Result<bool>> InsAsync();
    }
}