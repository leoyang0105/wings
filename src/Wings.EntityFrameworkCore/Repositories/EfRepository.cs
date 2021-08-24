using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Wings.Domain.Entities;
using Wings.Domain.Repositories;
using Wings.Domain.Uow;

namespace Wings.EntityFrameworkCore.Repositories
{
    public class EfRepository<TDbContext, TEntity> : RepositoryBase<TEntity>, IRepository<TEntity>
        where TDbContext : IDbContext
        where TEntity : class, IEntity
    {
        private readonly TDbContext _dbContext;
        private DbSet<TEntity> _entities;
        public EfRepository(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public override IUnitOfWork UnitOfWork => _dbContext;
        public override IQueryable<TEntity> Table => Entities;
        public override IQueryable<TEntity> TableNoTracking => Entities.AsNoTracking();
        public DbSet<TEntity> Entities
        {
            get
            {
                if (_entities == null)
                    _entities = _dbContext.Set<TEntity>();
                return _entities;
            }
        }


        public override async Task<long> GetCountAsync(CancellationToken cancellationToken = default)
        {
            return await Entities.LongCountAsync(cancellationToken);
        }

        public override async Task<List<TEntity>> GetListAsync(CancellationToken cancellationToken = default)
        {
            return await Entities.ToListAsync(cancellationToken);
        }

        public override async Task<TEntity> InsertAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            Entities.Add(entity);
            if (autoSave)
            {
                await SaveChangesAsync(cancellationToken);
            }
            return entity;
        }
        public override async Task InsertRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            Entities.AddRange(entities);
            if (autoSave)
            {
                await SaveChangesAsync(cancellationToken);
            }
        }

        public override async Task<TEntity> UpdateAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            var updatedEntity = Entities.Update(entity).Entity;
            if (autoSave)
                await SaveChangesAsync(cancellationToken);
            return updatedEntity;
        }
        public override async Task UpdateRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            Entities.UpdateRange(entities);
            if (autoSave)
            {
                await SaveChangesAsync(cancellationToken);
            }
        }
        public override async Task DeleteAsync([NotNull] TEntity entity, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            Entities.Remove(entity);
            if (autoSave)
            {
                await SaveChangesAsync(cancellationToken);
            }
        }
        public override async Task DeleteRangeAsync([NotNull] IEnumerable<TEntity> entities, bool autoSave = false, CancellationToken cancellationToken = default)
        {
            Entities.RemoveRange(entities);
            if (autoSave)
            {
                await SaveChangesAsync(cancellationToken);
            }
        }
        protected override async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await UnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public override async Task<TEntity> FindByIdAsync(object id)
        {
            return await Entities.FindAsync(id);
        }
    }
    public class EfRepository<TDbContext, TEntity, TKey> : EfRepository<TDbContext, TEntity>, IRepository<TEntity, TKey>
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
