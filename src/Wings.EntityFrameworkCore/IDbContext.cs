using Microsoft.EntityFrameworkCore;
using Wings.Domain.Uow;

namespace Wings.EntityFrameworkCore
{
    public interface IDbContext : IUnitOfWork
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
