using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class PaymentMapper
{
    public  static Pago ToEntity(PaymentsDomain domain) => new Pago
    {
        IdPago = domain.IdPago,
        Estado = domain.Estado,
        Monto = domain.Monto,
        FechaPago = domain.FechaPago,
      
        
        
    };
}