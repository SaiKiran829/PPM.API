using PPM.Model;
using IEntityOperation;
using DAL;

namespace PPM.Domain
{
    public class EmployeeManagement : IEmployee
    {
        readonly EmployeeDal employeeDal = new EmployeeDal();
        public void AddEmployee(Employee employee)
        {
            employeeDal.InsertIntoEmployeeTable(employee.employeeID, employee.employeefirstName, employee.lastName, employee.email, employee.mobile, employee.address, employee.roleId);
        }

        public void DisplayAllEmployees()
        {
            employeeDal.ReadData();
        }

        public void DisplayEmployeeById(int employeeId)
        {
            employeeDal.ReadDataById(employeeId);
        }

        public void DeleteEmployee(int employeeId)
        {
            employeeDal.DeleteEmployeeFromTable(employeeId);
        }

    }
}
