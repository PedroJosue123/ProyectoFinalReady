using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class GetListPreparationOrderMapper
{
    public static GetListPreparationOrderDomain ToDomain(Pedido entity)
    {
        var envio = entity.IdPedidosProductosNavigation.IdPreparacionNavigation?.IdEnvioNavigation;

        // Manejo de estados con null-checks
        bool estadoEnviado = envio?.Estado ?? false;
        bool estadoEntregado = envio?.Llegada ?? false;

        // Determinar el estado del pedido
        string estadoString;
        if (estadoEnviado && estadoEntregado)
        {
            estadoString = "Producto enviado y entregado";
        }
        else if (estadoEnviado)
        {
            estadoString = "Pedido enviado";
        }
        else
        {
            estadoString = "Preparado, pendiente de envío";
        }

        return new GetListPreparationOrderDomain(
            entity.IdPedido,
            estadoString,
            entity.IdPedidosProductosNavigation.NombreTransaccion
        );
    }


}