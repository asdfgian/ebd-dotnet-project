using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Services
{
    public class ProviderService(
        IProviderRepository repository) : IProviderService
    {
        public async Task<ProviderDetailDto> CreateProvider(CreateProviderDto dto)
        {
            var provider = new Provider()
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
            await repository.AddAsync(provider);
            return new ProviderDetailDto(
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
        }

        public async Task<IEnumerable<ProviderListDto>> ListProviders()
        {
            var providers = await repository.GetAllAsync();
            return providers.Select(p => new ProviderListDto(
                p.Ruc,
                p.Name,
                p.Email ?? string.Empty,
                p.Phone ?? string.Empty,
                p.Status
            ));
        }

        public async Task<ProviderDetailDto> ProviderById(int id)
        {
            var provider = await repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Proveedor con id {id} no encontrado.");
            return new ProviderDetailDto(
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
        }

        public Task<ProviderDetailDto> UpdateProviderById(int id, UpdateProviderDto dto)
        {
            throw new NotImplementedException();
        }
    }
}