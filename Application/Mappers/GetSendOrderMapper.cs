using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class GetSendOrderMapper
{
    public static GetSendOrderDomain ToDomain(Preparacion entity) => new GetSendOrderDomain(
        entity.IdEnvioNavigation.NombreEmpresa,
        entity.IdEnvioNavigation.RucEmpresa, 
        entity.IdEnvioNavigation.Asesor,
        entity.IdEnvioNavigation.NumeroTelefonico, entity.IdEnvioNavigation.DireccionEnvio,
        entity.IdEnvioNavigation.DireccionRecojo,
        entity.IdEnvioNavigation.FechaLlegada, entity.IdEnvioNavigation.NroGuia
        
    );

}