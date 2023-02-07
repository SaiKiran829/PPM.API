using System;
using System.Collections.Generic;

namespace PPM.DAl.Models;

public partial class Role
{
    public int Roleid { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
