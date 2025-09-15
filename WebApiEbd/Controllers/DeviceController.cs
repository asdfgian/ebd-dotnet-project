using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Context;

namespace WebApiEbd.Controllers;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEbd.Dto;
using WebApiEbd.Models;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DeviceController(AppDbContext context) : ControllerBase
{

    // GET: api/device
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DeviceListDto>>> GetAll()
    {
        var devices = await context.Device
            .AsNoTracking()
            .Include(d => d.Brand)
            .Select(d => new DeviceListDto(
                d.Id,
                d.Name,
                d.Price,
                d.Status,
                d.Brand.Name
            ))
            .ToListAsync();

        return Ok(devices);
    }

    // GET: api/device/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<DeviceDetailDto>> GetById(int id)
    {
        var device = await context.Device
            .AsNoTracking()
            .Include(d => d.Brand)
            .Where(d => d.Id == id)
            .Select(d => new DeviceDetailDto(
                d.Id,
                d.Name,
                d.Description,
                d.Price,
                d.Model,
                d.SerialNumber,
                d.Status,
                d.CreatedAt,
                d.UpdatedAt,
                new BrandDto(d.Brand.Id, d.Brand.Name, d.Brand.CountryOrigin.Name)
            ))
            .FirstOrDefaultAsync();

        if (device == null)
            return NotFound($"No se encontró el dispositivo con id {id}");

        return Ok(device);
    }

    // POST: api/device
    [HttpPost]
    public async Task<ActionResult<DeviceDetailDto>> Create([FromBody] CreateDeviceDto dto)
    {
        if (await context.Device.AnyAsync(d => d.SerialNumber == dto.SerialNumber))
            return Conflict($"Ya existe un dispositivo con serial '{dto.SerialNumber}'");

        var device = new Device
        {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            Model = dto.Model,
            SerialNumber = dto.SerialNumber,
            Status = dto.Status,
            BrandId = dto.BrandId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        context.Device.Add(device);
        await context.SaveChangesAsync();

        var created = await context.Device
            .Include(d => d.Brand)
            .Where(d => d.Id == device.Id)
            .Select(d => new DeviceDetailDto(
                d.Id,
                d.Name,
                d.Description,
                d.Price,
                d.Model,
                d.SerialNumber,
                d.Status,
                d.CreatedAt,
                d.UpdatedAt,
                new BrandDto(d.Brand.Id, d.Brand.Name, d.Brand.CountryOrigin.Name)
            ))
            .FirstAsync();

        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: api/device/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<DeviceDetailDto>> Update(int id, [FromBody] UpdateDeviceDto dto)
    {
        var device = await context.Device.FindAsync(id);
        if (device == null)
            return NotFound($"No se encontró el dispositivo con id {id}");

        device.Name = dto.Name;
        device.Description = dto.Description;
        device.Price = dto.Price;
        device.Model = dto.Model;
        device.Status = dto.Status;
        device.BrandId = dto.BrandId;
        device.UpdatedAt = DateTime.UtcNow;

        context.Device.Update(device);
        await context.SaveChangesAsync();

        var updated = await context.Device
            .Include(d => d.Brand)
            .Where(d => d.Id == device.Id)
            .Select(d => new DeviceDetailDto(
                d.Id,
                d.Name,
                d.Description,
                d.Price,
                d.Model,
                d.SerialNumber,
                d.Status,
                d.CreatedAt,
                d.UpdatedAt,
                new BrandDto(d.Brand.Id, d.Brand.Name, d.Brand.CountryOrigin.Name)
            ))
            .FirstAsync();

        return Ok(updated);
    }

    // DELETE: api/device/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var device = await context.Device.FindAsync(id);
        if (device == null)
            return NotFound($"No se encontró el dispositivo con id {id}");

        context.Device.Remove(device);
        await context.SaveChangesAsync();

        return NoContent();
    }
}
