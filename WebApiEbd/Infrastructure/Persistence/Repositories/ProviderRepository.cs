using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories
{
    public class ProviderRepository(AppDbContext ctx) : IProviderRepository
    {
        private readonly AppDbContext ctx = ctx;

        public async Task<Provider?> AddAsync(Provider provider)
        {
            var exists = await ctx.Provider
                .AnyAsync(p => p.Name == provider.Name);

            if (exists)
            {
                throw new InvalidOperationException(
                    $"Ya existe un proveedor registrado con el nombre '{provider.Name}'."
                );
            }

            ctx.Provider.Add(provider);
            await ctx.SaveChangesAsync();

            return provider;
        }

        public async Task<IEnumerable<Provider>> GetAllAsync()
        {
            return await ctx.Provider.AsNoTracking()
                .ToListAsync();
        }

        public async Task<Provider?> GetByIdAsync(int id)
        {
            return await ctx.Provider
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Provider?> UpdateAsync(Provider provider)
        {
            var existing = await ctx.Provider.FindAsync(provider.Id);
            if (existing is null)
                return null;

            existing.Name = provider.Name;
            existing.Phone = provider.Phone;
            existing.Email = provider.Email;
            existing.Address = provider.Address;

            ctx.Provider.Update(existing);
            await ctx.SaveChangesAsync();

            return existing;
        }
    }
}
