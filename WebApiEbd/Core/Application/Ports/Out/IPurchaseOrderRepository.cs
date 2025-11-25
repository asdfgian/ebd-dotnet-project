using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out;

public interface IPurchaseOrderRepository
{
    Task<PurchaseOrder> GetByIdAsync(int id);
    Task<IEnumerable<PurchaseOrder>> GetAllAsync();
    Task<PurchaseOrder> AddAsync(PurchaseOrder purchaseOrder);
    Task<PurchaseOrder> UpdateAsync(PurchaseOrder purchaseOrder);
    Task DeleteByIdAsync(int id);
}