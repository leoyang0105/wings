using Wings.Domain.Entities;

namespace Wings.Domain.Repositories
{
    public interface IRepository { }
    public interface IRepository<TEntity> : IReadOnlyBaseRepository<TEntity>, IBaseRepository<TEntity>
        where TEntity : class
    { }
    public interface IRepository<TEntity, TKey> : IReadOnlyBaseRepository<TEntity, TKey>, IBaseRepository<TEntity, TKey>
        where TEntity : class
    { }
}
