using Domain.Interface;
using Infraestructure.Context;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository;

public class UserRepository : IUserRepository<User>
{
private readonly TransactivaDbContext _context;

public UserRepository(TransactivaDbContext context)
{
    _context = context;
}

public async Task<User?> GetUserWithUserTypeByEmailAsync(string email, CancellationToken cancellationToken)
{
    return await _context.Users
        .AsNoTracking()                          // ✅ importante para lectura pura
        .Include(u => u.UserType)                // ✅ relaciones necesarias
        .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
}
}