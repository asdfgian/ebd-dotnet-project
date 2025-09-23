using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;

namespace WebApiEbd.Presentation.Api.Controllers.Movement
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class MovementController(IMovementService service) : ControllerBase
    {
        // GET: movement/{id}
        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetById(int id)
        {
            var movements = await service.ListMovementsByDeviceId(id);
            return Ok(movements);
        }

        // POST: movement
        [HttpPost]
        public async Task<ActionResult> Add([FromBody] MovementRequestDto dto)
        {
            var movement = await service.AddMovement(dto);
            return Ok(movement);
        }

    }
}