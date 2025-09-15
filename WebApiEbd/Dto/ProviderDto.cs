namespace WebApiEbd.Dto;


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