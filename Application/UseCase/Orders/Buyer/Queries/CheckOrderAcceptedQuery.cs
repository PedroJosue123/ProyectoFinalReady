using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.Orders.Buyer.Queries;

public sealed record CheckOrderAcceptedQuery(int OrderId) : IRequest<bool>
{
    internal sealed class Handler : IRequestHandler<CheckOrderAcceptedQuery, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Handler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(CheckOrderAcceptedQuery request, CancellationToken cancellationToken)
        {
            var pedido = await _unitOfWork.Repository<Pedido>().GetByIdAsync(request.OrderId);

            if (pedido == null || !pedido.Estado.GetValueOrDefault())
                throw new Exception("El pedido no ha sido aceptado.");

            return true;
        }
    }
}