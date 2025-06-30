namespace Domain.Entities;

public class GetListPreparationOrderDomain
{
    
    public int IdPedido { get; set; }
    public string? EstadoString { get; set; }
    
    public string Transaccion { get; set; } = null!;

    public GetListPreparationOrderDomain(int idPedido, string? estadoString , string transaccion)
    {
        IdPedido = idPedido;
        EstadoString = estadoString;
        Transaccion = transaccion;
    }
}