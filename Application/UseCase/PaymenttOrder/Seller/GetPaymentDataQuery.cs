using Application.Mappers;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.PaymenttOrder.Seller;

public record GetPaymentDataQuery(int OrderId) : IRequest<PaymentGetRequestDomain>;

// HANDLER
public class GetPaymentDataQueryHandler : IRequestHandler<GetPaymentDataQuery, PaymentGetRequestDomain>
{
    private readonly IPaymentOrderRepository<Userprofile, Pedido> _repository;

    public GetPaymentDataQueryHandler(IPaymentOrderRepository<Userprofile, Pedido> repository)
    {
        _repository = repository;
    }

    public async Task<PaymentGetRequestDomain> Handle(GetPaymentDataQuery request, CancellationToken cancellationToken)
    {
        var pedido = await _repository.GetPedidoDataForPaymentAsync(request.OrderId, cancellationToken);

        if (pedido == null)
            throw new Exception("Payment data not found.");

        return PaymentGetOrderMapper.ToDomain(pedido);
    }
}