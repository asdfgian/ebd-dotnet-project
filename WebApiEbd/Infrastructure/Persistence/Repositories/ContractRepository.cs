using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories;

public class ContractRepository(AppDbContext ctx) : IContractRepository
{
    public async Task<Contract?> GetByIdAsync(int id)
    {
        return await ctx.Contract
            .AsNoTracking()
            .Include(c => c.Provider)
            .Include(c => c.User)
            .ThenInclude(u => u.Role)
            .Include(c => c.ContractsDevice)
            .ThenInclude(cd => cd.Device)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Contract>> GetAllAsync()
    {
        return await ctx.Contract
            .AsNoTracking()
            .Include(c => c.Provider)
            .Include(c => c.User)
            .ThenInclude(u => u.Role)
            .Include(c => c.ContractsDevice)
            .ThenInclude(cd => cd.Device)
            .ToListAsync();
    }

    public async Task<Contract> AddAsync(Contract contract)
    {
        ctx.Contract.Add(contract);
        await ctx.SaveChangesAsync();
        
        await ctx.Entry(contract)
            .Reference(c => c.Provider)
            .LoadAsync();
        await ctx.Entry(contract)
            .Reference(c => c.User)
            .LoadAsync();
        await ctx.Entry(contract.User!)
            .Reference(u => u.Role)
            .LoadAsync();

        return contract;
    }

    public async Task<Contract> UpdateAsync(Contract contract)
    {
        var existing = await ctx.Contract.FindAsync(contract.Id);
        if (existing is null)
            throw new KeyNotFoundException($"Contrato con id {contract.Id} no encontrado.");

        existing.Title = contract.Title;
        existing.StartDate = contract.StartDate;
        existing.EndDate = contract.EndDate;
        existing.Amount = contract.Amount;
        existing.ProviderId = contract.ProviderId;
        existing.Status = contract.Status;
        existing.Route = contract.Route;
        existing.OrderId = contract.OrderId;

        // Actualizar dispositivos
        if (contract.ContractsDevice.Count > 0)
        {
            var existingDevices = await ctx.ContractsDevice
                .Where(cd => cd.ContractId == contract.Id)
                .ToListAsync();
            
            ctx.ContractsDevice.RemoveRange(existingDevices);
            ctx.ContractsDevice.AddRange(contract.ContractsDevice);
        }

        ctx.Contract.Update(existing);
        await ctx.SaveChangesAsync();

        await ctx.Entry(existing)
            .Reference(c => c.Provider)
            .LoadAsync();
        await ctx.Entry(existing)
            .Reference(c => c.User)
            .LoadAsync();

        return existing;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var contract = await ctx.Contract.FindAsync(id);
        if (contract is not null)
        {
            ctx.Contract.Remove(contract);
            await ctx.SaveChangesAsync();
        }
    }
}
