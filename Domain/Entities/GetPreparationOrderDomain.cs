namespace Domain.Entities;

public class GetPreparationOrderDomain
{
    public int IdPreparacion { get; set; }
    public string? EstadoString { get; set; }
    
    public string Producto { get; set; } = null!;

    public int Cantidad { get; set; }
    public string ComoEnvia { get; private set; } = null!;
    public string Detalles { get; private set; } = null!;
    
    public GetPreparationOrderDomain(int idPreparacion, string? estadoString, string producto, int cantidad, string comoEnvia, string detalles)
    {
        IdPreparacion = idPreparacion;
        EstadoString = estadoString;
        Producto = producto;
        Cantidad = cantidad;
        ComoEnvia = comoEnvia;
        Detalles = detalles;
    }
}