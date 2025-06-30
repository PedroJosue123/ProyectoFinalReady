namespace Domain.Entities;

public class GetListPrepationDomain
{
    public string? NameComprador { get; }

    public int Cantidad { get; set; }
    
    public string Producto { get; set; } = null!;
    public string? EstadoString { get; set; }
    public int IdPedidosProductos { get; private set; }

  
    
    
    
    public GetListPrepationDomain(
        string? namecomprador,
        int cantidad,
        string producto,
        string? estadoString,
        int idPedidosProductos
    )
    {
       
        if (string.IsNullOrWhiteSpace(producto)) throw new ArgumentException("Producto es obligatorio.");
        if (cantidad <= 0) throw new ArgumentException("Cantidad debe ser mayor a cero.");
       
        NameComprador = namecomprador;
        Cantidad = cantidad;
        Producto = producto;
        EstadoString = estadoString;
        IdPedidosProductos = idPedidosProductos;

    }
}