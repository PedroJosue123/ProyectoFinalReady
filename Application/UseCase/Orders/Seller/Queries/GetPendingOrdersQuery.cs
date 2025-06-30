using Application.Mappers;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.Orders.Seller.Queries;

public record GetPendingOrdersQuery(int SellerId) : IRequest<List<OrderGetRequestDomain>>;

public class GetPendingOrdersQueryHandler : IRequestHandler<GetPendingOrdersQuery, List<OrderGetRequestDomain>>
{
    private readonly IOrderSellerRepository<Pedido> _repository;

    public GetPendingOrdersQueryHandler(IOrderSellerRepository<Pedido> repository)
    {
        _repository = repository;
    }

    public async Task<List<OrderGetRequestDomain>> Handle(GetPendingOrdersQuery request, CancellationToken cancellationToken)
    {
        var pedidos = await _repository.GetPendingOrdersAsync(request.SellerId, cancellationToken);

        if (pedidos == null || !pedidos.Any())
            throw new Exception("No pending orders found.");

        return pedidos.Select(OrderRequestMapper.ToDomain).ToList();
    }
}
