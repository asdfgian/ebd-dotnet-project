using Microsoft.EntityFrameworkCore;
using WebApiEbd.Application.Ports.Out;
using WebApiEbd.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories
{
    public class UserRepository(AppDbContext ctx) : IUserRepository
    {
        public async Task<IEnumerable<User>> GetAll()
        {
            return await ctx.User.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetById(int id)
        {
            return await ctx.User.FindAsync(id);
        }

        public async Task Update(User user)
        {
            ctx.User.Update(user);
            await ctx.SaveChangesAsync();
        }
    }
}