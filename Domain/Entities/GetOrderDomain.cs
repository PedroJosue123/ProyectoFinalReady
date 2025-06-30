namespace Domain.Entities;

public class GetOrderDomain
{
    public string? EstadoString { get; set; }
    public DateTime? FechaSolicitada { get; set; }
    public DateTime? FechaPago { get; set; }
    public string Producto { get; set; } = null!;

    public int Cantidad { get; set; }

    public string Descripcion { get; set; } = null!;
    
    public string? NameProveedor { get; }

    public string DireccionEntrega { get; set; } = null!;
    
    public decimal? Monto { get; set; }

    public DateTime FechaLlegadaAcordada { get; set; }

    
    public string NombreTransaccion { get; set; } = null!;
    
    public GetOrderDomain(
        string? estadoString,
        DateTime? fechaSolicitada,
        DateTime? fechaPago,
        string producto,
        int cantidad,
        string descripcion,
        string? nameProveedor,
        string direccionEntrega,
        decimal? monto,
        DateTime fechaLlegadaAcordada,
        string nombreTransaccion)
    {
       
        if (string.IsNullOrWhiteSpace(producto)) throw new ArgumentException("Producto es obligatorio.");
        if (string.IsNullOrWhiteSpace(descripcion)) throw new ArgumentException("Descripcion es obligatoria.");
        if (string.IsNullOrWhiteSpace(direccionEntrega)) throw new ArgumentException("DireccionEntrega es obligatoria.");
        if (string.IsNullOrWhiteSpace(nombreTransaccion)) throw new ArgumentException("NombreTransaccion es obligatorio.");
        if (cantidad <= 0) throw new ArgumentException("Cantidad debe ser mayor a cero.");
        if (monto == null || monto <= 0) throw new ArgumentException("Monto debe ser mayor a cero.");
        if (fechaLlegadaAcordada == default) throw new ArgumentException("FechaLlegadaAcordada es obligatoria.");

        EstadoString = estadoString;
        FechaSolicitada = fechaSolicitada;
        FechaPago = fechaPago;
        Producto = producto;
        Cantidad = cantidad;
        Descripcion = descripcion;
        NameProveedor = nameProveedor;
        DireccionEntrega = direccionEntrega;
        Monto = monto;
        FechaLlegadaAcordada = fechaLlegadaAcordada;
        NombreTransaccion = nombreTransaccion;
    }

    
}