using Domain.Dtos;
using Domain.Entities;

namespace Application.IUseCase;

public interface IPaymentOrder
{
    Task<bool> VerificationPaymentPassword(int iduser, string password);
    Task<PaymentGetRequestDomain> GeyDataPayment(int id);
    Task<bool> Payment(int id, PaymentCartDto card);
}