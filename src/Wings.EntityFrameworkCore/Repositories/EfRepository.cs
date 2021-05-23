using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wings.Domain.Entities;
using Wings.Domain.Repositories;

namespace Wings.EntityFrameworkCore.Repositories
{
    public class EfRepository<TDbContext, TEntity> : RepositoryBase<TEntity>, IEfRepository<TEntity>
        where TDbContext : IDbContext
        where TEntity : class, IEntity
    {
        private DbSet<TEntity> _entities;
        public EfRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public IDbContext DbContext { get; private set; }
        public DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = this.DbContext.Set<TEntity>();
                return _entities;
            }
        }

        public override async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await this.Entities.LongCountAsync(cancellationToken);
        }

        public override async Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await this.Entities.ToListAsync(cancellationToken);
        }

        public override async Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            this.Entities.Add(entity);
            if (autoSave)
            {
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
            return entity;
        }
        public override async Task InsertRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            this.Entities.RemoveRange(entities);
            if (autoSave)
            {
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public override async Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var updatedEntity = Entities.Update(entity).Entity;
            if (autoSave)
                await this.DbContext.SaveChangesAsync(cancellationToken);
            return updatedEntity;
        }
        public override async Task UpdateRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            this.Entities.UpdateRange(entities);
            if (autoSave)
            {
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public override async Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            this.Entities.Remove(entity);
            if (autoSave)
            {
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
        }
        public override async Task DeleteRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            this.Entities.RemoveRange(entities);
            if (autoSave)
            {
                await this.DbContext.SaveChangesAsync(cancellationToken);
            }
        }
        protected override async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await this.DbContext.SaveChangesAsync(cancellationToken);
        }
    }
    public class EfRepository<TDbContext, TEntity, TKey> : EfRepository<TDbContext, TEntity>, IEfRepository<TEntity, TKey>
        where TDbContext : IDbContext
        where TEntity : class, IEntity<TKey>
    {
        public EfRepository(TDbContext dbContext) : base(dbContext)
        {
        }
        public async Task DeleteAsync(TKey id, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entity = await FindAsync(id, cancellationToken);
            if (entity == null)
                return;
            await DeleteAsync(entity);
            if (autoSave)
                await SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteRangeAsync(IEnumerable<TKey> ids, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var entities = await Entities.Where(e => ids.Contains(e.Id)).ToListAsync(cancellationToken);
            if (!entities.Any())
                return;
            Entities.RemoveRange(entities);
            if (autoSave)
                await SaveChangesAsync(cancellationToken);
        }

        public async Task<TEntity> FindAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await Entities.FindAsync(id, cancellationToken);
        }
    }
}
