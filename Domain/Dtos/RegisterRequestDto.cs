namespace Domain.Dtos;

public class RegisterRequestDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public int UserTypeId { get; set; }

    public string Name { get; set; }
    public string? Ruc { get; set; }
    public string ManagerName { get; set; }
    public string ManagerDni { get; set; }
    public string ManagerEmail { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string PaymentPasswordHash { get;set; }
}