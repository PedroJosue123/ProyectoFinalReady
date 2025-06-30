using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class GetListPreparationOrderMapper
{
    public static GetListPreparationOrderDomain ToDomain(Pedido entity)
    {

        // Extraer los estados 

        bool estadoEnviado = (bool)entity.IdPedidosProductosNavigation.IdPreparacionNavigation.IdEnvioNavigation.Estado;
        bool estadoEntregado =
            (bool)entity.IdPedidosProductosNavigation.IdPreparacionNavigation.IdEnvioNavigation.Llegada;

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

        return new GetListPreparationOrderDomain(entity.IdPedido,estadoString, entity.IdPedidosProductosNavigation.NombreTransaccion);

    }

}