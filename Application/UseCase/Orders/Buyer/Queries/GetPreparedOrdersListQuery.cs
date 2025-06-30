using Application.Mappers;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.Orders.Buyer.Queries;

public record GetPreparedOrdersListQuery(int BuyerId) : IRequest<List<GetListPreparationOrderDomain>>;

public class GetPreparedOrdersListQueryHandler : IRequestHandler<GetPreparedOrdersListQuery, List<GetListPreparationOrderDomain>>
{
    private readonly IOrderBuyerRepository<Pedido> _repository;
    public GetPreparedOrdersListQueryHandler(IOrderBuyerRepository<Pedido> repository)
    {
        _repository = repository;
    }

    public async Task<List<GetListPreparationOrderDomain>> Handle(GetPreparedOrdersListQuery request, CancellationToken cancellationToken)
    {
        var pedidos = await _repository.GetPreparedOrdersListAsync(request.BuyerId, cancellationToken);
        return pedidos.Select(p => GetListPreparationOrderMapper.ToDomain(p)).ToList();
    }
}