namespace Application.Mappers;

ppublic class ProductMapper
{
    public static Pedidosproducto ToEntity(ProductOrdenDomain domain) => new Pedidosproducto
        
    { IdPedidosProductos = domain.IdPedidosProductos,  
        Producto = domain.Producto,
        Cantidad = domain.Cantidad,
        Descripcion = domain.Descripcion,
        DireccionEntrega = domain.DireccionEntrega,
        FechaLlegadaAcordada = domain.FechaLlegadaAcordada,
        NombreTransaccion = domain.NombreTransaccion,
        IdPago = domain.IdPago
    };

}