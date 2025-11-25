using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories;

public class CountryOriginRepository(AppDbContext ctx) : ICountryOriginRepository
{
    public async Task<CountryOrigin?> GetByIdAsync(int id)
    {
        return await ctx.CountryOrigin
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<CountryOrigin>> GetAllAsync()
    {
        return await ctx.CountryOrigin
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CountryOrigin> AddAsync(CountryOrigin countryOrigin)
    {
        ctx.CountryOrigin.Add(countryOrigin);
        await ctx.SaveChangesAsync();
        return countryOrigin;
    }

    public async Task<CountryOrigin> UpdateAsync(CountryOrigin countryOrigin)
    {
        var existing = await ctx.CountryOrigin.FindAsync(countryOrigin.Id);
        if (existing is null)
            throw new KeyNotFoundException($"País con id {countryOrigin.Id} no encontrado.");

        existing.Name = countryOrigin.Name;
        ctx.CountryOrigin.Update(existing);
        await ctx.SaveChangesAsync();
        return existing;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var country = await ctx.CountryOrigin.FindAsync(id);
        if (country is not null)
        {
            ctx.CountryOrigin.Remove(country);
            await ctx.SaveChangesAsync();
        }
    }
}
