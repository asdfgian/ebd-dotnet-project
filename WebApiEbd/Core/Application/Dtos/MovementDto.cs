namespace WebApiEbd.Core.Application.Dtos;

public record MovementRequestDto(
    string Comment,
    int DeviceId,
    string Type,
    int UserOriginId,
    int UserDestinationId,
    int CreatedBy
);

public record MovementResponseDto(
    DateTime? Date,
    string Comment,
    int DeviceId,
    string Type,
    int? UserOriginId,
    int? UserDestinationId,
    int CreatedBy
);