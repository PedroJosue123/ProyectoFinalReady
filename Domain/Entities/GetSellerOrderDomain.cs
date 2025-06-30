namespace Domain.Entities;

public class GetSellerOrderDomain
{
   
    public DateTime? FechaSolicitada { get; set; }
    public DateTime? FechaPago { get; set; }
    public DateTime FechaLlegadaAcordada { get; set; }
    public string? NameComprador { get; }


    public string Producto { get; set; } = null!;

    public int Cantidad { get; set; }

    public string Descripcion { get; set; } = null!;
    

    public string DireccionEntrega { get; set; } = null!;
    
    public decimal? Monto { get; set; }
    
    public int IdPedidosProductos { get; private set; }

    
    
    
    public GetSellerOrderDomain(
       
        DateTime? fechaSolicitada,
        DateTime? fechaPago,
        string? namecomprador,
        DateTime fechaLlegadaAcordada,
        string producto,
        int cantidad,
        string descripcion,
        string direccionEntrega,
        decimal? monto,
        int idPedidosProductos
    )
    {
        
        FechaSolicitada = fechaSolicitada;
        FechaLlegadaAcordada = fechaLlegadaAcordada;
        NameComprador = namecomprador;
        FechaPago = fechaPago;
        Producto = producto;
        Cantidad = cantidad;
        Descripcion = descripcion;
        DireccionEntrega = direccionEntrega;
        Monto = monto;
        IdPedidosProductos = idPedidosProductos;

    }
}