using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories
{
    public class UserRepository(AppDbContext ctx) : IUserRepository
    {
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await ctx.User
                .AsNoTracking()
                .Include(u => u.Role)
                .ToListAsync();
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            return await ctx.User
                .AsNoTracking()
                .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User?> GetByIdAsyncTracked(int id)
        {
            return await ctx.User
                .Include(u => u.Role)
                .Include(u => u.Department)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(User user)
        {
            ctx.User.Update(user);
            await ctx.SaveChangesAsync();
        }
    }
}