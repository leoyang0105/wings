using Microsoft.EntityFrameworkCore;
using Wings.Domain.Entities;
using Wings.Domain.Repositories;

namespace Wings.EntityFrameworkCore.Repositories
{
    public interface IEfRepository<TEntity> : IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IDbContext DbContext { get; }
        DbSet<TEntity> Entities { get; }
    }
    public interface IEfRepository<TEntity, TKey> : IEfRepository<TEntity>, IRepository<TEntity, TKey>
       where TEntity : class, IEntity<TKey>
    {
    }
}
