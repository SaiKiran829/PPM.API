using System;
using System.Text.RegularExpressions;
using PPM.Domain;
using PPM.Model;
using DAL;

namespace UserInterface
{
    public class UI
    {
        public void View()
        {
            RoleManagement roleManagement = new RoleManagement();
            RoleDal roleDal = new RoleDal();
            EmployeeManagement employeeManagement = new EmployeeManagement();
            EmployeeDal employeeDal = new EmployeeDal();
            ProjectManagement projectManagement = new ProjectManagement();
            ProjectDal projectDal = new ProjectDal();
            Boolean error = false;
            Regex phoneNumber = new Regex(@"(^[0-9]{10}$)|(^\+[0-9]{2}\s+[0-9]{2}[0-9]{8}$)|(^[0-9]{3}-[0-9]{4}-[0-9]{4}$)");
            Regex emailFormat = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            Regex date = new Regex(@"(((0|1)[0-9]|2[0-9]|3[0-1])\/(0[1-9]|1[0-2])\/((19|20)\d\d))$");
        View:
            Console.WriteLine("");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("******************************************|   Hello Prolifics employee   |**********************************************");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.Write("                                           Enter \"1\" to view Project Module");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("                                           Enter \"2\" to view Employee Module");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.Write("                                           Enter \"3\" to view Role Module");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("                                           Enter \"x\" to exit application");
            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("");
            var read = Console.ReadLine();
            while (true)
            {
                switch (read)
                {
                    case "1":
                        while (true)
                        {
                        projectModule:
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("********************************************|    Project Module   |*****************************************************");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                            Console.Write("                                           Enter \"1\" to Add Project");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.Write("                                           Enter \"2\" to list all Projects");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.Write("                                           Enter \"3\" to list Project by Id");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.Write("                                           Enter \"4\" to add employee to project");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("                                           Enter \"5\" to delete employee from project");
                            Console.WriteLine("");
                            Console.WriteLine("                                           Enter \"6\" to View projects with employees");
                            Console.WriteLine("");
                            Console.WriteLine("                                           Enter \"7\" to delete a project");
                            Console.WriteLine("");
                            Console.WriteLine("                                           Enter \"x\" to get to main menu");
                            var projectSelector = Console.ReadLine();


                            switch (projectSelector)
                            {
                                case "1":
                                    do
                                    {
                                        try
                                        {
                                        inputprojectid:
                                            Console.WriteLine("Enter Project Id");
                                            int projectid = Convert.ToInt32(Console.ReadLine());
                                            if (projectDal.IfExistsInProjects(projectid))
                                            {
                                                Console.WriteLine("The id already exists try new id");
                                                Console.WriteLine("Enter any key to try again");
                                                Console.WriteLine("Enter \"x\" to get to main menu");
                                                string idTry = Console.ReadLine();
                                                if (idTry == "x")
                                                {
                                                    goto projectModule;
                                                }
                                                else
                                                {
                                                    goto inputprojectid;
                                                }
                                            }
                                            Console.WriteLine("Enter the name of project");
                                            string name = Console.ReadLine();
                                        StartDate:
                                            Console.WriteLine("Enter start date of project in DD/MM/YYYY format");
                                            string start = Console.ReadLine();
                                            if (!date.IsMatch(start))
                                            {
                                                Console.WriteLine("Invalid date format");
                                                Console.WriteLine("Enter any key to retry again");
                                                Console.WriteLine("Enter \"x\" to exit to main menu");
                                                var sDateread = Console.ReadLine();
                                                if (sDateread == "x")
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    goto StartDate;
                                                }

                                            }
                                        EndDate:
                                            Console.WriteLine("Enter end date of project in DD/MM/YYYY format");
                                            string end = Console.ReadLine();
                                            if (!date.IsMatch(end))
                                            {
                                                Console.WriteLine("Invalid date format");
                                                Console.WriteLine("Enter any key to retry again");
                                                Console.WriteLine("Enter \"x\" to exit to main menu");
                                                var eDateread = Console.ReadLine();
                                                if (eDateread == "x")
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    goto EndDate;
                                                }
                                            }
                                            if (Convert.ToDateTime(start).CompareTo(Convert.ToDateTime(end)) < 0)
                                            {
                                                Project project1 = new Project(name, start, end, projectid);
                                                projectManagement.AddProject(project1);
                                                Console.WriteLine("");
                                                Console.WriteLine("Added Successfully!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("end date should be greater than start date");
                                                Console.WriteLine("Enter any key to retry again");
                                                Console.WriteLine("Enter \"x\" to exit to main menu");
                                                var endDateread = Console.ReadLine();
                                                if (endDateread == "x")
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    goto EndDate;
                                                }
                                            }
                                            Console.ReadLine();
                                            Console.WriteLine("");
                                            Console.WriteLine("Would to like to add employees to this project ?");
                                            Console.WriteLine("Enter \"Yes\"  to add or enter anything to deny");
                                            var addEmployeeOrNot = Console.ReadLine();
                                            if (addEmployeeOrNot == "Yes")
                                            {
                                                employeeManagement.DisplayAllEmployees();
                                                Console.WriteLine("Above are the available employees");
                                                Console.WriteLine("Enter the id of employee to add into project");
                                                int employeeIdSelect = Convert.ToInt32(Console.ReadLine());
                                                if (employeeDal.IfExistsInEmployeeTable(employeeIdSelect))
                                                {
                                                    projectManagement.AddEmployeesIntoProject(projectid, employeeIdSelect);
                                                    Console.WriteLine("Added Successfully !");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Employee does not Exist");
                                                }

                                            }
                                            Console.WriteLine("Enter any key to get to main menu");
                                            Console.ReadLine();
                                        }

                                        catch (FormatException e)
                                        {
                                            Console.WriteLine("\nError : Only use numbers for id");
                                            Console.WriteLine("Enter any key to try again");
                                            Console.WriteLine("Enter \"x\" to get to main menu");
                                            read = Console.ReadLine();
                                            if (read == "x")
                                            {

                                                break;
                                            }
                                            error = true;
                                        }
                                        catch (Exception e)
                                        {
                                            Console.WriteLine("\nError : Only use numbers for id");
                                            Console.WriteLine("Enter any key to try again");
                                            Console.WriteLine("Enter \"x\" to get to main menu");

                                            read = Console.ReadLine();
                                            if (read == "x")
                                            {

                                                break;
                                            }
                                            error = true;
                                        }
                                    } while (error);
                                    break;

                                case "2":
                                    projectManagement.DisplayAllProjects();
                                    Console.WriteLine("Enter any key to get to main menu");
                                    Console.ReadLine();
                                    break;

                                case "3":
                                    try
                                    {
                                        Console.WriteLine("Search via project id");
                                        Console.WriteLine("Enter the id of project");
                                        int eid = Convert.ToInt32(Console.ReadLine());
                                        projectManagement.DisplayProjectById(eid);
                                        Console.WriteLine("Enter any key to get to main menu");
                                        Console.ReadLine();
                                    }
                                    catch (Exception e) { Console.WriteLine("Id can only be in numbers"); }
                                    break;

                                case "4":
                                    try
                                    {
                                        Console.WriteLine("");
                                        projectDal.ReadData();
                                        Console.WriteLine("Above are the available projects");
                                        Console.WriteLine();
                                        employeeDal.ReadData();
                                        Console.WriteLine("Above are the available employees");
                                        Console.WriteLine("Enter the Id of the project for which you want to add employees");
                                        int PROJId = Convert.ToInt32(Console.ReadLine());
                                        if (projectDal.IfExistsInProjects(PROJId))
                                        {
                                            Console.WriteLine("Enter the id of employee to add into project");
                                            int employeeIdSelecting = Convert.ToInt32(Console.ReadLine());
                                            if (employeeDal.IfExistsInEmployeeTable(employeeIdSelecting))
                                            {

                                                if (!projectDal.IfExistsInProjectsWithEmployees(PROJId, employeeIdSelecting))
                                                {
                                                    projectManagement.AddEmployeesIntoProject(PROJId, employeeIdSelecting);
                                                    Console.WriteLine("Added Successfully!!!!!!!");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("Employee already exists in this project");
                                                }
                                            }
                                            else
                                            {
                                                Console.WriteLine("Employee does not exist");
                                            }

                                        }
                                        else
                                        {
                                            Console.WriteLine("Project does not exist");
                                        }
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Invalid entry");
                                    }
                                    Console.WriteLine("Enter any key to get to main menu");
                                    Console.ReadLine();
                                    break;

                                case "5":
                                    try
                                    {
                                        Console.WriteLine("Enter the Id of the project for which you want to delete employees");
                                        int PROJId1 = Convert.ToInt32(Console.ReadLine());
                                        if (projectDal.IfExistsInProjects(PROJId1))
                                        {
                                            Console.WriteLine("Enter the id of employee to delete from project");
                                            int EmpId1 = Convert.ToInt32(Console.ReadLine());
                                            if (projectDal.IfExistsInProjectsWithEmployees(PROJId1, EmpId1))
                                            {
                                                projectManagement.DeleteEmployeeFromProject(PROJId1, EmpId1);
                                            }
                                            else
                                            {
                                                Console.WriteLine("This employee does not present in this project");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("The project do not IfExist");
                                        }
                                    }
                                    catch (FormatException e)
                                    {
                                        Console.WriteLine("Id can only be integer");
                                    }
                                    Console.WriteLine("Enter any key to get to main menu");
                                    Console.ReadLine();
                                    break;

                                case "6":
                                    try
                                    {
                                        Console.WriteLine("Enter the id of the project");
                                        int readForId = Convert.ToInt32(Console.ReadLine());
                                        if (projectDal.IfExistsInProjects(readForId))
                                        {
                                            if (projectDal.IfExistsInProjectsWithEmployeesTable(readForId))
                                            {
                                                projectManagement.DisplayAllEmployeeInProjectById(readForId);
                                            }
                                            else
                                            {
                                                Console.WriteLine("This project have no employees");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("No project present with that id");
                                        }
                                    }
                                    catch (FormatException e)
                                    {
                                        Console.WriteLine("Id can only be number");
                                    }
                                    Console.WriteLine("Enter any key to get to main menu");
                                    Console.ReadLine();
                                    break;


                                case "7":
                                    Console.WriteLine("Enter the Id of the project to delete");
                                    int idforDeleting = Convert.ToInt32(Console.ReadLine());
                                    if (projectDal.IfExistsInProjects(idforDeleting))
                                    {
                                        projectManagement.DeleteProject(idforDeleting);
                                        Console.WriteLine("");
                                        Console.WriteLine("Deleted Successfully !");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Project does not exists");
                                    }
                                    Console.WriteLine("Enter any key to get to main menu");
                                    Console.ReadLine();
                                    break;

                                case "x":
                                    goto View;

                                default:
                                    Console.WriteLine("");
                                    Console.WriteLine("Invalid input, please provide correct input");
                                    break;
                            }
                        }

                    case "2":
                        while (true)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("********************************************|    Employee Module   |****************************************************");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                            Console.Write("                                           Enter \"1\" to Add Employee");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.Write("                                           Enter \"2\" to list all Employees");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("                                           Enter \"3\" to list Employees by Id");
                            Console.WriteLine("");
                            Console.WriteLine("                                           Enter \"4\" to delete Employee");
                            Console.WriteLine("");
                            Console.WriteLine("                                           Enter \"x\" to get to main menu");
                            var employeeSelector = Console.ReadLine();
                            switch (employeeSelector)
                            {
                                case "1":
                                inputempid:
                                    try
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("");
                                        Console.WriteLine("Enter the Id of employee");
                                        int empId = Convert.ToInt32(Console.ReadLine());
                                        if (employeeDal.IfExistsInEmployeeTable(empId))
                                        {
                                            Console.WriteLine("\nError : Employee with this employee id already exists");
                                            Console.WriteLine("Enter any key to try again");
                                            Console.WriteLine("Enter \"x\" to get to main menu");
                                            read = Console.ReadLine();
                                            if (read == "x")
                                            {

                                                break;
                                            }
                                            else
                                            {
                                                goto inputempid;
                                            }
                                        }
                                        Console.WriteLine("Enter employee first name");
                                        var fname = Console.ReadLine();
                                        Console.WriteLine("Enter employee last name");
                                        var lname = Console.ReadLine();
                                    Email:
                                        Console.WriteLine("Enter employee email id");
                                        var EMAIL = Console.ReadLine();
                                        if (!emailFormat.IsMatch(EMAIL))
                                        {
                                            Console.WriteLine("Invalid email format");
                                            Console.WriteLine("Enter any key to retry again");
                                            Console.WriteLine("Enter \"x\" to exit to main menu");
                                            var emailread = Console.ReadLine();
                                            if (emailread == "x")
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                goto Email;
                                            }

                                        }
                                    mobile:
                                        Console.WriteLine("Enter employee mobile number");
                                        var mobile = Console.ReadLine();
                                        if (!phoneNumber.IsMatch(mobile))
                                        {
                                            Console.WriteLine("Invalid mobile number format");
                                            Console.WriteLine("Enter any key to retry again");
                                            Console.WriteLine("Enter \"x\" to exit to main menu");
                                            var mobread = Console.ReadLine();
                                            if (mobread == "x")
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                goto mobile;
                                            }
                                        }
                                        Console.WriteLine("Enter address of the employee");
                                        var address = Console.ReadLine();
                                    roleID:
                                        Console.WriteLine("Enter the Role Id");
                                        int roleID = Convert.ToInt32(Console.ReadLine());
                                        if (roleDal.IfExistsInRoleTable(roleID))
                                        {
                                            Employee employeeToTable = new Employee(empId, fname, lname, EMAIL, mobile, address, roleID);
                                            employeeManagement.AddEmployee(employeeToTable);
                                        }
                                        else
                                        {
                                            Console.WriteLine("This role id does not exist");
                                            Console.WriteLine();
                                            roleDal.ReadData();
                                            Console.WriteLine("Above are the available roles");
                                            Console.WriteLine();
                                            Console.WriteLine("Enter any key to retry again");
                                            Console.WriteLine("Enter \"x\" to exit to main menu");
                                            var roleidread = Console.ReadLine();
                                            if (roleidread == "x")
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                goto roleID;
                                            }
                                        }
                                    }
                                    catch (FormatException ex)
                                    {
                                        Console.WriteLine("Id cannot be null or character");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Id cannot be null or character");
                                    }
                                    Console.WriteLine("Enter any key to get to main menu");
                                    Console.ReadLine();
                                    break;

