using Domain.Interface;
using Infraestructure.Context;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository;



public class SendOrderRepository(TransactivaDbContext context) : ISendOrderRepository<Preparacion>
{
    public async Task<Preparacion?> GetPreparedOrderWithShipmentAsync(int preparationId, CancellationToken cancellationToken)
    {
        return await context.Preparacions
            .Where(p => p.IdPreparacion == preparationId && p.Estado == true)
            .Include(p => p.IdEnvioNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }
}