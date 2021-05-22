using Microsoft.EntityFrameworkCore;

namespace Wings.EntityFrameworkCore
{
    public interface IDbContext : IUnitOfWork
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
