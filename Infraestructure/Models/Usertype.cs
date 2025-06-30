using System;
using System.Collections.Generic;

namespace Infraestructure.Models;

public partial class Usertype
{
    public int UserTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
