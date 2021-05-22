namespace Wings.EntityFrameworkCore
{
    public interface IDbContextProvider<TDbContext>
        where TDbContext : IDbContext
    {
        TDbContext DbContext { get; }
    }
}
