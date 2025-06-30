using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.Orders.Seller.Commands;

public record AcceptOrderRequestCommand(int OrderId) : IRequest<int>;

public class AcceptOrderRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<AcceptOrderRequestCommand, int>
{
    public async Task<int> Handle(AcceptOrderRequestCommand request, CancellationToken cancellationToken)
    {
        var pedido = await unitOfWork.Repository<Pedido>().GetByIdAsync(request.OrderId);

        if (pedido == null)
            throw new Exception("Order not found");

        pedido.Estado = true;

        await unitOfWork.SaveChange();
        return pedido.IdPedido;
    }

}