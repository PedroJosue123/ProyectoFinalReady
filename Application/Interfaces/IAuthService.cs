namespace Application.Interfaces;

public interface IAuthService
{
   
    
    string HashPassword(string password);
    bool VerifyPassword(string plain, string hashed);
    bool IsPasswordSecure(string password);
    string GenerateToken(int userId, string email, string role);
}