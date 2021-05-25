using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wings.Domain.Entities;
using Wings.Domain.Uow;

namespace Wings.Domain.Repositories
{
    public interface IBaseRepository<TEntity> : IReadOnlyBaseRepository<TEntity>
        where TEntity : class, IEntity
    {
        IUnitOfWork UnitOfWork { get; }
        IQueryable<TEntity> Table { get; }
        Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task InsertRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
        Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task UpdateRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default);
    }
    public interface IBaseRepository<TEntity, TKey> : IBaseRepository<TEntity>, IReadOnlyBaseRepository<TEntity, TKey>
    where TEntity : class, IEntity<TKey>
    {
        Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default);
        Task DeleteRangeAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default);
    }
}
