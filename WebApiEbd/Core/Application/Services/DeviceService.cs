using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Services
{
    public class DeviceService(IDeviceRepository repository) : IDeviceService
    {
        public async Task<DeviceDetailDto> CreateDevice(CreateDeviceDto dto)
        {
            var device = new Device
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                Model = dto.Model,
                SerialNumber = dto.SerialNumber,
                Status = dto.Status,
                BrandId = dto.BrandId,
                CreatedAt = DateTime.UtcNow
            };

            var created = await repository.AddAsync(device);

            return new DeviceDetailDto(
                created.Id,
                created.Name,
                created.Description,
                created.Price,
                created.Model,
                created.SerialNumber,
                created.Status,
                created.CreatedAt,
                created.UpdatedAt,
                new BrandDto(created.Brand.Id, created.Brand.Name, created.Brand.CountryOrigin.Name)
            );
        }

        public async Task<DeviceDetailDto> DeviceDetailById(int id)
        {
            var device = await repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Dispositivo con id {id} no encontrado.");
            return new DeviceDetailDto(
                device.Id,
                device.Name,
                device.Description,
                device.Price,
                device.Model,
                device.SerialNumber,
                device.Status,
                device.CreatedAt,
                device.UpdatedAt,
                new BrandDto(device.Brand.Id, device.Brand.Name, device.Brand.CountryOrigin.Name)
            );
        }

        public async Task<IEnumerable<DeviceListDto>> ListDevices()
        {
            var devices = await repository.GetAllAsync();

            return devices.Select(d => new DeviceListDto(
                d.Id,
                d.Name,
                d.Price,
                d.Status,
                d.Brand.Name
            ));
        }

        public async Task<DeviceDetailDto> UpdateUserById(int id, UpdateDeviceDto dto)
        {
            var existing = await repository.GetByIdAsync(id) ?? throw new KeyNotFoundException($"Dispositivo con id {id} no encontrado.");
            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Model = dto.Model;
            existing.Status = dto.Status;
            existing.BrandId = dto.BrandId;
            existing.UpdatedAt = DateTime.UtcNow;

            var updated = await repository.UpdateAsync(existing);

            return new DeviceDetailDto(
                updated.Id,
                updated.Name,
                updated.Description,
                updated.Price,
                updated.Model,
                updated.SerialNumber,
                updated.Status,
                updated.CreatedAt,
                updated.UpdatedAt,
                new BrandDto(updated.Brand.Id, updated.Brand.Name, updated.Brand.CountryOrigin.Name)
            );
        }
    }
}