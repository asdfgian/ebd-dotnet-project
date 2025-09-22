using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;

namespace WebApiEbd.Presentation.Api.Controllers.Brand;

[Route("[controller]")]
[ApiController]
[Authorize]
public class BrandController(IBrandService service) : ControllerBase
{
    // GET: brand/all
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var brands = await service.ListBrands();
        return Ok(brands);
    }

    // GET: brand/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var brand = await service.BrandById(id);
        return Ok(brand);
    }

    // POST: brand
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateBrandDto dto)
    {
        var created = await service.CreateBrand(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: brand/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateBrandDto dto)
    {
        var updated = await service.UpdateBrandById(id, dto);
        return Ok(updated);
    }
}