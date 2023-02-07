using ConsoleTables;
using DAL;
using FluentAssertions;

namespace NUnitTests;

[TestFixture]
public class Tests
{
    int roleId = 5;
    string roleName = "Technical Architech";
    int employeeId = 9;
    int projectId = 10;

    [Test]
    public void AddingRole()
    {
        RoleDal roleDal = new RoleDal();
        roleDal.InsertIntoRoleTable(roleId,roleName);
        Assert.True(roleDal.IfExistsInRoleTable(roleId));
    }


    [Test]
    public void DeleteRoleFromTable()
    {
        RoleDal roleDal = new RoleDal();
        int roleIdToDelete = 8;
        string roleNameToDelete = "TechLead";
        roleDal.InsertIntoRoleTable(roleIdToDelete,roleNameToDelete);
        roleDal.DeleteRoleFromTable(roleIdToDelete);
        Assert.False(roleDal.IfExistsInRoleTable(roleIdToDelete));
    }

    [Test]
    public void AddingEmployee()
    {
        
        string firstName = "Sai Kiran";
        string lastName = "Pasapula";
        string emailId = "Sai@gmail.com";
        string mobile = "9182234756";
        string address = "home";
        int roleId = 1;
        EmployeeDal employeeDal = new EmployeeDal();
        employeeDal.InsertIntoEmployeeTable(employeeId,firstName,lastName,emailId,mobile,address,roleId);
        Assert.True(employeeDal.IfExistsInEmployeeTable(employeeId));
    }

    [Test]
    public void DeleteEmployee()
    {
        EmployeeDal employeeDal = new EmployeeDal();
        employeeDal.DeleteEmployeeFromTable(employeeId);
        Assert.False(employeeDal.IfExistsInEmployeeTable(employeeId));
    }

    [Test]
    public void Addingproject()
    {
        string projectName = "Prolifics";
        string startDate = "02/02/2022";
        string endDate = "02/02/2023";
        ProjectDal projectDal = new ProjectDal();
        projectDal.InsertIntoProjectTable(projectId,projectName,startDate,endDate);
        Assert.True(projectDal.IfExistsInProjects(projectId));
    }

    [Test]
    public void DeleteProject()
    {
        ProjectDal projectDal = new ProjectDal();
        projectDal.DeleteProject(projectId);
        Assert.False(projectDal.IfExistsInProjects(projectId));
    }

    [Test]
    public void AddingEmployeeIntoProject()
    {
        string projectName = "Prolifics";
        string startDate = "02/02/2022";
        string endDate = "02/02/2023";
        ProjectDal projectDal = new ProjectDal();
        projectDal.InsertIntoProjectTable(projectId,projectName,startDate,endDate);

         string firstName = "Sai Kiran";
        string lastName = "Pasapula";
        string emailId = "Sai@gmail.com";
        string mobile = "9182234756";
        string address = "home";
        int roleId = 1;
        EmployeeDal employeeDal = new EmployeeDal();
        employeeDal.InsertIntoEmployeeTable(employeeId,firstName,lastName,emailId,mobile,address,roleId);

        projectDal.AddEmployeesIntoProject(projectId,employeeId);
        Assert.True(projectDal.IfExistsInProjectsWithEmployees(projectId,employeeId));

         projectDal.DeleteProject(projectId);
         employeeDal.DeleteEmployeeFromTable(employeeId);

    }


    [Test]
    public void DeleteEmployeeFromProject()
    {
        string projectName = "Prolifics";
        string startDate = "02/02/2022";
        string endDate = "02/02/2023";
        ProjectDal projectDal = new ProjectDal();
        projectDal.InsertIntoProjectTable(projectId,projectName,startDate,endDate);

         string firstName = "Sai Kiran";
        string lastName = "Pasapula";
        string emailId = "Sai@gmail.com";
        string mobile = "9182234756";
        string address = "home";
        int roleId = 1;
        EmployeeDal employeeDal = new EmployeeDal();
        employeeDal.InsertIntoEmployeeTable(employeeId,firstName,lastName,emailId,mobile,address,roleId);

        projectDal.AddEmployeesIntoProject(projectId,employeeId);

        projectDal.DeleteEmployeeFromProject(projectId,employeeId);

        Assert.False(projectDal.IfExistsInProjectsWithEmployees(projectId,employeeId));

         projectDal.DeleteProject(projectId);
         employeeDal.DeleteEmployeeFromTable(employeeId);

    }

}