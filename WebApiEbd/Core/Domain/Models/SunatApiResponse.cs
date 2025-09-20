using System.Text.Json.Serialization;
namespace WebApiEbd.Core.Domain.Models;

public class SunatApiResponse
{
    [JsonPropertyName("razon_social")]
    public string RazonSocial { get; set; } = null!;

    [JsonPropertyName("numero_documento")]
    public string NumeroDocumento { get; set; } = null!;

    [JsonPropertyName("estado")]
    public string Estado { get; set; } = null!;

    [JsonPropertyName("direccion")]
    public string? Direccion { get; set; }

    [JsonPropertyName("distrito")]
    public string Distrito { get; set; } = null!;

    [JsonPropertyName("provincia")]
    public string Provincia { get; set; } = null!;
}
