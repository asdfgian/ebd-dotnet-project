namespace WebApiEbd.Core.Application.Dtos;

public record ContractDeviceDto(
    int DeviceId,
    string DeviceName,
    string DeviceSerialNumber,
    decimal RentalPrice
);

public record ContractListDto(
    int Id,
    string Title,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal Amount,
    string Status,
    string ProviderName,
    string UserName
);

public record ContractDetailDto(
    int Id,
    string Title,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal Amount,
    string Status,
    string? Route,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    ProviderDetailDto Provider,
    UserDetailDto User,
    IEnumerable<ContractDeviceDto> Devices
);

public record CreateContractDto(
    string Title,
    DateOnly StartDate,
    DateOnly EndDate,
    decimal Amount,
    int ProviderId,
    int UserId,
    int? OrderId,
    string? Route,
    List<ContractDeviceItemDto> Devices
);

public record UpdateContractDto(
    string? Title,
    DateOnly? StartDate,
    DateOnly? EndDate,
    decimal? Amount,
    int? ProviderId,
    string? Status,
    string? Route,
    List<ContractDeviceItemDto>? Devices
);

public record ContractDeviceItemDto(
    int DeviceId,
    decimal RentalPrice
);
