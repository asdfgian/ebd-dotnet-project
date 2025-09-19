using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Application.Dtos;
using WebApiEbd.Application.Ports.In;

namespace WebApiEbd.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IUserAuthService service) : ControllerBase
{

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] RegisterDto dto)
    {
        var res = await service.SignUp(dto);
        if (!res.Success) return BadRequest(res.Error);
        return Ok(new { token = res.Token });
    }


    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] LoginDto dto)
    {
        var res = await service.SignIn(dto.Identifier, dto.Password);
        if (!res.Success) return Unauthorized(res.Error);
        return Ok(new { token = res.Token });
    }

    [HttpPost("sign-out")]
    [Authorize]
    public new IActionResult SignOut()
    {
        return Ok(new { message = "Sesión cerrada correctamente" });
    }

}