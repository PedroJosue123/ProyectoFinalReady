namespace Domain.Interface;

public interface IOrderSellerRepository <Pedido>
{
    Task<List<Pedido>> GetSellerPreparedOrdersListAsync(int sellerId, CancellationToken cancellationToken); 
    Task<Pedido?> GetPreparationPreviewAsync(int idPedidosProducto, CancellationToken cancellationToken); 
    Task<List<Pedido>> GetPendingOrdersAsync(int sellerId, CancellationToken cancellationToken);
    Task<Pedido?> GetPedidoWithPagoAsync(int pedidoId, CancellationToken cancellationToken);
}