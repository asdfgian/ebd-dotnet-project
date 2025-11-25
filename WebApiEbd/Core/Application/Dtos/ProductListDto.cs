namespace WebApiEbd.Core.Application.Dtos;

public record ProductListDto(
    int Id,
    string Name,
    string? Model,
    string BrandName);

public record ProductDetailDto(
    int Id,
    string Name,
    string Description,
    string? Model,
    int BrandId,
    string BrandName
);

public record CreateProductDto(
    string Name,
    string? Description,
    string Model,
    int BrandId
);

public record UpdateProductDto(
    string? Name,
    string? Description,
    string? Model,
    int? BrandId
);
