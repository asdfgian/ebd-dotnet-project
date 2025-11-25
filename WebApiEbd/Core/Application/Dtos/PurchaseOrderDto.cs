namespace WebApiEbd.Core.Application.Dtos;

public record PurchaseOrderProductDto(
    int ProductId,
    string ProductName,
    string? ProductModel,
    int Quantity,
    decimal Price
);

public record PurchaseOrderListDto(
    int Id,
    DateTime? CreatedAt,
    decimal Total,
    string Status,
    string ProviderName,
    string UserName
);

public record PurchaseOrderDetailDto(
    int Id,
    DateTime? CreatedAt,
    decimal Total,
    string Status,
    ProviderDetailDto Provider,
    UserListDto User,
    IEnumerable<PurchaseOrderProductDto> Products
);

public record CreatePurchaseOrderDto(
    int ProviderId,
    int UserId,
    List<PurchaseOrderItemDto> Products
);

public record UpdatePurchaseOrderDto(
    string? Status,
    int? ProviderId,
    List<PurchaseOrderItemDto>? Products
);

public record PurchaseOrderItemDto(
    int ProductId,
    int Quantity,
    decimal Price
);
