using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PPM.DAl.Models;

public partial class ProlificsProjectManagementEntities : DbContext
{
    public ProlificsProjectManagementEntities()
    {
    }

    public ProlificsProjectManagementEntities(DbContextOptions<ProlificsProjectManagementEntities> options)
        : base(options)
    {
    }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<Role> Roles { get; set; }



    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseMySql("server=10.11.50.58;port=3306;user= tempuser;password=Admin@1234;database=prolificsprojectmanagement", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.31-mysql"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Employeeid).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.HasIndex(e => e.RoleId, "roleId");

            entity.Property(e => e.Employeeid)
                .ValueGeneratedNever()
                .HasColumnName("employeeid");
            entity.Property(e => e.Address)
                .HasMaxLength(200)
                .HasColumnName("address");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .HasColumnName("firstName");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .HasColumnName("lastName");
            entity.Property(e => e.MobileNumber)
                .HasMaxLength(15)
                .HasColumnName("mobileNumber");
            entity.Property(e => e.RoleId).HasColumnName("roleId");

            entity.HasOne(d => d.Role).WithMany(p => p.Employees)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("employees_ibfk_1");
        });

        modelBuilder.Entity<Project>(entity =>
        {
            entity.HasKey(e => e.ProjectId).HasName("PRIMARY");

            entity.ToTable("projects");

            entity.Property(e => e.ProjectId).HasColumnName("projectId");
            entity.Property(e => e.EndDate)
                .HasMaxLength(15)
                .HasColumnName("endDate");
            entity.Property(e => e.ProjectName)
                .HasMaxLength(50)
                .HasColumnName("projectName");
            entity.Property(e => e.StartDate)
                .HasMaxLength(15)
                .HasColumnName("startDate");

            entity.HasMany(d => d.Employees).WithMany(p => p.Projects)
                .UsingEntity<Dictionary<string, object>>(
                    "Projectswithemployee",
                    r => r.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("Constr_ProjectsWithEmployees_employeeId_fk"),
                    l => l.HasOne<Project>().WithMany()
                        .HasForeignKey("ProjectId")
                        .HasConstraintName("Constr_ProjectsWithEmployees_projectId_fk"),
                    j =>
                    {
                        j.HasKey("ProjectId", "EmployeeId")
                            .HasName("PRIMARY")
                            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                        j
                            .ToTable("projectswithemployees")
                            .HasCharSet("ascii")
                            .UseCollation("ascii_general_ci");
                        j.HasIndex(new[] { "EmployeeId" }, "Constr_ProjectsWithEmployees_employeeId_fk");
                    });
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Roleid).HasName("PRIMARY");

            entity.ToTable("roles");

            entity.Property(e => e.Roleid).HasColumnName("roleid");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("roleName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
