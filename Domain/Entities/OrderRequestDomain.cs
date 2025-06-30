namespace Domain.Entities;

public class OrderRequestDomain
{
    public string? Name { get; set; }

    public string Producto { get; set; } = null!;

    public int Cantidad { get; set; }

    public string Descripcion { get; set; } = null!;

    public string DireccionEntrega { get; set; } = null!;

    public DateTime FechaLlegadaAcordada { get; set; }

    public decimal? Monto { get; set; }

    // ✅ Constructor
    public OrderRequestDomain(string? name, string producto, int cantidad, string descripcion,
        string direccionEntrega, DateTime fechaLlegadaAcordada, decimal? monto)
    {
        Name = name;
        Producto = producto;
        Cantidad = cantidad;
        Descripcion = descripcion;
        DireccionEntrega = direccionEntrega;
        FechaLlegadaAcordada = fechaLlegadaAcordada;
        Monto = monto;
    }

   
}