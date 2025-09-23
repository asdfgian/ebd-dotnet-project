using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories
{
    public class MovementRepository(AppDbContext ctx) : IMovementRepository
    {
        public async Task<Movement> AddAsync(Movement movement)
        {
            ctx.Movement.Add(movement);
            await ctx.SaveChangesAsync();
            await ctx.Entry(movement).Reference(m => m.Device).LoadAsync();
            await ctx.Entry(movement).Reference(m => m.CreatedByNavigation).LoadAsync();
            await ctx.Entry(movement).Reference(m => m.UserOrigin).LoadAsync();
            await ctx.Entry(movement).Reference(m => m.UserDestination).LoadAsync();

            return movement;
        }

        public async Task<IEnumerable<Movement>> GetMovementsByDeviceIdAsync(int deviceId)
        {
            return await ctx.Movement
                .AsNoTracking()
                .Where(m => m.DeviceId == deviceId)
                .Include(m => m.Device)
                .Include(m => m.CreatedByNavigation)
                .Include(m => m.UserOrigin)
                .Include(m => m.UserDestination)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }
    }
}