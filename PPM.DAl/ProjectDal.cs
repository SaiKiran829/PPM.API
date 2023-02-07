using ConsoleTables;
using PPM.Model;
using PPM.DAl.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ProjectDal
    {
        public void InsertIntoProjectTable(int projectId, string projectName, string startDate, string endDate)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    PPM.DAl.Models.Project project = new PPM.DAl.Models.Project()
                    {
                        ProjectId = projectId,
                        ProjectName = projectName,
                        StartDate = startDate,
                        EndDate = endDate
                    };
                    context.Projects.Add(project);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops, an error occured " + ex);
            }
        }

        public Boolean IfExistsInProjects(int projectId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    PPM.DAl.Models.Project project = context.Projects.Find(projectId);
                    if(project != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops, error occured");
            }
            return false;
        }

        public void ReadData()
        {
            try
            {
                using (ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var projects = context.Projects.ToList();
                    if(projects.Count> 0)
                    {
                        var table = new ConsoleTable("Project Id", "Name ", "Start Date", "End Date");
                        foreach (var project in projects)
                        {
                            table.AddRow(project.ProjectId, project.ProjectName, project.StartDate, project.EndDate);
                        }
                        table.Write();
                    }
                    else
                    {
                        Console.WriteLine("No projects available");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops an error occured");
            }
        }

        public void ReadDataById(int projectId)
        {
            try
            {
                using (ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    PPM.DAl.Models.Project project = context.Projects.Find(projectId);
                    if(project != null)
                    {
                        var table = new ConsoleTable("Project Id", "Name ", "Start Date", "End Date");
                        table.AddRow(project.ProjectId, project.ProjectName, project.StartDate, project.EndDate);
                        table.Write() ;
                    }
                    else
                    {
                        Console.WriteLine("No project exists with that id");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops error occured");
            }
        }

        public void AddEmployeesIntoProject(int projectId, int employeeId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var employee = context.Employees.Find(employeeId);
                    context.Projects.Find(projectId).Employees.Add(employee);
                    context.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops error occured  " + ex);
            }
        }

        public void sort(List<PPM.DAl.Models.Employee> arr)
        {
            int n = arr.Count;

            for (int i = 0; i < n - 1; i++)
            {
                int min_idx = i;
                for (int j = i + 1; j < n; j++)
                    if (arr[j].RoleId < arr[min_idx].RoleId)
                        min_idx = j;

                int temp = arr[min_idx].RoleId;
                arr[min_idx] = arr[i];
                arr[i].RoleId = temp;
            }
        }

            public void DisplayAllEmployeeInProjectById(int projectId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var employeeList = context.Projects.Include(p => p.Employees).ThenInclude(x => x.Role).Where(x => x.ProjectId == projectId).First().Employees.ToList();
                    employeeList.OrderBy(x => x.RoleId);
                    if(employeeList.Count > 0)
                    {
                        Console.WriteLine($"Name of the project - {context.Projects.Find(projectId).ProjectName}");
                        Console.WriteLine();
                        var table = new ConsoleTable("Employee Id", "Employee Name", "Role Name");
                        foreach (var employee in employeeList)
                        {
                            table.AddRow(employee.Employeeid, employee.FirstName, employee.Role.RoleName);
                        }
                        table.Write();
                    }
                    else
                    {
                        Console.WriteLine("No employees in this project");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops error occured  " + ex);
            }
        }

        public void DeleteProject(int projectId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    PPM.DAl.Models.Project project = context.Projects.Find(projectId);
                    context.Projects.Remove(project);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops, error occured" + ex);
            }
        }

        public void DeleteEmployeeFromProject(int projectId, int employeeId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var employee = context.Employees.Find(employeeId);
                    context.Projects.Include(x => x.Employees).Where(x => x.ProjectId == projectId).First().Employees.Remove(employee);
                    context.SaveChanges();
                    Console.WriteLine("Deleted Successfully");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops, error occured");
            }
        }

        public Boolean IfExistsInProjectsWithEmployees(int projectId, int employeeId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var employee = context.Employees.Find(employeeId);
                    var employeeList = context.Projects.Include(p => p.Employees).ThenInclude(x => x.Role).Where(x => x.ProjectId == projectId).First().Employees.ToList();
                    if (employeeList.Contains(employee))
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops, error occured");
            }
            return false;
        }

        public Boolean IfExistsInProjectsWithEmployeesTable(int projectId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    if (context.Projects.Find(projectId).Employees != null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Oops, error occured" + ex);
            }
            return false;
        }
    }
}
