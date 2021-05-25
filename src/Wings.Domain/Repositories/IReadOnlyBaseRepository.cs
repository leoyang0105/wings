using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wings.Domain.Entities;

namespace Wings.Domain.Repositories
{
    public interface IReadOnlyBaseRepository<TEntity> : IRepository
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> TableNoTracking { get; }
        Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);
        Task<long> GetCountAsync(CancellationToken cancellationToken = default);
    }
    public interface IReadOnlyBaseRepository<TEntity, TKey> : IReadOnlyBaseRepository<TEntity>
        where TEntity : class, IEntity<TKey>
    {
        Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
