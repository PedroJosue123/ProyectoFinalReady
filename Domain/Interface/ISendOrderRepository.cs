namespace Domain.Interface;

public interface ISendOrderRepository<Preparacion>
{
    Task<Preparacion?> GetPreparedOrderWithShipmentAsync(int preparationId, CancellationToken cancellationToken);
}