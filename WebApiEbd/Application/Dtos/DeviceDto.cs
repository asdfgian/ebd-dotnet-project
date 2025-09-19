namespace WebApiEbd.Application.Dtos;

public record DeviceListDto(
    int Id,
    string Name,
    decimal Price,
    string Status,
    string BrandName
);

public record DeviceDetailDto(
    int Id,
    string Name,
    string? Description,
    decimal Price,
    string? Model,
    string SerialNumber,
    string Status,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    BrandDto Brand
);

public record CreateDeviceDto(
    string Name,
    string? Description,
    decimal Price,
    string? Model,
    string SerialNumber,
    string Status,
    int BrandId
);

public record UpdateDeviceDto(
    string Name,
    string? Description,
    decimal Price,
    string? Model,
    string Status,
    int BrandId
);
