using Microsoft.EntityFrameworkCore;
using WebApiEbd.Application.Ports.Out;
using WebApiEbd.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories
{
    public class UserAuthRepository(AppDbContext ctx) : IUserAuthRepository
    {
        public async Task Add(User user)
        {
            ctx.User.Add(user);
            await ctx.SaveChangesAsync();
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await ctx.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await ctx.User.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}