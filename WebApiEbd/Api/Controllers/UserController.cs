using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEbd.Application.Dtos;
using WebApiEbd.Application.Ports.In;

namespace WebApiEbd.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(IUserService service) : ControllerBase
{
    // GET: api/user/all
    [HttpGet("all")]
    public async Task<ActionResult> GetAll()
    {
        var users = await service.ListUsers();
        return Ok(users);
    }

    // GET: api/user/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult> GetById(int id)
    {
        var user = await service.UserDetailById(id);

        if (user == null)
            return NotFound($"No se encontró el usuario con id {id}");

        return Ok(user);
    }

    // PUT: api/user/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult> Update(int id, [FromBody] UpdateUserDto dto)
    {
        var user = await service.UpdateUserById(id, dto);
        return Ok(user);
    }

}