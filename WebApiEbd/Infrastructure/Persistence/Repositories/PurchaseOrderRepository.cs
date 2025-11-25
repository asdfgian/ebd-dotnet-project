using Microsoft.EntityFrameworkCore;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;
using WebApiEbd.Infrastructure.Persistence.Context;

namespace WebApiEbd.Infrastructure.Persistence.Repositories;

public class PurchaseOrderRepository(AppDbContext ctx) : IPurchaseOrderRepository
{
    public async Task<PurchaseOrder?> GetByIdAsync(int id)
    {
        return await ctx.PurchaseOrder
            .AsNoTracking()
            .Include(po => po.Provider)
            .Include(po => po.User)
            .ThenInclude(u => u.Role)
            .Include(po => po.PurchaseOrderDevice)
            .ThenInclude(pod => pod.Product)
            .FirstOrDefaultAsync(po => po.Id == id);
    }

    public async Task<IEnumerable<PurchaseOrder>> GetAllAsync()
    {
        return await ctx.PurchaseOrder
            .AsNoTracking()
            .Include(po => po.Provider)
            .Include(po => po.User)
            .ThenInclude(u => u.Role)
            .Include(po => po.PurchaseOrderDevice)
            .ThenInclude(pod => pod.Product)
            .ToListAsync();
    }

    public async Task<PurchaseOrder> AddAsync(PurchaseOrder purchaseOrder)
    {
        ctx.PurchaseOrder.Add(purchaseOrder);
        await ctx.SaveChangesAsync();

        await ctx.Entry(purchaseOrder)
            .Reference(po => po.Provider)
            .LoadAsync();
        await ctx.Entry(purchaseOrder)
            .Reference(po => po.User)
            .LoadAsync();

        return purchaseOrder;
    }

    public async Task<PurchaseOrder> UpdateAsync(PurchaseOrder purchaseOrder)
    {
        var existing = await ctx.PurchaseOrder.FindAsync(purchaseOrder.Id);
        if (existing is null)
            throw new KeyNotFoundException($"Orden de compra con id {purchaseOrder.Id} no encontrada.");

        existing.Status = purchaseOrder.Status;
        existing.ProviderId = purchaseOrder.ProviderId;
        existing.Total = purchaseOrder.Total;

        // Actualizar productos
        if (purchaseOrder.PurchaseOrderDevice.Count > 0)
        {
            var existingProducts = await ctx.PurchaseOrderDevice
                .Where(pod => pod.OrderId == purchaseOrder.Id)
                .ToListAsync();

            ctx.PurchaseOrderDevice.RemoveRange(existingProducts);
            ctx.PurchaseOrderDevice.AddRange(purchaseOrder.PurchaseOrderDevice);
        }

        ctx.PurchaseOrder.Update(existing);
        await ctx.SaveChangesAsync();

        await ctx.Entry(existing)
            .Reference(po => po.Provider)
            .LoadAsync();
        await ctx.Entry(existing)
            .Reference(po => po.User)
            .LoadAsync();

        return existing;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var order = await ctx.PurchaseOrder.FindAsync(id);
        if (order is not null)
        {
            ctx.PurchaseOrder.Remove(order);
            await ctx.SaveChangesAsync();
        }
    }
}
