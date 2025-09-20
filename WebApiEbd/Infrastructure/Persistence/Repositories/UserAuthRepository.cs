using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories
{
    public class UserAuthRepository(AppDbContext ctx) : IUserAuthRepository
    {
        public async Task AddAsync(User user)
        {
            ctx.User.Add(user);
            await ctx.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await ctx.User.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await ctx.User.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}