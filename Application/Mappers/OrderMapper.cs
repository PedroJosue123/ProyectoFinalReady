using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class OrderMapper
{
    /*public static OrderDomain ToDomain(Pedido entity) => new OrderDomain(
        entity, entity.Idcliente , 0, en);*/

    public static Pedido ToEntity(OrderDomain domain) => new Pedido{

        IdPedido = 0,
        IdComprador = domain.IdComprador,
        IdProveedor = domain.IdProveedor,
        IdPedidosProductos = domain.IdPedidosProductos,
        Estado = domain.Estado
    
    };

    public static OrderDomain ToDomain(Pedido entity) => new OrderDomain(entity.IdProveedor, entity.IdComprador,
        entity.IdPedidosProductos, entity.Estado
    );




}