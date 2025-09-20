
namespace WebApiEbd.Presentation.Api.Controllers.Device;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;

[Route("[controller]")]
[ApiController]
[Authorize]
public class DeviceController(IDeviceService service) : ControllerBase
{
    // GET: device/all
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var devices = await service.ListDevices();
        return Ok(devices);
    }

    // GET: device/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var device = await service.DeviceDetailById(id);
        return Ok(device);
    }

    // POST: device
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDeviceDto dto)
    {
        var created = await service.CreateDevice(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: device/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateDeviceDto dto)
    {
        var updated = await service.UpdateUserById(id, dto);
        return Ok(updated);
    }
}
