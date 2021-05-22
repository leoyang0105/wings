using System;
using System.Threading;
using System.Threading.Tasks;

namespace Wings.EntityFrameworkCore
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
