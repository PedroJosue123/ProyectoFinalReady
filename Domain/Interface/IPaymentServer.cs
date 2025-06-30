namespace Domain.Interface;

public interface IPaymentServer
{
    bool paymentt(string NroCard, string FechaNacimiento, string cvv);
}