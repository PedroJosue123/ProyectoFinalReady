using Domain.Entities;
using Infraestructure.Models;

namespace Application.Mappers;

public class UserMapper
{
    public static UserDomain ToDomain(User entity) => new UserDomain(
        entity.UserId, entity.Email, entity.Password, entity.CreatedAt,
        entity.UserTypeId, entity.FailedLoginAttempts ?? 0, entity.LockoutUntil);

    public static User ToEntity(UserDomain domain) => new User
    {
        UserId = domain.Id,
        Email = domain.Email,
        Password = domain.PasswordHash,
        CreatedAt = domain.CreatedAt,
        UserTypeId = domain.UserTypeId,
        FailedLoginAttempts = domain.FailedLoginAttempts,
        LockoutUntil = domain.LockoutUntil
    };
}

