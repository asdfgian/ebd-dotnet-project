using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In
{
    public interface IProviderService
    {
        Task<IEnumerable<ProviderListDto>> ListProviders();

        Task<ProviderDetailDto> ProviderById(int id);

        Task<ProviderDetailDto> CreateProvider(CreateProviderDto dto);

        Task<ProviderDetailDto> UpdateProviderById(int id, UpdateProviderDto dto);
    }
}