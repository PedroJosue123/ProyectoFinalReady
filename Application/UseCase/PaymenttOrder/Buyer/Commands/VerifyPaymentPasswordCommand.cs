using Domain.Interface;
using Infraestructure.Models;
using MediatR;

namespace Application.UseCase.PaymenttOrder.Buyer.Commands;

public record VerifyPaymentPasswordCommand(int UserId, string Password) : IRequest<bool>;

public class VerifyPaymentPasswordHandler(
    IPaymentOrderRepository<Userprofile, Pedido> repository,
    IAuthService authService
) : IRequestHandler<VerifyPaymentPasswordCommand, bool>
{
    public async Task<bool> Handle(VerifyPaymentPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetUserProfileWithPasswordAsync(request.UserId, cancellationToken);
        if (user == null || !authService.VerifyPassword(request.Password, user.PaymentPassword))
            throw new Exception("Incorrect password");

        return true;
    }
}