namespace Domain.Entities;

public class OrderDomain
{
    public int IdProveedor { get; private set; }
    public int IdComprador { get; private set; }
    public int IdPedidosProductos { get; private set; }
    public bool? Estado { get; set; }

    public OrderDomain(int idProveedor, int idComprador, int idPedidosProductos, bool? estado)
    {
        var errores = new List<string>();

        if (idProveedor == null || idProveedor <= 0)
            errores.Add("IdProveedor");
        if (idComprador == null || idComprador <= 0)
            errores.Add("IdComprador");
        if (estado == null)
            errores.Add("Estado");

        if (errores.Any())
            throw new ArgumentException("Los siguientes campos son obligatorios o inválidos: " + string.Join(", ", errores));

        IdProveedor = idProveedor;
        IdComprador = idComprador;
        IdPedidosProductos = idPedidosProductos;
        Estado = estado;
    }
    
    public void ChangeEstado() => Estado = true;
}