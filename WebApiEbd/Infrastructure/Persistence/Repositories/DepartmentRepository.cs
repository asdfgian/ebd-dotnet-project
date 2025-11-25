using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories;

public class DepartmentRepository(AppDbContext ctx) : IDepartmentRepository
{
    public async Task<Department?> GetByIdAsync(int id)
    {
        return await ctx.Department
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await ctx.Department
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Department> AddAsync(Department department)
    {
        ctx.Department.Add(department);
        await ctx.SaveChangesAsync();
        return department;
    }

    public async Task<Department> UpdateAsync(Department department)
    {
        var existing = await ctx.Department.FindAsync(department.Id);
        if (existing is null)
            throw new KeyNotFoundException($"Departamento con id {department.Id} no encontrado.");

        existing.Name = department.Name;
        ctx.Department.Update(existing);
        await ctx.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var department = await ctx.Department.FindAsync(id);
        if (department is not null)
        {
            ctx.Department.Remove(department);
            await ctx.SaveChangesAsync();
        }
    }
}
