using Application.Mappers;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.Orders.Buyer.Queries;

public record GetPreparedOrderDetailQuery(int BuyerId, int OrderId) : IRequest<GetPreparationOrderDomain>;

public class GetPreparedOrderDetailQueryHandler : IRequestHandler<GetPreparedOrderDetailQuery, GetPreparationOrderDomain>
{
    private readonly IOrderBuyerRepository<Pedido> _repository;
    public GetPreparedOrderDetailQueryHandler(IOrderBuyerRepository<Pedido>  repository)
    {
        _repository = repository;
    }

    public async Task<GetPreparationOrderDomain> Handle(GetPreparedOrderDetailQuery request, CancellationToken cancellationToken)
    {
        var pedido = await _repository.GetPreparedOrderDetailAsync(request.BuyerId, request.OrderId, cancellationToken);
        if (pedido == null)
            throw new Exception("No se encontró el pedido preparado");

        return GetPreparationOrderMapper.ToDomain(pedido);
    }
}