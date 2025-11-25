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
                p.Id,
                p.Ruc,
                p.Name,
                p.Email ?? string.Empty,
                p.Phone ?? string.Empty,
                p.Status
            ));
        }

        public async Task<ProviderDetailDto> ProviderById(int id)
        {
            var provider = await repository.GetByIdAsync(id) ??
                           throw new KeyNotFoundException($"Proveedor con id {id} no encontrado.");
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

    public async Task<ProviderDetailDto> UpdateProviderById(int id, UpdateProviderDto dto)
    {
        var provider = await repository.GetByIdAsync(id) ??
                       throw new KeyNotFoundException($"Proveedor con id {id} no encontrado.");

        if (!string.IsNullOrWhiteSpace(dto.Name))
            provider.Name = dto.Name;

        if (!string.IsNullOrWhiteSpace(dto.Address))
            provider.Address = dto.Address;

        if (!string.IsNullOrWhiteSpace(dto.District))
            provider.District = dto.District;

        if (!string.IsNullOrWhiteSpace(dto.Province))
            provider.Province = dto.Province;

        if (!string.IsNullOrWhiteSpace(dto.Department))
            provider.Department = dto.Department;

        if (!string.IsNullOrWhiteSpace(dto.Status))
            provider.Status = dto.Status;

        if (!string.IsNullOrWhiteSpace(dto.Email))
            provider.Email = dto.Email;

        if (!string.IsNullOrWhiteSpace(dto.Phone))
            provider.Phone = dto.Phone;

        var updated = await repository.UpdateAsync(provider) ??
                      throw new InvalidOperationException("No se pudo actualizar el proveedor.");

        return new ProviderDetailDto(
            updated.Id,
            updated.Ruc,
            updated.Name,
            updated.Address,
            updated.District,
            updated.Province,
            updated.Department,
            updated.Status,
            updated.Email,
            updated.Phone
        );
    }
    }
}