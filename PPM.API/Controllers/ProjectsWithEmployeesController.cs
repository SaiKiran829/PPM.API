using Microsoft.AspNetCore.Mvc;
using PPM.DAl.Models;
using Microsoft.EntityFrameworkCore;


namespace PPM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsWithEmployeesController : Controller
    {
        [HttpGet]
        public async Task<ActionResult<IAsyncEnumerable<Project>>> GetProjectsWithEmployees(int projectId)
        {
            try
            {
                using (ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var employeesInProject = context.Projects.Where(x => x.ProjectId == projectId).First().Employees.ToList();
                    if (employeesInProject == null)
                    {
                        return NotFound("No employees in this project");
                    }
                    return Ok(employeesInProject);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployeeToProject(int projectId, int employeeId)
        {
            try
            {
                using (ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var project = context.Projects.FirstOrDefault(x => x.ProjectId == projectId);
                    if (project == null)
                    {
                        return NotFound("Project does not exists");
                    }
                    var employee = context.Employees.FirstOrDefault(x => x.Employeeid == employeeId);
                    if (employee == null)
                    {
                        return NotFound("Employee does not exists");
                    }
                    context.Projects.Find(projectId).Employees.Add(employee);
                    await context.SaveChangesAsync();
                    return Ok("Employee Added to project");
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error occured");
            }
        }

        [HttpDelete]
        public async Task<IActionResult> RemoveEmployeeFromProject(int projectId, int employeeId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var project = context.Projects.FirstOrDefault(x => x.ProjectId == projectId);
                    if(project == null)
                    {
                        return NotFound("Project not found");
                    }
                    var employee = context.Employees.FirstOrDefault(x => x.Employeeid == employeeId);
                    if(employee == null)
                    {
                        return NotFound("employee does not exists");
                    }
                    var employeeInProject = context.Projects.Include(x => x.Employees).Where(x => x.ProjectId == projectId).First().Employees.FirstOrDefault(x => x.Employeeid == employeeId);
                    if(employeeInProject == null)
                    {
                        return NotFound("Employee does not exists in this project");
                    }
                    context.Projects.Find(projectId).Employees.Remove(employeeInProject);
                    await context.SaveChangesAsync();
                    return Ok("Deleted successfully");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Error occured");
            }
        }
    }
}