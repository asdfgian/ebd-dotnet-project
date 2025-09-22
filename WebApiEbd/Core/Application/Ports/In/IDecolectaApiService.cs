using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In;

public interface IDecolectaApiService
{
    Task<DecolectaDto> ProviderDetail(string ruc);
}