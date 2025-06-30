using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class SendProductMapper
{
    public static Envio ToEntity(SendProductDomain domain) => new Envio
    {
        IdEnvio = domain.IdSend,
        Asesor = domain.Asesor,
        DireccionEnvio = domain.DireccionEnvio,
        NumeroTelefonico = domain.NumeroTelefonico,
        RucEmpresa = domain.RucEmpresa,
        DireccionRecojo = domain.DireccionRecojo,
        FechaLlegada = domain.FechaLlegada,
        NombreEmpresa = domain.NombreEmpresa,
        NroGuia = domain.NroGuia,
        Estado = true
    };
    
 

}