namespace Application.UseCase;

public class LoginUser : ILoginUser
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAuthService _authService;

    public LoginUser(IUnitOfWork unitOfWork, IAuthService authService)
    {
        _unitOfWork = unitOfWork;
        _authService = authService;
    }

    public async Task<string?> Execute(LoginRequestDto request)
    {   
        var userRepo = _unitOfWork.Repository<User>();
        var userEf = await userRepo.GetAll().Include(u => u.UserType).FirstOrDefaultAsync(u => u.Email == request.Email);

        if (userEf == null) throw new Exception("Usuario no encontrado");

        var user = UserMapper.ToDomain(userEf);

        if (user.LockoutUntil.HasValue && user.LockoutUntil > DateTime.UtcNow)
            throw new Exception($"Cuenta bloqueada hasta {user.LockoutUntil.Value.ToLocalTime()}");

        if (!_authService.VerifyPassword(request.Password, user.PasswordHash))
        {
            user.IncrementFailedLogin();
            userEf.FailedLoginAttempts = user.FailedLoginAttempts;

            if (user.FailedLoginAttempts >= 5)
            {
                user.LockAccount(TimeSpan.FromMinutes(10));
                userEf.LockoutUntil = user.LockoutUntil;
                userEf.FailedLoginAttempts = 0;
            }

            await _unitOfWork.SaveChange();
            throw new Exception("Contraseña incorrecta");
        }

        user.ResetLoginAttempts();
        userEf.FailedLoginAttempts = 0;
        userEf.LockoutUntil = null;
        await _unitOfWork.SaveChange();

        return _authService.GenerateToken(user.Id, user.Email, userEf.UserType.Name);
    }
}