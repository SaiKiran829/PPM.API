using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PPM.DAl.Models;

public partial class Employee
{
    public int Employeeid { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? MobileNumber { get; set; }

    public string? Address { get; set; }

    public int RoleId { get; set; }

    [JsonIgnore]
    public virtual Role Role { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; } = new List<Project>();
}
