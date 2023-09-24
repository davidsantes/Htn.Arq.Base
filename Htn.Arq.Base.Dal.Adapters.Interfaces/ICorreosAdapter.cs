
using Htn.Arq.Base.Bll.Entities;

namespace Htn.Arq.Base.Dal.Adapters.Interfaces
{
    public interface ICorreosAdapter
    {
        Task<Result<bool>> InsAsync();
    }
}