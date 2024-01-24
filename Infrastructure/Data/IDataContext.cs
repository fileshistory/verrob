using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public interface IDataContext
{
    public DbSet<TEntity> Set<TEntity>() where TEntity : class;
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}