                                case "2":
                                    employeeManagement.DisplayAllEmployees();
                                    Console.WriteLine("Enter any key to get to main menu");
                                    Console.ReadLine();
                                    break;

                                case "3":
                                    try
                                    {
                                        Console.WriteLine("Enter the id of the employee");
                                        int searchEmployeeById = Convert.ToInt32(Console.ReadLine());
                                        if (!employeeDal.IfExistsInEmployeeTable(searchEmployeeById))
                                        {
                                            Console.WriteLine("\nError : This employee id does not exists");
                                        }
                                        else
                                        {
                                            employeeManagement.DisplayEmployeeById(searchEmployeeById);
                                        }
                                    }
                                    catch (FormatException e)
                                    {
                                        Console.WriteLine("Id can only be numbers");
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Invalid input");
                                    }
                                    Console.WriteLine("Enter any key to get to main menu");
                                    Console.ReadLine();
                                    break;

                                case "4":
                                    try
                                    {
                                        Console.WriteLine("Enter id of the employee ");
                                        int idForDeleting = Convert.ToInt32(Console.ReadLine());
                                        if (employeeDal.IfExistsInEmployeeTable(idForDeleting))
                                        {
                                            employeeManagement.DeleteEmployee(idForDeleting);
                                            Console.WriteLine("Enter any key to get to main menu");
                                            Console.ReadLine();
                                        }
                                        else
                                        {
                                            Console.WriteLine("No employee with this id found to delete");
                                            Console.WriteLine("Enter any key to get to main menu");
                                            Console.ReadLine();
                                        }
                                    }
                                    catch (FormatException e)
                                    {
                                        Console.WriteLine("Enter valid input");
                                    }
                                    break;
                                case "x":
                                    goto View;

