namespace Domain.Interface;

public interface IUnitOfWork : IDisposable 
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : class;
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();

    Task<int> SaveChange();
}