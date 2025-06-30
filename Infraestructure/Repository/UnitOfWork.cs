using System.Collections;
using Infraestructure.Context;
using Infraestructure.Repository;
using Microsoft.EntityFrameworkCore.Storage;

public class UnitOfWork : IUnitOfWork
{
    private Hashtable? _repositories { get; }
    private readonly TransactivaDbContext _context;
    private IDbContextTransaction? _transaction; // 👈 transacción actual

    public UnitOfWork(TransactivaDbContext context)
    {
        _context = context;
        _repositories = new Hashtable();
    }

    public async Task<int> SaveChange()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _transaction?.Dispose(); // asegúrate de limpiar la transacción
        _context.Dispose();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class
    {
        var type = typeof(TEntity).Name;
        if (_repositories.ContainsKey(type))
            return (IGenericRepository<TEntity>)_repositories[type]!;

        var repositoryType = typeof(GenericRepository<>);
        var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _context);

        if (repositoryInstance != null)
        {
            _repositories.Add(type, repositoryInstance);
            return (IGenericRepository<TEntity>)repositoryInstance;
        }

        throw new Exception($"Could not create repository instance for type {type}");
    }

    // 🚀 Agrega estos métodos para manejar transacciones
    public async Task BeginTransactionAsync()
    {
        if (_transaction == null)
            _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}