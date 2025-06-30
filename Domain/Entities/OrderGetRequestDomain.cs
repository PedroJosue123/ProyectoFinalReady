namespace Domain.Entities;

public class OrderGetRequestDomain
{

    public int IdPedido { get; set; }
    public string? Name { get; set; }
    public string Producto { get; set; } = null!;

    public int Cantidad { get; set; }

    public string Descripcion { get; set; } = null!;

    public string DireccionEntrega { get; set; } = null!;

    public DateTime FechaLlegadaAcordada { get; set; }
    public decimal? MontoPago { get; set; }
    
    public int IdProducto { get; set; }
    public OrderGetRequestDomain(
        int idPedido,
        string? name,
        string producto,
        int cantidad,
        string descripcion,
        string direccionEntrega,
        DateTime fechaLlegadaAcordada,
        decimal? montoPago,
        int idProducto)
        
    {
        IdPedido = idPedido;
        Name = name;
        Producto = producto;
        Cantidad = cantidad;
        Descripcion = descripcion;
        DireccionEntrega = direccionEntrega;
        FechaLlegadaAcordada = fechaLlegadaAcordada;
        MontoPago = montoPago;
        IdProducto = idProducto;

    }

}