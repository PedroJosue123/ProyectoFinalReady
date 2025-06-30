using Domain.Entities;

namespace Application.IUseCase;

public interface IOrderRequests
{

    Task<List<OrderGetRequestDomain>> GetSolicitud(int id);
    
    Task<int> AceptarSolicitud(int id);

    Task<int> VerSiPago(int id);
}