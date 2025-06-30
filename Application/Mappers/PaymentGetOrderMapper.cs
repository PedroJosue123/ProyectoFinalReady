using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class PaymentGetOrderMapper
{
    public  static PaymentGetRequestDomain ToDomain (Pedido entity) => new PaymentGetRequestDomain(
        entity.IdPedidosProductosNavigation.Cantidad, entity.IdPedidosProductosNavigation.Producto, 
        entity.IdPedidosProductosNavigation.IdPagoNavigation.Monto
    );
   
}