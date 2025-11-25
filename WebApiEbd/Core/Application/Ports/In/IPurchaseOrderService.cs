using WebApiEbd.Core.Application.Dtos;

namespace WebApiEbd.Core.Application.Ports.In;

public interface IPurchaseOrderService
{
    Task<IEnumerable<PurchaseOrderListDto>> ListPurchaseOrders();
    Task<PurchaseOrderDetailDto> PurchaseOrderById(int id);
    Task<PurchaseOrderDetailDto> CreatePurchaseOrder(CreatePurchaseOrderDto dto);
    Task<PurchaseOrderDetailDto> UpdatePurchaseOrderById(int id, UpdatePurchaseOrderDto dto);
    Task DeletePurchaseOrderById(int id);
}
