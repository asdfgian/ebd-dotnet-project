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
public class ProviderController(
    AppDbContext context,
    HttpClient httpClient,
    IConfiguration configuration) : ControllerBase
{

    // GET: api/provider/by-ruc?ruc=20601030013
    [HttpGet("by-ruc")]
    public async Task<ActionResult<ProviderApiDetailDto>> GetProviderByRuc([FromQuery] string ruc)
    {
        if (string.IsNullOrWhiteSpace(ruc))
            return BadRequest("El parámetro RUC es obligatorio");

        var baseUrl = configuration["ExternalApis:Sunat:BaseUrl"];
        var token = configuration["ExternalApis:Sunat:Token"];

        if (string.IsNullOrEmpty(baseUrl) || string.IsNullOrEmpty(token))
            return StatusCode(500, "Configuración de la API externa no encontrada");

        var url = $"{baseUrl}/ruc?numero={ruc}";

        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, "Error al consultar la API externa");

        var data = await response.Content.ReadFromJsonAsync<SunatApiResponse>();
        if (data == null)
            return NotFound("No se encontró información para el RUC especificado");

        var dto = new ProviderApiDetailDto(
            data.NumeroDocumento,
            data.RazonSocial,
            data.Direccion ?? string.Empty,
            data.Distrito,
            data.Provincia,
            data.Estado
        );

        return Ok(dto);
    }

    // GET: api/provider
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProviderListDto>>> GetAllProviders()
    {
        var providers = await context.Provider
            .AsNoTracking()
            .Select(p => new ProviderListDto(
                p.Ruc,
                p.Name,
                p.Email ?? string.Empty,
                p.Phone ?? string.Empty,
                p.Status
            ))
            .ToListAsync();

        return Ok(providers);
    }


    // GET: api/provider/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProviderDetailDto>> GetProviderById(int id)
    {
        var provider = await context.Provider
            .Where(p => p.Id == id)
            .Select(p => new ProviderDetailDto(
                p.Id,
                p.Ruc,
                p.Name,
                p.Address,
                p.District,
                p.Province,
                p.Department,
                p.Status,
                p.Email,
                p.Phone
            ))
            .FirstOrDefaultAsync();

        if (provider == null)
            return NotFound($"No se encontró el proveedor con id {id}");

        return Ok(provider);
    }

    // POST: api/provider
    [HttpPost]
    public async Task<ActionResult<ProviderDetailDto>> CreateProvider([FromBody] CreateProviderDto dto)
    {
        var provider = new Provider
        {
            Ruc = dto.Ruc,
            Name = dto.Name,
            Address = dto.Address,
            District = dto.District,
            Province = dto.Province,
            Department = dto.Department,
            Status = dto.Status,
            Email = dto.Email,
            Phone = dto.Phone
        };

        context.Provider.Add(provider);
        await context.SaveChangesAsync();

        var result = new ProviderDetailDto(
            provider.Id,
            provider.Ruc,
            provider.Name,
            provider.Address,
            provider.District,
            provider.Province,
            provider.Department,
            provider.Status,
            provider.Email,
            provider.Phone
        );

        return CreatedAtAction(nameof(GetProviderById), new { id = provider.Id }, result);
    }

    // PUT: api/provider/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProviderDetailDto>> UpdateProvider(int id, [FromBody] UpdateProviderDto dto)
    {
        var provider = await context.Provider.FindAsync(id);
        if (provider == null)
            return NotFound($"No se encontró el proveedor con id {id}");

        provider.Name = dto.Name ?? provider.Name;
        provider.Address = dto.Address ?? provider.Address;
        provider.District = dto.District ?? provider.District;
        provider.Province = dto.Province ?? provider.Province;
        provider.Department = dto.Department ?? provider.Department;
        provider.Status = dto.Status ?? provider.Status;
        provider.Email = dto.Email ?? provider.Email;
        provider.Phone = dto.Phone ?? provider.Phone;

        await context.SaveChangesAsync();

        var result = new ProviderDetailDto(
            provider.Id,
            provider.Ruc,
            provider.Name,
            provider.Address,
            provider.District,
            provider.Province,
            provider.Department,
            provider.Status,
            provider.Email,
            provider.Phone
        );

        return Ok(result);
    }
}