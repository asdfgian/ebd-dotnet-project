using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories
{
    public class DeviceRepository(AppDbContext ctx) : IDeviceRepository
    {
        public async Task<Device> AddAsync(Device device)
        {
            var exists = await ctx.Device.AnyAsync(d => d.SerialNumber == device.SerialNumber);
            if (exists)
            {
                throw new InvalidOperationException($"Ya existe un dispositivo con SerialNumber {device.SerialNumber}");
            }

            ctx.Device.Add(device);
            await ctx.SaveChangesAsync();
            return device;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var device = await ctx.Device.FindAsync(id) ?? throw new KeyNotFoundException($"No se encontró el dispositivo con id {id}");
            ctx.Device.Remove(device);
            await ctx.SaveChangesAsync();
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await ctx.Device
                .AsNoTracking()
                .Include(d => d.Brand)
                .ToListAsync();

        }

        public async Task<Device?> GetByIdAsync(int id)
        {
            return await ctx.Device
                .AsNoTracking()
                .Include(d => d.Brand)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<Device> UpdateAsync(Device device)
        {
            var existing = await ctx.Device.FindAsync(device.Id) ?? throw new KeyNotFoundException($"No se encontró el dispositivo con id {device.Id}");
            existing.Name = device.Name;
            existing.SerialNumber = device.SerialNumber;
            existing.BrandId = device.BrandId;
            existing.UpdatedAt = DateTime.UtcNow;

            ctx.Device.Update(existing);
            await ctx.SaveChangesAsync();

            return existing;
        }
    }
}