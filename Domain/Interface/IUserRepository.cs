namespace Domain.Interface;

public interface IUserRepository<User>
{
    Task<User?> GetUserWithUserTypeByEmailAsync(string email, CancellationToken cancellationToken);


}