                                default:
                                    Console.WriteLine("");
                                    Console.WriteLine("Invalid input, please provide correct input");
                                    break;
                            }
                        }

                    case "3":
                        while (true)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                            Console.WriteLine("************************************************|    Role Module   |****************************************************");
                            Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                            Console.Write("                                                Enter \"1\" to Add Role");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.Write("                                                Enter \"2\" to list all Roles");
                            Console.WriteLine("");
                            Console.WriteLine("");
                            Console.WriteLine("                                                Enter \"3\" to list Roles by Id");
                            Console.WriteLine("");
                            Console.WriteLine("                                                Enter \"4\" to delete Role");
                            Console.WriteLine("");
                            Console.WriteLine("                                                Enter \"x\" to get to main menu");
                            Console.WriteLine("");
                            var roleSelector = Console.ReadLine();
                            switch (roleSelector)
                            {
                                case "1":
                                    try
                                    {
                                        Console.WriteLine("");
                                        Console.WriteLine("");
                                    inputroleid:
                                        Console.WriteLine("Enter the Role Id");
                                        int roleIDD = Convert.ToInt32(Console.ReadLine());
                                        if (roleDal.IfExistsInRoleTable(roleIDD))
                                        {
                                            Console.WriteLine("\nError : This role id already exists");
                                            Console.WriteLine("Enter any key to try again");
                                            Console.WriteLine("Enter \"x\" to get to main menu");
                                            read = Console.ReadLine();
                                            if (read == "x")
                                            {

                                                break;
                                            }
                                            else
                                            {
                                                goto inputroleid;
                                            }
                                        }
                                        Console.WriteLine("Enter the name of Role");
                                        string roleName = Convert.ToString(Console.ReadLine());
                                        Role roleAddToTable = new Role(roleIDD, roleName);
                                        roleManagement.RoleAdd(roleAddToTable);
                                        Console.WriteLine("Enter any key to get to main menu");
                                        Console.ReadLine();
                                    }
                                    catch (Exception e)
                                    {
                                        Console.WriteLine("Role Id can only be in numbers");
                                        Console.ReadLine();
                                    }
                                    break;

                                 case "2":
                                     roleManagement.ViewRoles();
                                     Console.WriteLine("Enter any key to get to main menu");
                                     Console.ReadLine();
                                     break;

                                 case "3":
                                     try
                                     {
                                        inputroleIDD:
                                         Console.WriteLine("Enter the id of the role");
                                         int searchRoleById = Convert.ToInt32(Console.ReadLine());
                                        if (!roleDal.IfExistsInRoleTable(searchRoleById))
                                        {
                                            Console.WriteLine("\nError : This role id does not exists");
                                            Console.WriteLine("Enter any key to try again");
                                            Console.WriteLine("Enter \"x\" to get to main menu");
                                            read = Console.ReadLine();
                                            if (read == "x")
                                            {

                                                break;
                                            }
                                            else
                                            {
                                                goto inputroleIDD;
                                            }
                                        }
                                        Console.WriteLine();
                                        roleManagement.SearchById(searchRoleById);
                                        Console.WriteLine();
                                        Console.WriteLine("Enter any key to get to main menu");
                                         Console.ReadLine();
                                     }
                                     catch (FormatException e)
                                     {
                                         Console.WriteLine("Id can only be numbers");
                                     }
                                     catch (Exception e)
                                     {
                                         Console.WriteLine("Invalid input");
                                     }
                                     break;

                                 case "4":
                                     try
                                     {
                                        deleteRoleById:
                                         Console.WriteLine("Enter the id of the role");
                                         int deleteRoleById = Convert.ToInt32(Console.ReadLine());
                                        if (!roleDal.IfExistsInRoleTable(deleteRoleById))
                                        {
                                            Console.WriteLine("\nError : This role id does not exist");
                                            Console.WriteLine("Enter any key to try again");
                                            Console.WriteLine("Enter \"x\" to get to main menu");
                                            read = Console.ReadLine();
                                            if (read == "x")
                                            {

                                                break;
                                            }
                                            else
                                            {
                                                goto deleteRoleById;
                                            }
                                        }
                                        if(roleDal.IfExistsInEmployeeTable(deleteRoleById))
                                        {
                                            Console.WriteLine("An employee with this role id exists, delete that first");
                                        }
                                        else
                                        {
                                            roleManagement.DeleteById(deleteRoleById);
                                        }
                                         Console.WriteLine("Enter any key to get to main menu");
                                         Console.ReadLine();
                                     }
                                     catch (FormatException e)
                                     {
                                         Console.WriteLine("Id can only be numbers");
                                     }
                                     catch (Exception e)
                                     {
                                         Console.WriteLine("Invalid input");
                                     }
                                     break;
                                case "x":
                                    goto View;

                                default:
                                    Console.WriteLine("Invalid entry");
                                    break;

                            }
                        }
                    case "x":
                        return;
                }
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("******************************************|   Hello Prolifics employee   |**********************************************");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.Write("                                           Enter \"1\" to view Project Module");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.Write("                                           Enter \"2\" to view Employee Module");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.Write("                                           Enter \"3\" to view Role Module");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("                                           Enter \"x\" to exit application");
                Console.WriteLine("------------------------------------------------------------------------------------------------------------------------");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                Console.WriteLine("");
                read = Console.ReadLine();
            }
        }
    }
}
