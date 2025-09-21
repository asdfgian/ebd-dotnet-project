using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories
{
    public class BrandRepository(AppDbContext ctx) : IBrandRepository
    {
        public async Task<Brand?> AddAsync(Brand brand)
        {
            var exists = await ctx.Brand
                .AnyAsync(b => b.Name == brand.Name);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe una marca registrada con el nombre '{brand.Name}'."
                );
            }

            ctx.Brand.Add(brand);
            await ctx.SaveChangesAsync();

            await ctx.Entry(brand)
                .Reference(b => b.CountryOrigin)
                .LoadAsync();

            return brand;
        }


        public async Task<IEnumerable<Brand>> GetAllAsync()
        {
            return await ctx.Brand
                .AsNoTracking()
                .Include(b => b.CountryOrigin)
                .ToListAsync();
        }
        public async Task<Brand?> GetByIdAsync(int id)
        {
            return await ctx.Brand
                .AsNoTracking()
                .Include(b => b.CountryOrigin)
                .FirstOrDefaultAsync(b => b.Id == id);
        }
        public async Task<Brand?> UpdateAsync(Brand brand)
        {
            var existing = await ctx.Brand.FindAsync(brand.Id);
            if (existing is null)
                return null;

            existing.Name = brand.Name;
            existing.CountryOriginId = brand.CountryOriginId;

            ctx.Brand.Update(existing);
            await ctx.SaveChangesAsync();

            await ctx.Entry(existing)
                .Reference(b => b.CountryOrigin)
                .LoadAsync();
            return existing;
        }
    }
}