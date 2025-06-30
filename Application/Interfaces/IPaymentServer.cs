namespace Application.Interfaces;

public interface IPaymentServer
{
    bool paymentt(string NroCard, string FechaNacimiento, string cvv);
}