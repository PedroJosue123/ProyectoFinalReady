using Domain.Interface;
using Infraestructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCase.Orders.Seller.Queries;

public record CheckIfOrderIsPaidQuery(int OrderId) : IRequest<int>;

public class CheckIfOrderIsPaidHandler(
    IOrderSellerRepository<Pedido>repository
) : IRequestHandler<CheckIfOrderIsPaidQuery, int>
{
    public async Task<int> Handle(CheckIfOrderIsPaidQuery request, CancellationToken cancellationToken)
    {
        var pedido = await repository.GetPedidoWithPagoAsync(request.OrderId, cancellationToken);

        if (pedido == null)
            throw new Exception("Order no existe");

        if (pedido.IdPedidosProductosNavigation.IdPagoNavigation.Estado != true)
            throw new Exception("Order no pagada");

        return pedido.IdPedidosProductosNavigation.IdPedidosProductos;
    }
}