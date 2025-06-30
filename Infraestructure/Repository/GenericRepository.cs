using Domain.Interface;
using Infraestructure.Context;
using Microsoft.EntityFrameworkCore;
namespace Infraestructure.Repository;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    private readonly DbSet<T> _context;

    public GenericRepository(TransactivaDbContext context)
    {
        _context = context.Set<T>();
    }
    public IQueryable<T> GetAll()
    {
        return _context.AsQueryable();
    }

    public async Task AddAsync(T entity)
    {
        await _context.AddAsync(entity);
    }
    public async Task<IEnumerable<T>> GetALLAsync()
    {
        return await _context.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _context.FindAsync(id);
    }

    public async Task UpdateAsync(int id, T entity)
    {
        var entidad = await _context.FindAsync(id);
        if (entidad != null)
        {
            _context.Update(entity);
        }
    }
    public async Task DeleteAsync(int id)
    {   
        var entity = await _context.FindAsync(id);
        if (entity != null)
        {
            _context.Remove(entity);
        }
    }


}