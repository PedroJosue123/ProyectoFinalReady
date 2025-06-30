using Application.IUseCase;
using Infraestructure.Models;

namespace Application.UseCase;

public class RegisterUser : IRegisterUser
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public RegisterUser(IUnitOfWork unitOfWork, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task<bool> Execute(RegisterRequestDto request)
    {
        if (!_authService.IsPasswordSecure(request.Password))
            throw new Exception("La contraseña debe tener al menos 8 caracteres, incluir mayúsculas, minúsculas, números y símbolos.");

        var exists = await _unitOfWork.Repository<User>()
            .GetAll()
            .AnyAsync(u => u.Email == request.Email);
        
        if (exists) throw new Exception("Email ya registrado");

        var user = new UserDomain(0, request.Email, _authService.HashPassword(request.Password), DateTime.UtcNow, request.UserTypeId);
        var userEntity = UserMapper.ToEntity(user);

        var profile = new UserProfileDomain(0, 0, request.Name, request.Ruc, request.ManagerName, request.ManagerDni, request.ManagerEmail
            , request.Phone, request.Address, _authService.HashPassword(request.PaymentPasswordHash));
        var profileEntity = UserProfileMapper.ToEntity(profile);

        try
        {
            await _unitOfWork.BeginTransactionAsync();

            // Guardamos primero el usuario
            await _unitOfWork.Repository<User>().AddAsync(userEntity);
            await _unitOfWork.SaveChange();

            // Asociamos el ID generado al perfil
            profileEntity.UserId = userEntity.UserId;

            // Guardamos el perfil
            await _unitOfWork.Repository<Userprofile>().AddAsync(profileEntity);
            await _unitOfWork.SaveChange();

            await _unitOfWork.CommitTransactionAsync();
            return true;
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw new Exception("Datos Incompletos");

        }
    }
}