using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;

public class AuthService : IAuthService
{
    // private readonly IUnitOfWork _unitOfWork;
    // private readonly TransactivaDbContext _context;
    // private readonly IConfiguration _config;
    //
    // public AuthService(IUnitOfWork unitOfWork, TransactivaDbContext context, IConfiguration config)
    // {
    //     _unitOfWork = unitOfWork;
    //     _context = context;
    //     _config = config;
    // }

    /*public async Task<string?> LoginAsync(LoginRequestDto request)
    {
        var user = await _context.users
            .Include(u => u.UserType)
            .FirstOrDefaultAsync(u => u.Email == request.Email);

        if (user == null)
            return null;

        if (user.LockoutUntil != null && user.LockoutUntil > DateTime.UtcNow)
            throw new Exception($"Tu cuenta está bloqueada hasta las {user.LockoutUntil.Value.ToLocalTime():HH:mm:ss}.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
        {
            user.FailedLoginAttempts++;

            if (user.FailedLoginAttempts >= 5)
            {
                user.LockoutUntil = DateTime.UtcNow.AddMinutes(10); // bloquea 10 min
                user.FailedLoginAttempts = 0; // reset
            }

            await _context.SaveChangesAsync();
            return null;
        }

        user.FailedLoginAttempts = 0;
        user.LockoutUntil = null;
        await _context.SaveChangesAsync();

        // Generar token
        var key = Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]);
        var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            /*new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString())#1#
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim(ClaimTypes.Role, user.UserType.Name)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<bool> RegisterAsync(RegisterRequestDto request)
    {
        // Validar fuerza de la contraseña
        if (!EsPasswordSegura(request.Password))
            throw new Exception("La contraseña debe tener al menos 8 caracteres, una mayúscula, una minúscula, un número y un carácter especial.");

        var existing = await _context.users.AnyAsync(u => u.Email == request.Email);
        if (existing) return false;

        var user = new user
        {
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            UserTypeId = request.UserTypeId,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Repository<user>().AddAsync(user);
        
        await _unitOfWork.SaveChange(); 
    
        var profile = new userprofile
        {
            UserId = user.UserId,
            Name = request.Name,
            Ruc = request.Ruc,
            ManagerName = request.ManagerName,
            ManagerDni = request.ManagerDni,
            ManagerEmail = request.ManagerEmail,
            Phone = request.Phone,
            Address = request.Address
        };

        await _unitOfWork.Repository<userprofile>().AddAsync(profile);
        await _unitOfWork.SaveChange();

        return true;
    }

    private bool EsPasswordSegura(string password)
    {
        if (password.Length < 8) return false;

        bool tieneMayus = password.Any(char.IsUpper);
        bool tieneMinus = password.Any(char.IsLower);
        bool tieneNumero = password.Any(char.IsDigit);
        bool tieneEspecial = password.Any(c => !char.IsLetterOrDigit(c));

        return tieneMayus && tieneMinus && tieneNumero && tieneEspecial;
    }*/
    
    private readonly IConfiguration _config;

    public AuthService(IConfiguration config)
    {
        _config = config;
    }

    public string HashPassword(string password) => BCrypt.Net.BCrypt.HashPassword(password);
    public bool VerifyPassword(string plain, string hashed) => BCrypt.Net.BCrypt.Verify(plain, hashed);
    public bool IsPasswordSecure(string password) => password.Length >= 8 && password.Any(char.IsUpper) && password.Any(char.IsLower) && password.Any(char.IsDigit) && password.Any(ch => !char.IsLetterOrDigit(ch));

    public string GenerateToken(int userId, string email, string role)
    {
        var key = Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]);
        var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}