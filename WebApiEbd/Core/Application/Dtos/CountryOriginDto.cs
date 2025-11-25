namespace WebApiEbd.Core.Application.Dtos;

public record CountryOriginDto(
    int Id,
    string Name
);

public record CreateCountryOriginDto(
    string Name
);

public record UpdateCountryOriginDto(
    string? Name
);
