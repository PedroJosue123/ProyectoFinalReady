using Domain.Entities;
using Domain.Interface;
using Infraestructure.Context;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository;

public class OrderBuyerRepository(TransactivaDbContext _context) : IOrderBuyerRepository<Pedido>
{
    public async Task<List<Pedido>> GetBuyerOrdersAsync(int buyerId, CancellationToken cancellationToken)
    {
        return await _context.Pedidos
            .Where(p => p.IdComprador == buyerId)
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPagoNavigation)
            .Include(p => p.IdProveedorNavigation)
            .ThenInclude(prov => prov.Userprofile)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Pedido?> GetPreparedOrderDetailAsync(int buyerId, int orderId, CancellationToken cancellationToken)
    {
        return await _context.Pedidos
            .Where(p => p.IdComprador == buyerId && p.IdPedido == orderId)
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPreparacionNavigation)
            .ThenInclude(pr => pr.IdEnvioNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Pedido>> GetPreparedOrdersListAsync(int buyerId, CancellationToken cancellationToken)
    {
        return await _context.Pedidos
            .Where(p => p.IdComprador == buyerId &&
                        p.IdPedidosProductosNavigation.IdPreparacionNavigation.Estado == true)
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPreparacionNavigation)
            .ThenInclude(pr => pr.IdEnvioNavigation)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }


}