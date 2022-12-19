
using Microsoft.EntityFrameworkCore;

namespace Constracts.Common.Interfaces
{
    public interface IUnitOfWork<TConText>: IDisposable where TConText: DbContext
    {
        Task<int> CommitAsync();
    }
}
