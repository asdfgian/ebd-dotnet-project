using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;

namespace WebApiEbd.Presentation.Api.Controllers.Provider;

[Route("[controller]")]
[ApiController]
[Authorize]
public class ProviderController(
    IProviderService service,
    IDecolectaApiService apiService) : ControllerBase
{

    // GET: provider/by-ruc?ruc=20601030013
    [HttpGet("by-ruc")]
    public async Task<ActionResult<ProviderApiDetailDto>> GetProviderByRuc([FromQuery] string ruc)
    {
        if (string.IsNullOrWhiteSpace(ruc))
            return BadRequest("El parámetro RUC es obligatorio");

        var decoletaDto = await apiService.ProviderDetail(ruc);

        var dto = new ProviderApiDetailDto(
            decoletaDto.Ruc,
            decoletaDto.SocialReason,
            decoletaDto.Address ?? string.Empty,
            decoletaDto.District,
            decoletaDto.Province,
            decoletaDto.Status
        );

        return Ok(dto);
    }

    // GET: provider/all
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<ProviderListDto>>> GetAllProviders()
    {
        var providers = await service.ListProviders();
        return Ok(providers);
    }


    // GET: provider/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ProviderDetailDto>> GetProviderById(int id)
    {
        var provider = await service.ProviderById(id);

        return Ok(provider);
    }

    // POST: provider
    [HttpPost]
    public async Task<ActionResult<ProviderDetailDto>> CreateProvider([FromBody] CreateProviderDto dto)
    {
        var provider = await service.CreateProvider(dto);


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

    // PUT: provider/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<ProviderDetailDto>> UpdateProvider(int id, [FromBody] UpdateProviderDto dto)
    {
        var provider = await service.UpdateProviderById(id, dto);

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