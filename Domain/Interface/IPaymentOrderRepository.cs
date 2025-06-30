namespace Domain.Interface;

public interface IPaymentOrderRepository <Userprofile, Pedido>
{
    Task<Userprofile?> GetUserProfileWithPasswordAsync(int userId, CancellationToken cancellationToken);
    Task<Pedido?> GetPedidoWithPaymentAsync(int orderId, CancellationToken cancellationToken);
}