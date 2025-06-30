using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class GetOrderMapper
{
  
    
    public static GetOrderDomain ToDomain(Pedido entity)
    {
        // Extraer los estados
        DateTime? fechapagado = entity.IdPedidosProductosNavigation.IdPagoNavigation.FechaPago;

        bool estadoPedido = entity.Estado ?? false;
        bool estadoPago = entity.IdPedidosProductosNavigation.IdPagoNavigation?.Estado ?? false;
        bool estadoPreparado = entity.IdPedidosProductosNavigation.IdPreparacionNavigation?.Estado ?? false;

        // Lógica del estado
        string estadoString;

        if (estadoPedido && estadoPago && estadoPreparado)
        {
            estadoString = "El pedido está preparado";
        }
        else if (estadoPedido && estadoPago)
        {
            estadoString = "Aceptado y pagado pero no preparado";
        }
        else if (estadoPedido && !estadoPago)
        {
            fechapagado = null;
            estadoString = "Pedido aceptado pero no pagado";
        }
        else
        {
            estadoString = "No aceptado";
        }

        return new GetOrderDomain(
            estadoString,
            entity.IdPedidosProductosNavigation.FechaSolicitada,
            fechapagado,
            entity.IdPedidosProductosNavigation.Producto,
            entity.IdPedidosProductosNavigation.Cantidad,
            entity.IdPedidosProductosNavigation.Descripcion,
            entity.IdProveedorNavigation.Userprofile.Name,
            entity.IdPedidosProductosNavigation.DireccionEntrega,
            entity.IdPedidosProductosNavigation.IdPagoNavigation.Monto,
            entity.IdPedidosProductosNavigation.FechaLlegadaAcordada,
            entity.IdPedidosProductosNavigation.NombreTransaccion
        );
    }

}