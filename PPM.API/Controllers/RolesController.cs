using System.Net;
using Microsoft.AspNetCore.Mvc;
using PPM.DAl;
using PPM.DAl.Models;

namespace PPM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        [HttpGet(Name = "GetRoles")]
        public IActionResult GetRoles()
        {
            using (ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
            {
                var roles = context.Roles.ToList();
                return Ok(roles);
            }
        }
        

        [HttpGet("{roleId}", Name = "GetRoleById")]
        public IActionResult GetRoleById(int roleId)
        {
            using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
            {
                var role = context.Roles.FirstOrDefault(x => x.Roleid == roleId);
                if(role == null)
                {
                    return NotFound("No Role exists with this id");
                }
                return Ok(role);
            }
        }

        [HttpPost()]
        public async Task<IActionResult> PostRoles([FromBody] Role role)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    if (role.Roleid != null)
                    {
                        if (string.IsNullOrEmpty(role.RoleName))
                        {
                            return BadRequest("Role Name cannot be empty");
                        }

                        foreach (Role role1 in context.Roles)
                        {
                            if (role1.Roleid == role.Roleid)
                            {
                                return BadRequest("Role id already exists");
                            }
                        }
                        context.Roles.Add(role);
                        await context.SaveChangesAsync();
                        return CreatedAtAction("GetRoleById", new { RoleId = role.Roleid }, role);
                    }
                    return BadRequest("Role Id already exists");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Unkown error occured");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutRole([FromBody] Role role)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                var entity = context.Roles.FirstOrDefault(x => x.Roleid == role.Roleid);
                if(entity == null)
                {
                    return NotFound("No role exists with that id");
                }
                entity.Roleid = role.Roleid;
                entity.RoleName = role.RoleName;
                await context.SaveChangesAsync();
                return Ok("Updated successfully");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }

        [HttpDelete("{roleId}")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            try
            {
                using(ProlificsProjectManagementEntities context = new ProlificsProjectManagementEntities())
                {
                    var role = context.Roles.FirstOrDefault(x => x.Roleid == roleId);
                    if(role == null)
                    {
                        return NotFound("No roles exists with that id");
                    }
                    context.Remove(role);
                    await context.SaveChangesAsync();
                    return Ok("Deleted Successfully");
                }
            }
            catch(Exception ex)
            {
                return BadRequest("Something went wrong");
            }
        }
    }
}