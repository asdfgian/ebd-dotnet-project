using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiEbd.Apps.Api.Controllers.Test
{
    [ApiController]
    public class HelloWorld : ControllerBase
    {
        [HttpGet("hello-world")]
        [ProducesResponseType<int>(StatusCodes.Status200OK)]
        [AllowAnonymous]
        public IActionResult GetPublic()
        {
            return Ok(new { Message = "Hello World!" });
        }

    }
}