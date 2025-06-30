using Application.Mappers;
using Domain.Dtos;
using Domain.Entities;
using Domain.Interface;
using Infraestructure.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCase.Users.Commands;

public sealed record RegisterUserCommand(RegisterRequestDto Request) : IRequest<bool>
{
    internal sealed class Handler : IRequestHandler<RegisterUserCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthService _authService;
        private readonly IUserRepository<User> _userRepository;
        
        public Handler(IUnitOfWork unitOfWork, IUserRepository<User> userRepository, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _authService = authService;
            _userRepository= userRepository;
        }

        public async Task<bool> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var request = command.Request;

            if (!_authService.IsPasswordSecure(request.Password))
                throw new Exception("La contraseña debe tener al menos 8 caracteres, incluir mayúsculas, minúsculas, números y símbolos.");

            var userEf = await _userRepository
                .GetUserWithUserTypeByEmailAsync(request.Email, cancellationToken);

            if (userEf != null)
                throw new Exception("El correo ya existe cree otro");


            var user = new UserDomain(0, request.Email, _authService.HashPassword(request.Password), DateTime.UtcNow, request.UserTypeId);
            var userEntity = UserMapper.ToEntity(user);

            var profile = new UserProfileDomain(0, 0, request.Name, request.Ruc, request.ManagerName, request.ManagerDni, request.ManagerEmail,
                request.Phone, request.Address, _authService.HashPassword(request.PaymentPasswordHash));
            var profileEntity = UserProfileMapper.ToEntity(profile);

            try
            {
                await _unitOfWork.BeginTransactionAsync();

                await _unitOfWork.Repository<User>().AddAsync(userEntity);
                await _unitOfWork.SaveChange();

                profileEntity.UserId = userEntity.UserId;

                await _unitOfWork.Repository<Userprofile>().AddAsync(profileEntity);
                await _unitOfWork.SaveChange();

                await _unitOfWork.CommitTransactionAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackTransactionAsync();
                throw new Exception("Datos incompletos o falló el registro.");
            }
        }
    }
}