using ConsoleTables;
using PPM.DAl.Models;
using PPM.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using MySqlConnector;

namespace DAL
{
    public class RoleDal
    {
        public void InsertIntoRoleTable(int roleId, string roleName)
        {
            try
            {
                using (var context = new ProlificsProjectManagementEntities())
                {
                    PPM.DAl.Models.Role role = new PPM.DAl.Models.Role();
                    {
                        role.Roleid = roleId;
                        role.RoleName = roleName;
                    };
                    context.Roles.Add(role);
                    context.SaveChanges();
                    Console.WriteLine();
                    Console.WriteLine("Added successfully");
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops an error occured " + e);
            }
        }

        public void ReadData()
        {
            try
            {
                using (ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var roles = context.Roles.ToList();
                    var table = new ConsoleTable("Role Id", "Role Name ");
                    foreach (var role in roles)
                    {
                        table.AddRow(role.Roleid,role.RoleName);
                    }
                    table.Write();
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops an error occured " + e);
            }
        }


        public void ReadDataById(int roleId)
        {
            try
            {
                using (ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    PPM.DAl.Models.Role role = context.Roles.Find(roleId);
                    if(role != null)
                    {
                        var table = new ConsoleTable("Role Id", "Role Name ");
                        table.AddRow(role.Roleid, role.RoleName);
                        table.Write();
                    }
                    else
                    {
                        Console.WriteLine("Role with this id does not exists");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Id already exists" + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops error occured" + ex);
            }
        }

        public void DeleteRoleFromTable(int roleId)
        {
            try
            {
                using (ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    PPM.DAl.Models.Role role = context.Roles.Find(roleId);
                    if(role != null)
                    {
                        context.Roles.Remove(role);
                        context.SaveChanges();
                        Console.WriteLine();
                        Console.WriteLine("Deleted successfully");
                        Console.WriteLine();
                    }
                    else
                    {
                        Console.WriteLine("This role id does not exist ");
                    }
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Cannot find id" + ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops, error occured" + ex);
            }
        }

        public Boolean IfExistsInRoleTable(int roleId)
        {
            using (ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
            {
                PPM.DAl.Models.Role role = context.Roles.Find(roleId);
                if(role != null)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean IfExistsInEmployeeTable(int roleId)
        {
            using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
            {
                foreach(var employee in context.Employees)
                {
                    if(employee.RoleId == roleId)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}