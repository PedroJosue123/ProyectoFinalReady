namespace Domain.Entities;

public class ProductOrdenDomain
{
    public int IdPedidosProductos { get; private set; }
    public string Producto { get; private set; } = null!;
    public int Cantidad { get; private set; }
    public string Descripcion { get; private set; } = null!;

    public string DireccionEntrega { get; private set; } = null!;
    public DateTime FechaLlegadaAcordada { get; private set; }
    public string NombreTransaccion { get; private set; } = null!;
    
    public int IdPago { get; set; }

    public ProductOrdenDomain(
        int idPedidosProductos,
        string producto,
        int cantidad,
        string descripcion,
        string direccionEntrega,
        DateTime fechaLlegadaAcordada,
        string nombreTransaccion,
        int idPago)
    {
        var errores = new List<string>();


        if (string.IsNullOrWhiteSpace(producto))
            errores.Add("Producto");
        if (cantidad <= 0)
            errores.Add("Cantidad");
        if (string.IsNullOrWhiteSpace(descripcion))
            errores.Add("Descripción");
        if (string.IsNullOrWhiteSpace(direccionEntrega))
            errores.Add("Dirección de entrega");
        if (fechaLlegadaAcordada == default)
            errores.Add("Fecha de llegada acordada");
        if (string.IsNullOrWhiteSpace(nombreTransaccion))
            errores.Add("Nombre de transacción");
        if (errores.Any())
            throw new Exception("Los siguientes campos son obligatorios o inválidos: " + string.Join(", ", errores));

        IdPedidosProductos = idPedidosProductos;
        Producto = producto;
        Cantidad = cantidad;
        Descripcion = descripcion;
        DireccionEntrega = direccionEntrega;
        FechaLlegadaAcordada = fechaLlegadaAcordada;
        NombreTransaccion = nombreTransaccion;
        IdPago = idPago;
    }
}