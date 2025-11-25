using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;

namespace WebApiEbd.Presentation.Api.Controllers.CountryOrigin;

[Route("[controller]")]
[ApiController]
[Authorize]
public class CountryOriginController(ICountryOriginService service) : ControllerBase
{
    // GET: countryorigin/all
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var countries = await service.ListCountries();
        return Ok(countries);
    }

    // GET: countryorigin/{id}
    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var country = await service.CountryById(id);
        return Ok(country);
    }

    // POST: countryorigin
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCountryOriginDto dto)
    {
        var created = await service.CreateCountry(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    // PUT: countryorigin/{id}
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateCountryOriginDto dto)
    {
        var updated = await service.UpdateCountryById(id, dto);
        return Ok(updated);
    }

    // DELETE: countryorigin/{id}
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await service.DeleteCountryById(id);
        return NoContent();
    }
}
