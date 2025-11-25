using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Core.Application.Ports.In;

namespace WebApiEbd.Presentation.Api.Controllers.Role;

[Route("[controller]")]
[ApiController]
[Authorize]
public class RoleController(IRoleService service) : ControllerBase
{
    // GET: role/all
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var roles = await service.ListRoles();
        return Ok(roles);
    }

    // GET: role/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var role = await service.RoleById(id);
        return Ok(role);
    }

    // POST: role
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequest request)
    {
        var created = await service.CreateRole(request.Name, request.Description);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: role/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateRoleRequest request)
    {
        var updated = await service.UpdateRoleById(id, request.Name, request.Description);
        return Ok(updated);
    }

    // DELETE: role/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteRoleById(id);
        return NoContent();
    }
}

public record CreateRoleRequest(string Name, string Description);
public record UpdateRoleRequest(string Name, string Description);
