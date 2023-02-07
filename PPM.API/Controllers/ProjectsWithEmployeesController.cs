using Microsoft.AspNetCore.Mvc;
using PPM.DAl.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;


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
                    var employeesInProject = context.Projects.FirstOrDefault(p => p.ProjectId == projectId).Employees.ToList();
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
    }
}
