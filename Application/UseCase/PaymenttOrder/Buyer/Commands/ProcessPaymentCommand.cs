using Domain.Dtos;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.PaymenttOrder.Buyer.Commands;


public record ProcessPaymentCommand(int OrderId, PaymentCartDto Cart) : IRequest<bool>;

public class ProcessPaymentHandler(
    IPaymentOrderRepository<Userprofile, Pedido> repository,
    IUnitOfWork unitOfWork,
    IPaymentServer paymentServer
) : IRequestHandler<ProcessPaymentCommand, bool>
{
    public async Task<bool> Handle(ProcessPaymentCommand request, CancellationToken cancellationToken)
    {
        var pedido = await repository.GetPedidoWithPaymentAsync(request.OrderId, cancellationToken);
        if (pedido == null)
            throw new Exception("Order not found or not accepted");

        var isValid = paymentServer.paymentt(request.Cart.NroTarjeta, request.Cart.FechaNacimiento, request.Cart.cvv);
        if (!isValid)
            throw new Exception("Payment validation failed");

        pedido.IdPedidosProductosNavigation.IdPagoNavigation.Estado = true;
        pedido.IdPedidosProductosNavigation.IdPagoNavigation.FechaPago = DateTime.Now;

        await unitOfWork.SaveChange();
        return true;
    }

}