namespace Domain.Dtos;

public class PedidoDto
{
    public int IdPedido { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdComprador { get; set; }

    public int? IdPedidosProductos { get; set; }

    public bool? Estado { get; set; }
    
}