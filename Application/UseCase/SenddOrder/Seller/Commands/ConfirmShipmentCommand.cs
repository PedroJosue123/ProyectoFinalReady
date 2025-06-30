using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.SenddOrder.Seller.Commands;


public record ConfirmShipmentCommand(int EnvioId) : IRequest<bool>;

public class ConfirmShipmentCommandHandler : IRequestHandler<ConfirmShipmentCommand, bool>
{
    private readonly IUnitOfWork _unitOfWork;

    public ConfirmShipmentCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> Handle(ConfirmShipmentCommand request, CancellationToken cancellationToken)
    {
        var envio = await _unitOfWork.Repository<Envio>().GetByIdAsync(request.EnvioId);
        if (envio == null)
            throw new Exception("Shipment not found");

        envio.Llegada = true;

        await _unitOfWork.SaveChange();
        return true;
    }
}