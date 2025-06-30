using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class PreparationOrderMapper
{
    public static Preparacion ToEntity(PreparationOrderDomain domain) => new Preparacion
    {   
        IdPreparacion = domain.Id,
        ComoEnvia = domain.ComoEnvia,
        Detalles = domain.Detalles,
        Estado = true
    };
}