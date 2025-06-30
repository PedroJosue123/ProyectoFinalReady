namespace Domain.Dtos;

public class CreateOrderDto
{
    public string Product { get; set; }
    public int Quantity { get; set; }
    public string Supplier { get; set; }
    public string Status { get; set; } // Enviado por el comprador
}