using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Core.Application.Ports.In;

namespace WebApiEbd.Presentation.Api.Controllers.Department;

[Route("[controller]")]
[ApiController]
[Authorize]
public class DepartmentController(IDepartmentService service) : ControllerBase
{
    // GET: department/all
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var departments = await service.ListDepartments();
        return Ok(departments);
    }

    // GET: department/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var department = await service.DepartmentById(id);
        return Ok(department);
    }

    // POST: department
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDepartmentRequest request)
    {
        var created = await service.CreateDepartment(request.Name);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: department/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDepartmentRequest request)
    {
        var updated = await service.UpdateDepartmentById(id, request.Name);
        return Ok(updated);
    }

    // DELETE: department/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteDepartmentById(id);
        return NoContent();
    }
}

public record CreateDepartmentRequest(string Name);
public record UpdateDepartmentRequest(string Name);
