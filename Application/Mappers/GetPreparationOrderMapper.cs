using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class GetPreparationOrderMapper
{
    public static GetPreparationOrderDomain ToDomain (Pedido entity)
    {
        // Extraer los estados 
        

        bool estadoEnviado = (bool)entity.IdPedidosProductosNavigation.IdPreparacionNavigation.IdEnvioNavigation.Estado;
        bool estadoEntregado = (bool)entity.IdPedidosProductosNavigation.IdPreparacionNavigation.IdEnvioNavigation.Llegada;

        // Lógica del estado
        string estadoString;

        if (estadoEnviado && estadoEntregado)
        {
            estadoString = "Producto Enviado y entegrado";
        }
        else if (!estadoEntregado && estadoEnviado)
        {
            
            estadoString = "Pedido enviado ";
        }
        else
        {
            estadoString = "No se ah preparado";
        }

        return new GetPreparationOrderDomain (entity.IdPedidosProductosNavigation.IdPreparacionNavigation.IdPreparacion,estadoString, entity.IdPedidosProductosNavigation.Producto, entity.IdPedidosProductosNavigation.Cantidad,
            entity.IdPedidosProductosNavigation.IdPreparacionNavigation.ComoEnvia, entity.IdPedidosProductosNavigation.IdPreparacionNavigation.Detalles
           
        );
    }
}