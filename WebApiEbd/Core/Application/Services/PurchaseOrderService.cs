using WebApiEbd.Core.Application.Dtos;
using WebApiEbd.Core.Application.Ports.In;
using WebApiEbd.Core.Application.Ports.Out;
using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Services;

public class PurchaseOrderService(
    IPurchaseOrderRepository purchaseOrderRepository,
    IProductRepository productRepository,
    IUserRepository userRepository,
    IProviderRepository providerRepository) : IPurchaseOrderService
{
    public async Task<IEnumerable<PurchaseOrderListDto>> ListPurchaseOrders()
    {
        var orders = await purchaseOrderRepository.GetAllAsync();
        return orders.Select(o => new PurchaseOrderListDto(
            o.Id,
            o.CreatedAt,
            o.Total,
            o.Status,
            o.Provider.Name,
            o.User.Name ?? o.User.Username
        ));
    }

    public async Task<PurchaseOrderDetailDto> PurchaseOrderById(int id)
    {
        var order = await purchaseOrderRepository.GetByIdAsync(id) ??
                    throw new KeyNotFoundException($"Orden de compra con id {id} no encontrada.");

        var products = order.PurchaseOrderDevice.Select(pod => new PurchaseOrderProductDto(
            pod.Product.Id,
            pod.Product.Name,
            pod.Product.Model,
            pod.Quantity,
            pod.Price
        ));

        return new PurchaseOrderDetailDto(
            order.Id,
            order.CreatedAt,
            order.Total,
            order.Status,
            new ProviderDetailDto(
                order.Provider.Id,
                order.Provider.Ruc,
                order.Provider.Name,
                order.Provider.Address,
                order.Provider.District,
                order.Provider.Province,
                order.Provider.Department,
                order.Provider.Status,
                order.Provider.Email,
                order.Provider.Phone
            ),
            new UserListDto(
                order.User.Id,
                order.User.Username,
                order.User.Name ?? string.Empty,
                order.User.Status,
                order.User.Role.Name
            ),
            products
        );
    }

    public async Task<PurchaseOrderDetailDto> CreatePurchaseOrder(CreatePurchaseOrderDto dto)
    {
        var provider = await providerRepository.GetByIdAsync(dto.ProviderId) ??
                       throw new KeyNotFoundException($"Proveedor con id {dto.ProviderId} no encontrado.");

        var user = await userRepository.GetByIdAsync(dto.UserId) ??
                   throw new KeyNotFoundException($"Usuario con id {dto.UserId} no encontrado.");

        decimal total = 0;
        var purchaseOrderDevices = new List<PurchaseOrderDevice>();

        foreach (var item in dto.Products)
        {
            var product = await productRepository.GetByIdAsync(item.ProductId) ??
                          throw new KeyNotFoundException($"Producto con id {item.ProductId} no encontrado.");

            var lineTotal = item.Quantity * item.Price;
            total += lineTotal;

            purchaseOrderDevices.Add(new PurchaseOrderDevice
            {
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Price = item.Price
            });
        }

        var order = new PurchaseOrder
        {
            ProviderId = dto.ProviderId,
            UserId = dto.UserId,
            Total = total,
            Status = "DRAFT",
            PurchaseOrderDevice = purchaseOrderDevices
        };

        var created = await purchaseOrderRepository.AddAsync(order);
        return await PurchaseOrderById(created.Id);
    }

    public async Task<PurchaseOrderDetailDto> UpdatePurchaseOrderById(int id, UpdatePurchaseOrderDto dto)
    {
        var order = await purchaseOrderRepository.GetByIdAsync(id) ??
                    throw new KeyNotFoundException($"Orden de compra con id {id} no encontrada.");

        if (!string.IsNullOrWhiteSpace(dto.Status))
            order.Status = dto.Status;

        if (dto.ProviderId.HasValue)
        {
            var provider = await providerRepository.GetByIdAsync(dto.ProviderId.Value) ??
                           throw new KeyNotFoundException($"Proveedor con id {dto.ProviderId.Value} no encontrado.");
            order.ProviderId = dto.ProviderId.Value;
        }

        if (dto.Products != null && dto.Products.Count > 0)
        {
            order.PurchaseOrderDevice.Clear();
            decimal total = 0;

            foreach (var item in dto.Products)
            {
                var product = await productRepository.GetByIdAsync(item.ProductId) ??
                              throw new KeyNotFoundException($"Producto con id {item.ProductId} no encontrado.");

                var lineTotal = item.Quantity * item.Price;
                total += lineTotal;

                order.PurchaseOrderDevice.Add(new PurchaseOrderDevice
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price
                });
            }

            order.Total = total;
        }

        var updated = await purchaseOrderRepository.UpdateAsync(order);
        return await PurchaseOrderById(updated.Id);
    }

    public async Task DeletePurchaseOrderById(int id)
    {
        await purchaseOrderRepository.DeleteByIdAsync(id);
    }
}
