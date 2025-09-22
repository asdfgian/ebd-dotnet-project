using System.Text.Json.Serialization;
namespace WebApiEbd.Core.Application.Dtos;

public class DecolectaDto
{
    [JsonPropertyName("razon_social")]
    public string SocialReason { get; set; } = null!;

    [JsonPropertyName("numero_documento")]
    public string Ruc { get; set; } = null!;

    [JsonPropertyName("estado")]
    public string Status { get; set; } = null!;

    [JsonPropertyName("direccion")]
    public string? Address { get; set; }

    [JsonPropertyName("distrito")]
    public string District { get; set; } = null!;

    [JsonPropertyName("provincia")]
    public string Province { get; set; } = null!;
}
