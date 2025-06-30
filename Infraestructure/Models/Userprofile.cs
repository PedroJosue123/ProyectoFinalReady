using System;
using System.Collections.Generic;

namespace Infraestructure.Models;

public partial class Userprofile
{
    public int UserProfileId { get; set; }

    public int UserId { get; set; }

    public string? Name { get; set; }

    public string? Ruc { get; set; }

    public string? ManagerName { get; set; }

    public string? ManagerDni { get; set; }

    public string ManagerEmail { get; set; } = null!;

    public string? Phone { get; set; }

    public string? Address { get; set; }

    public string PaymentPassword { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
