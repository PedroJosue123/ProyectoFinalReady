using Application.Mappers;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.SenddOrder.Buyer.Queries;


public record GetShipmentStatusQuery(int PreparationId) : IRequest<GetSendOrderDomain>;

public class GetShipmentStatusQueryHandler : IRequestHandler<GetShipmentStatusQuery, GetSendOrderDomain>
{
    private readonly ISendOrderRepository<Preparacion> _Repository;

    public GetShipmentStatusQueryHandler(ISendOrderRepository<Preparacion> repository)
    {
        _Repository = repository;
    }

    public async Task<GetSendOrderDomain> Handle(GetShipmentStatusQuery request, CancellationToken cancellationToken)
    {
        var preparacion = await _Repository
            .GetPreparedOrderWithShipmentAsync(request.PreparationId, cancellationToken);

        if (preparacion == null)
            throw new Exception("Shipment not found");

        return GetSendOrderMapper.ToDomain(preparacion);
    }
}