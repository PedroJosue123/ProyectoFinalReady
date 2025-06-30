using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public static class UserProfileMapper
{
    public static Userprofile ToEntity(UserProfileDomain domain) => new Userprofile
    {
        UserId = domain.UserId,
        Name = domain.Name,
        Ruc = domain.Ruc,
        ManagerName = domain.ManagerName,
        ManagerDni = domain.ManagerDni,
        ManagerEmail = domain.ManagerEmail,
        Phone = domain.Phone,
        Address = domain.Address,
        PaymentPassword =  domain.PaymentPasswordHash
    };
}