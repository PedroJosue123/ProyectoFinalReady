using Domain.Interface;
using Infraestructure.Context;
using Infraestructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repository;


public class PaymentOrderRepository(TransactivaDbContext context) : IPaymentOrderRepository <Userprofile, Pedido>
{
    public async Task<Userprofile?> GetUserProfileWithPasswordAsync(int userId, CancellationToken cancellationToken)
    {
        return await context.Userprofiles
            .Where(p => p.UserId == userId)
            .Select(p => new Userprofile
            {
                UserId = p.UserId,
                PaymentPassword = p.PaymentPassword
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

  
    public async Task<Pedido?> GetPedidoWithPaymentAsync(int orderId, CancellationToken cancellationToken)
    {
        return await context.Pedidos
            .Where(p => p.IdPedido == orderId && p.Estado == true)
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPagoNavigation)
            .FirstOrDefaultAsync(cancellationToken);
    }

    
    public async Task<Pedido?> GetPedidoDataForPaymentAsync(int idPedido, CancellationToken cancellationToken)
    {
        return await context.Pedidos
            .Include(p => p.IdPedidosProductosNavigation)
            .ThenInclude(pp => pp.IdPagoNavigation)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.IdPedido == idPedido && p.Estado == true, cancellationToken);
    }
}