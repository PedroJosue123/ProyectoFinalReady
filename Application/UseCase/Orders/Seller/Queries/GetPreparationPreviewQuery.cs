using Application.Mappers;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.Orders.Seller.Queries;


public record GetPreparationPreviewQuery(int ProductOrderId) : IRequest<GetSellerOrderDomain>;

public class GetPreparationPreviewHandler(
    IOrderSellerRepository<Pedido> repository)
    : IRequestHandler<GetPreparationPreviewQuery, GetSellerOrderDomain>
{
    public async Task<GetSellerOrderDomain> Handle(GetPreparationPreviewQuery request, CancellationToken cancellationToken)
    {
        var pedido = await repository.GetPreparationPreviewAsync(request.ProductOrderId, cancellationToken);

        if (pedido == null)
            throw new Exception("Preparation preview not found.");

        return GetSellerOrderMapper.ToDomain(pedido);
    }
}