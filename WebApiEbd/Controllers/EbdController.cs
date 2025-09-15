using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiEbd.Controllers;

[Route("api")]
[ApiController]
public class EbdController : ControllerBase
{
    [HttpGet("hello-world")]
    [AllowAnonymous]
    public IActionResult GetPublic()
    {
        return Ok(new { Message = "Hello World!" });
    }
}

