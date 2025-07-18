using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Application.Data.General;
public interface IApplicationGeneralDbContext
{
    ChangeTracker ChangeTracker { get; }
    EntityEntry Entry(object entity);
	Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync(CancellationToken cancellationToken = default);
    Task CommitTransactionAsync(CancellationToken cancellationToken = default);
    Task RollbackTransactionAsync(CancellationToken cancellationToken = default);
}