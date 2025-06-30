namespace Domain.Entities;

public class PaymentGetRequestDomain
{
    public int Cantidad { get; set; }

    public string Producto { get; private set; } = null!;

    public decimal? Monto { get; set; }

    public PaymentGetRequestDomain(int cantidad, string producto, decimal? monto)
    {
        Cantidad = cantidad;
        Producto = producto;
        Monto = monto;
    }

}