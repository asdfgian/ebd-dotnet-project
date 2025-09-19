using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEbd.Application.Dtos;
using WebApiEbd.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class BrandController(AppDbContext context) : ControllerBase
{
    // GET: api/brand/all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<BrandDto>>> GetAll()
    {
        var brands = await context.Brand
            .AsNoTracking()
            .Include(b => b.CountryOrigin)
            .Select(b => new BrandDto(b.Id, b.Name, b.CountryOrigin.Name))
            .ToListAsync();

        return Ok(brands);
    }

    // GET: api/brand/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<BrandDto>> GetById(int id)
    {
        var brand = await context.Brand
            .AsNoTracking()
            .Include(b => b.CountryOrigin)
            .Where(b => b.Id == id)
            .Select(b => new BrandDto(b.Id, b.Name, b.CountryOrigin.Name))
            .FirstOrDefaultAsync();

        if (brand == null)
            return NotFound($"No se encontró la marca con id {id}");

        return Ok(brand);
    }

    // POST: api/brand
    [HttpPost]
    public async Task<ActionResult<BrandDto>> Create([FromBody] CreateBrandDto dto)
    {
        if (await context.Brand.AnyAsync(b => b.Name == dto.Name))
            return Conflict($"Ya existe una marca con el nombre '{dto.Name}'");

        var brand = new Brand
        {
            Name = dto.Name,
            CountryOriginId = dto.CountryOriginId
        };

        context.Brand.Add(brand);
        await context.SaveChangesAsync();

        var countryName = await context.CountryOrigin
            .Where(c => c.Id == dto.CountryOriginId)
            .Select(c => c.Name)
            .FirstOrDefaultAsync();

        var result = new BrandDto(brand.Id, brand.Name, countryName!);

        return CreatedAtAction(nameof(GetById), new { id = brand.Id }, result);
    }


    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBrandDto dto)
    {
        var brand = await context.Brand.FindAsync(id);
        if (brand == null)
            return NotFound($"No se encontró la marca con id {id}");

        if (await context.Brand.AnyAsync(b => b.Name == dto.Name && b.Id != id))
            return Conflict($"Ya existe una marca con el nombre '{dto.Name}'");

        brand.Name = dto.Name;
        brand.CountryOriginId = dto.CountryOriginId;

        context.Brand.Update(brand);
        await context.SaveChangesAsync();

        return NoContent();
    }

}