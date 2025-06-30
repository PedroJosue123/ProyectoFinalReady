using Application.Mappers;
using Domain.Dtos;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCase.Users.Commands;

public sealed record LoginUserCommand(LoginRequestDto Request) : IRequest<string?>
{
    internal sealed class Handler : IRequestHandler<LoginUserCommand, string?>
    {
        private readonly IUserRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;

        public Handler(IUserRepository<User> userRepository, IUnitOfWork unitOfWork, IAuthService authService)
        {
            _userRepository= userRepository;
            _unitOfWork = unitOfWork;
            _authService = authService;
        }

        public async Task<string?> Handle(LoginUserCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            var userEf = await _userRepository
                .GetUserWithUserTypeByEmailAsync(request.Email, cancellationToken);

            if (userEf is null)
                throw new Exception("Usuario no encontrado");

            var userDomain = UserMapper.ToDomain(userEf);

            if (userDomain.LockoutUntil.HasValue && userDomain.LockoutUntil > DateTime.UtcNow)
                throw new Exception($"Cuenta bloqueada hasta {userDomain.LockoutUntil.Value.ToLocalTime()}");

            if (!_authService.VerifyPassword(request.Password, userDomain.PasswordHash))
            {
                userDomain.IncrementFailedLogin();

                var userTracked = await _unitOfWork.Repository<User>().GetByIdAsync(userDomain.Id);
                userTracked.FailedLoginAttempts = userDomain.FailedLoginAttempts;

                if (userDomain.FailedLoginAttempts >= 5)
                {
                    userDomain.LockAccount(TimeSpan.FromMinutes(10));
                    userTracked.LockoutUntil = userDomain.LockoutUntil;
                    userTracked.FailedLoginAttempts = 0;
                }

                await _unitOfWork.SaveChange();
                throw new Exception("Contraseña incorrecta");
            }

            userDomain.ResetLoginAttempts();
            var userToUpdate = await _unitOfWork.Repository<User>().GetByIdAsync(userDomain.Id);
            userToUpdate.FailedLoginAttempts = 0;
            userToUpdate.LockoutUntil = null;

            await _unitOfWork.SaveChange();

            return _authService.GenerateToken(
                userDomain.Id,
                userDomain.Email,
                userEf.UserType.Name
            );
        }
    }
}