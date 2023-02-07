using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using PPM.DAl.Models;
using System.Data.SqlClient;

namespace DAL
{
    public class EmployeeDal
    {
        public void InsertIntoEmployeeTable(int employeeId, string firstName, string lastName, string email, string mobileNumber, string address, int roleId)
        {
            try
            {
                using (ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    PPM.DAl.Models.Employee employee = new PPM.DAl.Models.Employee()
                    {
                        Employeeid = employeeId,
                        FirstName = firstName,
                        LastName = lastName,
                        Email = email,
                        MobileNumber = mobileNumber,
                        Address = address,
                        RoleId = roleId
                    };
                    context.Employees.Add(employee);
                    context.SaveChanges();
                    Console.WriteLine();
                    Console.WriteLine("Added successfully");
                    Console.WriteLine();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops! error occured ");
            }
        }
        public Boolean IfExistsInEmployeeTable(int employeeId)
        {
            using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
            {
                PPM.DAl.Models.Employee employee = context.Employees.Find(employeeId);
                if(employee != null)
                {
                    return true;
                }
            }
            return false;
        }

        public void ReadData()
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var employees = context.Employees.Include(e => e.Role).ToList();
                    if(employees.Count > 0)
                    {
                        var table = new ConsoleTable("Employee Id", "First name", "Last name", "Email address", "Phone number", "Address", "Role Id","Role Name");
                        foreach (var employee in employees)
                        {
                            table.AddRow(employee.Employeeid,employee.FirstName,employee.LastName,employee.Email,employee.MobileNumber,employee.Address,employee.RoleId,employee.Role.RoleName);
                        }
                        table.Write();
                    }
                    else
                    {
                        Console.WriteLine("Employee list is empty");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops error occured"+ex);
            }
        }


        public void ReadDataById(int employeeId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    PPM.DAl.Models.Employee employee = context.Employees.Include(e => e.Role).Where(e => e.Employeeid == employeeId).FirstOrDefault();
                    var table = new ConsoleTable("Employee Id", "First name", "Last name", "Email address", "Phone number", "Address", "Role Id", "Role Name");
                    table.AddRow(employee.Employeeid,employee.FirstName,employee.LastName,employee.Email,employee.MobileNumber,employee.Address,employee.RoleId,employee.Role.Roleid);
                    table.Write();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops error occured" + ex);
            }
        }

        public void DeleteEmployeeFromTable(int employeeId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    PPM.DAl.Models.Employee employee = context.Employees.Find(employeeId);
                    if(employee != null )
                    {
                        context.Employees.Remove(employee);
                        context.SaveChanges();
                        Console.WriteLine();
                        Console.WriteLine("Deleted successfully");
                        Console.WriteLine();
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Cannot find id" + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops, error occured" + ex);
            }
        }
    }
}
