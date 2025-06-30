namespace Domain.Entities;

public class UserDomain
{
    public int Id { get; }
    public string Email { get; }
    public string PasswordHash { get; }
    public DateTime CreatedAt { get; }
    public int UserTypeId { get; }
    public int FailedLoginAttempts { get; private set; }
    public DateTime? LockoutUntil { get; private set; }

    public UserDomain(int id, string email, string passwordHash, DateTime createdAt, int userTypeId,
        int failedLoginAttempts = 0, DateTime? lockoutUntil = null)
    {
        Id = id;
        Email = email;
        PasswordHash = passwordHash;
        CreatedAt = createdAt;
        UserTypeId = userTypeId;
        FailedLoginAttempts = failedLoginAttempts;
        LockoutUntil = lockoutUntil;
    }

    public void IncrementFailedLogin() => FailedLoginAttempts++;
    public void LockAccount(TimeSpan duration) => LockoutUntil = DateTime.UtcNow.Add(duration);
    public void ResetLoginAttempts() { FailedLoginAttempts = 0; LockoutUntil = null; }
}