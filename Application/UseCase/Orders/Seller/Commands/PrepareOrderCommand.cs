using Application.Mappers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.Orders.Seller.Commands;

public sealed record PrepareOrderCommand(int ProductOrderId, PreparationOrderDto PreparationDto) : IRequest<int>
{
    internal sealed class Handler(IUnitOfWork unitOfWork) : IRequestHandler<PrepareOrderCommand, int>
    {
        public async Task<int> Handle(PrepareOrderCommand command, CancellationToken cancellationToken)
        {
            var preparacion = new PreparationOrderDomain(0, command.PreparationDto.ComoEnvia, command.PreparationDto.Detalles);
            var preparacionEntity = PreparationOrderMapper.ToEntity(preparacion);

            await unitOfWork.Repository<Preparacion>().AddAsync(preparacionEntity);
            await unitOfWork.SaveChange();

            var producto = await unitOfWork.Repository<Pedidosproducto>().GetByIdAsync(command.ProductOrderId);
            producto.IdPreparacion = preparacionEntity.IdPreparacion;

            await unitOfWork.SaveChange();

            return preparacionEntity.IdPreparacion;
        }
    }
}