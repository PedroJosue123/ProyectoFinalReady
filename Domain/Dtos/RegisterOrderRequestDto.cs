namespace Domain.Dtos;

public class RegisterOrderRequestDto
{
    public string Producto { get; set; } = null!;

    public int Cantidad { get; set; }

    public string Descripcion { get; set; } = null!;
    
    public int Idproveedor { get; set; }

    public string DireccionEntrega { get; set; } = null!;

    public DateTime FechaLlegadaAcordada { get; set; }
    
    public decimal Monto { get; set; }
    public string NombreTransaccion { get; set; } = null!;
}