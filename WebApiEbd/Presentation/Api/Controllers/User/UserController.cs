using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;

namespace WebApiEbd.Presentation.Api.Controllers.User;

[Route("[controller]")]
[ApiController]
[Authorize]
public class UserController(IUserService service) : ControllerBase
{
    // GET: user/all
    [HttpGet("all")]
    public async Task<ActionResult> GetAll()
    {
        var users = await service.ListUsers();
        return Ok(users);
    }

    // GET: user/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDetailDto>> GetById(int id)
    {
        var user = await service.UserDetailById(id);
        return Ok(user);
    }

    // PUT: user/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserDetailDto>> Update(int id, [FromBody] UpdateUserDto dto)
    {
        var user = await service.UpdateUserById(id, dto);
        return Ok(user);
    }

}