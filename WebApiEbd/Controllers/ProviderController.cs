using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiEbd.Context;
using WebApiEbd.Dto;
using WebApiEbd.Models.Api;

namespace WebApiEbd.Controllers;

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

}