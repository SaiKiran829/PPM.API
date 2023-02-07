using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using PPM.DAl.Models;
using PPM.DTO;

namespace PPM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProjects()
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var projects = context.Projects.ToList();
                    if(projects == null)
                    {
                        return NotFound("No projects found");
                    }
                    return Ok(projects);
                }
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectById(int projectId)
        {
            using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
            {
                var project = context.Projects.FirstOrDefault(p => p.ProjectId == projectId);
                if(project == null)
                {
                    return NotFound("No project exists with that id");
                }
                return Ok(project);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostProject([FromBody] ProjectDto project)
        {
            using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
            {
                var newProject = new Project
                {
                    ProjectId = project.id,
                    ProjectName = project.projectName,
                    StartDate = project.startDate,
                    EndDate = project.endDate
                };
                if(project.id == null)
                {
                    return BadRequest("Project Id cannot be empty");
                }
                if (project.projectName == null)
                {
                    return BadRequest("Project name cannot be null");
                }
                if(project.startDate == null)
                {
                    return BadRequest("project must have start date");
                }
                if(project.endDate == null)
                {
                    return BadRequest("Project must have end date");
                }
                var projectId = context.Projects.FirstOrDefault(x => x.ProjectId == project.id);
                if(projectId != null)
                {
                    return BadRequest("Project with this id already exists");
                }
                context.Projects.Add(newProject);
                await context.SaveChangesAsync();
                return CreatedAtAction("GetProjectById", new { ProjectId = newProject.ProjectId }, newProject);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject([FromBody] ProjectDto project)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var entity = context.Projects.FirstOrDefault(x => x.ProjectId == project.id);
                    if (entity == null)
                    {
                        return NotFound("No project exists with that id");
                    }
                    entity.ProjectId = project.id;
                    entity.ProjectName = project.projectName;
                    entity.StartDate = project.startDate;
                    entity.EndDate = project.endDate;
                    await context.SaveChangesAsync();
                    return Ok("Updated");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("An error occured");
            }
        }

        [HttpDelete("{projectWithEmployees}")]
        public async Task<IActionResult> DeleteProject(int projectId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var entity = context.Projects.FirstOrDefault(x =>x.ProjectId == projectId);
                    if(entity == null)
                    {
                        return NotFound("Project id not found");
                    }
                    context.Projects.Remove(entity);
                    await context.SaveChangesAsync();
                    return Ok("Deleted Successfully");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Error occured");
            }
        }

        
    }
}