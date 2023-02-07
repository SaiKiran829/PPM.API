using PPM.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEntityOperation
{
    public interface IEmployee
    {
        void AddEmployee(Employee employee);
        void DisplayAllEmployees();
        void DisplayEmployeeById(int employeeId);
        void DeleteEmployee(int employeeId);
    }
}
