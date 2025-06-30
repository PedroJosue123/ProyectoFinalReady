namespace Domain.Entities;

public class UserProfileDomain
{
    public int Id { get; }
    public int UserId { get; }
    public string? Name { get; }
    public string? Ruc { get; }
    public string? ManagerName { get; }
    public string? ManagerDni { get; }
    public string ManagerEmail { get; }
    public string? Phone { get; }
    public string? Address { get; }
    
    public string PaymentPasswordHash { get; }
    

    public UserProfileDomain(int id, int userId, string? name, string? ruc, string? managerName, string? managerDni, string managerEmail, string? phone, string? address, string paymentPasswordHash)
    {
        Id = id;
        UserId = userId;
        Name = name;
        Ruc = ruc;
        ManagerName = managerName;
        ManagerDni = managerDni;
        ManagerEmail = managerEmail;
        Phone = phone;
        Address = address;
        PaymentPasswordHash = paymentPasswordHash;
    }
}