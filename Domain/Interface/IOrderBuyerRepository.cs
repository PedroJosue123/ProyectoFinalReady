namespace Domain.Interface;

public interface IOrderBuyerRepository <Pedido>
{
    Task<List<Pedido>> GetBuyerOrdersAsync(int buyerId, CancellationToken cancellationToken);
    Task<Pedido?> GetPreparedOrderDetailAsync(int buyerId, int orderId, CancellationToken cancellationToken);
    Task<List<Pedido>> GetPreparedOrdersListAsync(int buyerId, CancellationToken cancellationToken);
}