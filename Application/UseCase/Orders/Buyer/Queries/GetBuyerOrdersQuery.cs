using Application.Mappers;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.Orders.Buyer.Queries;

public record GetBuyerOrdersQuery(int BuyerId) : IRequest<List<GetOrderDomain>>;

public class GetBuyerOrdersQueryHandler : IRequestHandler<GetBuyerOrdersQuery, List<GetOrderDomain>>
{
    private readonly IOrderBuyerRepository<Pedido> _repository;

    public GetBuyerOrdersQueryHandler(IOrderBuyerRepository<Pedido> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetOrderDomain>> Handle(GetBuyerOrdersQuery request, CancellationToken cancellationToken)
    {
        var pedidos = await _repository.GetBuyerOrdersAsync(request.BuyerId, cancellationToken);
        return pedidos.Select(p => GetOrderMapper.ToDomain(p)).ToList();
    }
}