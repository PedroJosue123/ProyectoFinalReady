namespace Application.Mappers;

public class GetSellerOrderMapper
{
    public static GetSellerOrderDomain ToDomain(Pedido entity) => new GetSellerOrderDomain(
        entity.IdPedidosProductosNavigation.FechaSolicitada,entity.IdPedidosProductosNavigation.IdPagoNavigation.FechaPago,
        entity.IdCompradorNavigation.Userprofile.Name, entity.IdPedidosProductosNavigation.FechaLlegadaAcordada, entity.IdPedidosProductosNavigation.Producto,
        entity.IdPedidosProductosNavigation.Cantidad, entity.IdPedidosProductosNavigation.Descripcion, entity.IdPedidosProductosNavigation.DireccionEntrega, entity.IdPedidosProductosNavigation.IdPagoNavigation.Monto,
        entity.IdPedidosProductos
     
    );
    
   
}