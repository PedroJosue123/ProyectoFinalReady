using Application.Mappers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCase.Orders.Buyer.Commands;

public sealed record RegisterOrderCommand(RegisterOrderRequestDto Request, int BuyerId) : IRequest<int>
{
    internal sealed class Handler(IUnitOfWork unitOfWork) : IRequestHandler<RegisterOrderCommand, int>
    {
        public async Task<int> Handle(RegisterOrderCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;
            var id = command.BuyerId;

            var usuariosEncontrados = await unitOfWork.Repository<User>()
                .GetAll()
                .CountAsync(u => u.UserId == id || u.UserId == request.Idproveedor);

            if (usuariosEncontrados < 2)
                throw new Exception("Uno o ambos usuarios no existen");

            var order = new OrderDomain(request.Idproveedor, id, 0, false);
            var orderef = OrderMapper.ToEntity(order);

            var detalles_producto = new ProductOrdenDomain(0, request.Producto, request.Cantidad,
                request.Descripcion, request.DireccionEntrega, request.FechaLlegadaAcordada,
                request.NombreTransaccion, 0);

            var detalleef = ProductMapper.ToEntity(detalles_producto);
            detalleef.FechaSolicitada = DateTime.Now;

            var pago = new PaymentsDomain(0, null, false, request.Monto);
            var pagoef = PaymentMapper.ToEntity(pago);

            try
            {
                await unitOfWork.BeginTransactionAsync();

                await unitOfWork.Repository<Pago>().AddAsync(pagoef);
                await unitOfWork.SaveChange();

                detalleef.IdPago = pagoef.IdPago;

                await unitOfWork.Repository<Pedidosproducto>().AddAsync(detalleef);
                await unitOfWork.SaveChange();

                orderef.IdPedidosProductos = detalleef.IdPedidosProductos;

                await unitOfWork.Repository<Pedido>().AddAsync(orderef);
                await unitOfWork.SaveChange();

                await unitOfWork.CommitTransactionAsync();
                return orderef.IdPedido;
            }
            catch
            {
                await unitOfWork.RollbackTransactionAsync();
                throw new Exception("Datos Incompletos");
            }
        }
    }
}
