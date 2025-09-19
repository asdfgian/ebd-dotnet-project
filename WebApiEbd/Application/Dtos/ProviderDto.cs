namespace WebApiEbd.Application.Dtos;

public record ProviderApiDetailDto(
    string Ruc,
    string Name,
    string Address,
    string District,
    string Province,
    string Status
);

public record ProviderListDto(
    string Ruc,
    string Name,
    string Email,
    string Phone,
    string Status
);

public record ProviderDetailDto(
    int Id,
    string Ruc,
    string Name,
    string? Address,
    string District,
    string Province,
    string Department,
    string Status,
    string? Email,
    string? Phone
);

public record CreateProviderDto(
    string Ruc,
    string Name,
    string? Address,
    string District,
    string Province,
    string Department,
    string Status,
    string? Email,
    string? Phone
);

public record UpdateProviderDto(
    string? Name,
    string? Address,
    string? District,
    string? Province,
    string? Department,
    string? Status,
    string? Email,
    string? Phone
);
