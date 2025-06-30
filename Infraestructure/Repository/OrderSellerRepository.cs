using Domain.Interface;
using Infraestructure.Context;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository;

public class OrderSellerRepository (TransactivaDbContext context) : IOrderSellerRepository<Pedido>
{
    public async Task<List<Pedido>> GetSellerPreparedOrdersListAsync(int sellerId, CancellationToken cancellationToken)
    {
        return await context.Pedidos
            .Where(p => p.IdProveedor == sellerId && p.IdPedidosProductosNavigation.IdPagoNavigation.Estado == true)
            .Include(p => p.IdCompradorNavigation.Userprofile)
            .Include(p => p.IdPedidosProductosNavigation)
            .Include(p => p.IdPedidosProductosNavigation.IdPreparacionNavigation)
            .ThenInclude(pr => pr.IdEnvioNavigation)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Pedido?> GetPreparationPreviewAsync(int idPedidosProducto, CancellationToken cancellationToken)
    {
        return await context.Pedidos
            .Where(p => p.IdPedidosProductos == idPedidosProducto)
            .Include(p => p.IdCompradorNavigation.Userprofile)
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPagoNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);
    }
}