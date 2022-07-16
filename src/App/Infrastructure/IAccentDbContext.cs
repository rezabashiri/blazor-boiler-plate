namespace Infrastructure;

public interface IAccentDbContext
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
}