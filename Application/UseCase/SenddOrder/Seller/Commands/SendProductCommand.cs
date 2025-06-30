using Application.Mappers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.SenddOrder.Seller.Commands;

public record SendProductCommand(int PreparationId, SendProductDto Dto) : IRequest<int>;

public class SendProductCommandHandler : IRequestHandler<SendProductCommand, int>
{
    private readonly IUnitOfWork _unitOfWork;

    public SendProductCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<int> Handle(SendProductCommand request, CancellationToken cancellationToken)
    {
        var preparacion = await _unitOfWork.Repository<Preparacion>().GetByIdAsync(request.PreparationId);
        if (preparacion == null)
            throw new Exception("Preparation not found");

        var envioDomain = new SendProductDomain(0, request.Dto.NombreEmpresa, request.Dto.RucEmpresa,
            request.Dto.Asesor, request.Dto.NumeroTelefonico, request.Dto.DireccionEnvio,
            request.Dto.DireccionRecojo, request.Dto.FechaLlegada, request.Dto.NroGuia);

        var envioEntity = SendProductMapper.ToEntity(envioDomain);

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            await _unitOfWork.Repository<Envio>().AddAsync(envioEntity);
            await _unitOfWork.SaveChange();

            preparacion.IdEnvio = envioEntity.IdEnvio;
            await _unitOfWork.SaveChange();

            await _unitOfWork.CommitTransactionAsync();

            return envioEntity.IdEnvio;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new Exception("Transaction failed during product shipment.");
        }
    }
}