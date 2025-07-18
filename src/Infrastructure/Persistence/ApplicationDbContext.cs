using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Npgsql;

namespace Infrastructure.Persistence;

public abstract class ApplicationDbContext : DbContext, IUnitOfWork
{
    public readonly IPublisher _publiser;
    private IDbContextTransaction? _currentTransaction;
    public ApplicationDbContext(DbContextOptions options,
        IPublisher publiser) : base(options)
    {
        _publiser = publiser ?? throw new ArgumentNullException(nameof(publiser));
    }

    protected abstract override void OnModelCreating(ModelBuilder modelBuilder);

    public override async Task<int> SaveChangesAsync(
        CancellationToken cancellationToken = default)
    {
        try
        {
            return await base.SaveChangesAsync(cancellationToken);
        }
        catch (PostgresException pgEx)  
        {
            throw new Exception(
                $"Error en BD (SQLState={pgEx.SqlState}): {pgEx.MessageText}",
                pgEx);
        }
        catch (Exception dbEx) 
        {
            var inner = dbEx.GetBaseException();
            throw new Exception(
                $"Error al actualizar la base de datos: {inner.Message}",
                dbEx);
        }
    }
    public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        if (_currentTransaction != null)
        {
            return;
        }

        _currentTransaction = await Database.BeginTransactionAsync(cancellationToken);
    }

    public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync(cancellationToken);
            }
        }
        catch
        {
            await RollbackTransactionAsync(cancellationToken);
            throw;
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

    public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync(cancellationToken);
            }
        }
        finally
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }

}