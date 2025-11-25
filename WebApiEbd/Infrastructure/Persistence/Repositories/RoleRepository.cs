using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories;

public class RoleRepository(AppDbContext ctx) : IRoleRepository
{
    public async Task<Role?> GetByIdAsync(int id)
    {
        return await ctx.Role
            .AsNoTracking()
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return await ctx.Role
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Role> AddAsync(Role role)
    {
        ctx.Role.Add(role);
        await ctx.SaveChangesAsync();
        return role;
    }

    public async Task<Role> UpdateAsync(Role role)
    {
        var existing = await ctx.Role.FindAsync(role.Id);
        if (existing is null)
            throw new KeyNotFoundException($"Rol con id {role.Id} no encontrado.");

        existing.Name = role.Name;
        existing.Description = role.Description;
        ctx.Role.Update(existing);
        await ctx.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var role = await ctx.Role.FindAsync(id);
        if (role is not null)
        {
            ctx.Role.Remove(role);
            await ctx.SaveChangesAsync();
        }
    }
}
