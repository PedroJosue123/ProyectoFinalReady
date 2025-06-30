namespace Domain.Interface;

public interface IGenericRepository<T> where T : class
{
    Task AddAsync(T entity);
    IQueryable<T> GetAll();    
    Task<IEnumerable<T>> GetALLAsync();
    Task<T> GetByIdAsync(int id);
    Task UpdateAsync(int id, T entity);
    Task DeleteAsync(int id);
}
