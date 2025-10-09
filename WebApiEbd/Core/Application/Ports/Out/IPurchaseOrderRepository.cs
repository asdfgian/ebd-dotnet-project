using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out;

public interface IPurchaseOrderRepository
{
    Task<PurchaseOrder> AddAsync(PurchaseOrder purchaseOrder);
}