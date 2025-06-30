using System;
using System.Collections.Generic;

namespace Infraestructure.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public int UserTypeId { get; set; }

    public int? FailedLoginAttempts { get; set; }

    public DateTime? LockoutUntil { get; set; }

    public virtual ICollection<Pedido> PedidoIdCompradorNavigations { get; set; } = new List<Pedido>();

    public virtual ICollection<Pedido> PedidoIdProveedorNavigations { get; set; } = new List<Pedido>();

    public virtual Usertype UserType { get; set; } = null!;

    public virtual Userprofile? Userprofile { get; set; }
}
