using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;

namespace WebApiEbd.Core.Application.Services;

public class DecolectaApiService(IDecolectaApiProvider provider) : IDecolectaApiService
{
    public async Task<DecolectaDto> ProviderDetail(string ruc)
    {
        var response = await provider.GetDetailAsync(ruc);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException(
                $"Error al consultar el proveedor con RUC {ruc}. " +
                $"Código: {(int)response.StatusCode}, Detalle: {errorContent}");
        }

        var data = await response.Content.ReadFromJsonAsync<DecolectaDto>();

        if (data == null)
        {
            throw new InvalidOperationException(
                $"La API devolvió una respuesta vacía o inválida para el RUC {ruc}.");
        }

        return data;
    }

}