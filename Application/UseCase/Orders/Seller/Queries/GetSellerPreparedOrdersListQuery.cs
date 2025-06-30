using Application.Mappers;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.Orders.Seller.Queries;

public record GetSellerPreparedOrdersListQuery(int SellerId) : IRequest<List<GetListPrepationDomain>>;

public class GetSellerPreparedOrdersListHandler(
    IOrderSellerRepository<Pedido> repository)
    : IRequestHandler<GetSellerPreparedOrdersListQuery, List<GetListPrepationDomain>>
{
    public async Task<List<GetListPrepationDomain>> Handle(GetSellerPreparedOrdersListQuery request, CancellationToken cancellationToken)
    {
        var pedidos = await repository.GetSellerPreparedOrdersListAsync(request.SellerId, cancellationToken);

        if (!pedidos.Any())
            throw new Exception("No prepared orders found for the seller.");

        return pedidos.Select(GetListPreparationMapper.ToDomain).ToList();
    }
}