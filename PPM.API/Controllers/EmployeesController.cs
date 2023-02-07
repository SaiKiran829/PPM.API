using Amazon.CognitoIdentity.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using PPM.DAl.Models;
using PPM.DTO;
using PPM.Model;

namespace PPM.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmployeesController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        try
        {
            using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
            {
                var employees = context.Employees.ToList();
                if(employees == null)
                {
                    return NotFound("No employees exists");
                }
                return Ok(employees);
            }
        }
        catch(Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpGet("{employeeId}")]
    public async Task<IActionResult> GetEmployeeById(int employeeId)
    {
        try
        {
            using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
            {
                var employee = context.Employees.FirstOrDefault(x => x.Employeeid == employeeId);
                if(employee == null)
                {
                    return NotFound("The employee with this id does not exist");
                }
                return Ok(employee);
            }
        }
        catch(Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public async Task<IActionResult> PostEmployees([FromBody]  EmployeeDto employee)
    {
        try
        {
            using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
            {

                var NewEmployee = new PPM.DAl.Models.Employee
                {
                    Employeeid = employee.employeeID,
                    FirstName = employee.employeefirstName,
                    LastName = employee.lastName,
                    Email= employee.email,
                    MobileNumber = employee.mobile,
                    Address = employee.address,
                    RoleId =employee.roleId,
                };
                if(employee.employeeID == null)
                {
                    return BadRequest("Employee Id cannot be null");
                }
                if(employee.employeefirstName == null)
                {
                    return BadRequest("Employee Name Cannot be null");
                }
                if(employee.lastName == null)
                {
                    return BadRequest("Employee Name Cannot be null");
                }
                if(employee.email == null)
                {
                    return BadRequest("Employee email Cannot be null");
                }
                if(employee.mobile == null)
                {
                    return BadRequest("Employee number Cannot be null");
                }
                if(employee.address == null)
                {
                    return BadRequest("Employee address Cannot be null");
                }
                if(employee.roleId == null)
                {
                    return BadRequest("Employee role id Cannot be null");
                }
                foreach(var employee2 in context.Employees.Include(x => x.Role).ToList())
                {
                    if(employee2.Employeeid == employee.employeeID)
                    {
                        return BadRequest("Employee Id already exists");
                    }
                }
                var find = context.Roles.Where(x => x.Roleid == employee.roleId).ToList();
                if(find.Count == 0)
                {
                    return BadRequest("This role id does not exist select another");
                }
                context.Employees.Add(NewEmployee);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetEmployeeById", new { Employeeid = NewEmployee.Employeeid }, NewEmployee);
            }
        }
        catch(Exception ex)
        {
            return BadRequest();
        }
    }

    [HttpPut]
    public async Task<IActionResult> PutEmployee([FromBody] EmployeeDto employee)
    {
        using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
        {
                var entity = context.Employees.FirstOrDefault(x => x.Employeeid == employee.employeeID);
                if (entity == null)
                {
                    return NotFound("No role exists with that id");
                }
                entity.Employeeid = employee.employeeID;
                entity.FirstName = employee.employeefirstName;
                entity.LastName = employee.lastName;
                entity.Email = employee.email;
                entity.MobileNumber = employee.mobile;
                entity.Address = employee.address;
                entity.RoleId= employee.roleId;
                await context.SaveChangesAsync();
                return Ok("Updated successfully");
        }
    }

    [HttpDelete("{employeeId}")]
    public async Task<IActionResult> DeleteEmployee(int employeeId)
    {
        try
        {
            using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
            {
                var removeEmployee = context.Employees.FirstOrDefault(x => x.Employeeid == employeeId);
                if (removeEmployee == null)
                {
                    return NotFound("No employee exists with that id");
                }
                context.Employees.Remove(removeEmployee);
                await context.SaveChangesAsync();
                return Ok("Deleted successfully");
            }
        }
        catch(Exception ex)
        {
            return BadRequest();
        }
    }
}
