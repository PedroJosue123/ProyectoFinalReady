using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class OrderRequestMapper
{
    public static OrderGetRequestDomain ToDomain(Pedido entity) => new OrderGetRequestDomain
    (entity.IdPedido, entity.IdCompradorNavigation.Userprofile.Name, entity.IdPedidosProductosNavigation.Producto,
        entity.IdPedidosProductosNavigation.Cantidad, entity.IdPedidosProductosNavigation.Descripcion, entity.IdPedidosProductosNavigation.DireccionEntrega,
        entity.IdPedidosProductosNavigation.FechaLlegadaAcordada, entity.IdPedidosProductosNavigation.IdPagoNavigation.Monto, entity.IdPedidosProductosNavigation.IdPedidosProductos
    );




}