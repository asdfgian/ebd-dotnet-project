using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;

namespace WebApiEbd.Presentation.Api.Controllers.Auth
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController(IUserAuthService service) : ControllerBase
    {
        // POST: auth/sign-in
        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] LoginDto dto)
        {
            var res = await service.SignIn(dto.Identifier, dto.Password);
            if (!res.Success) return Unauthorized(res.Error);
            return Ok(new { token = res.Token });
        }

        // POST: auth/sign-up
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] RegisterDto dto)
        {
            var res = await service.SignUp(dto);
            if (!res.Success) return BadRequest(res.Error);
            return Ok(new { token = res.Token });
        }
    }
}