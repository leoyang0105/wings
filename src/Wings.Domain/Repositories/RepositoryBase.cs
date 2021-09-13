using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wings.Domain.Entities;
using Wings.Domain.Uow;

namespace Wings.Domain.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        public abstract IUnitOfWork UnitOfWork { get; }

        public abstract IQueryable<TEntity> Table { get; }

        public abstract IQueryable<TEntity> TableNoTracking { get; }
        public abstract Task<TEntity> FindByIdAsync(object id);
        protected abstract Task SaveChangesAsync(CancellationToken cancellationToken);
        public abstract Task<long> GetCountAsync(CancellationToken cancellationToken = default);
        public abstract Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default);
        public abstract Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);
        public abstract Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        public abstract Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default);

        public virtual async Task DeleteRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                await this.DeleteAsync(entity, false, cancellationToken);
            }
            if (autoSave)
            {
                await this.SaveChangesAsync(cancellationToken);
            }
        }
        public virtual async Task InsertRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                await this.InsertAsync(entity, false, cancellationToken);
            }
            if (autoSave)
            {
                await this.SaveChangesAsync(cancellationToken);
            }
        }
        public virtual async Task UpdateRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            foreach (var entity in entities)
            {
                await this.DeleteAsync(entity, false, cancellationToken);
            }
            if (autoSave)
            {
                await this.SaveChangesAsync(cancellationToken);
            }
        }
        public abstract EntityEntry<TEntity> Entry(TEntity entity);
    }
}
