using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class GetListPreparationMapper
{
    public static GetListPrepationDomain ToDomain(Pedido entity)
    {
        // Extraer los estados
        
    
      
        
        // Verifica cada propiedad antes de acceder a ellas, y usa un valor predeterminado (false) si son null
        bool estadoEnviado = entity.IdPedidosProductosNavigation?.IdPreparacionNavigation?.IdEnvioNavigation?.Estado ?? false;
        bool estadoPreparado = entity.IdPedidosProductosNavigation?.IdPreparacionNavigation?.Estado ?? false;
        bool estadoEntregado = entity.IdPedidosProductosNavigation?.IdPreparacionNavigation?.IdEnvioNavigation?.Llegada ?? false;


        // Lógica del estado
        string estadoString;

        if (estadoEnviado && estadoEntregado && estadoPreparado)
        {
            estadoString = "Producto Entregado";
        }
        else if (!estadoEntregado && estadoEnviado && estadoPreparado)
        {
            estadoString = "Pedido preparado y enviado";
        }
        else if ( !estadoEnviado && estadoPreparado)
        {
            estadoString = "Pedido preparado";
        }
        else
        {
            estadoString = "No se ah preparado";
        }

       

        return new GetListPrepationDomain(
            entity.IdCompradorNavigation.Userprofile.Name, entity.IdPedidosProductosNavigation.Cantidad, entity.IdPedidosProductosNavigation.Producto,
            estadoString, entity.IdPedidosProductos
            
        );
    }
}