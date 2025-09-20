namespace WebApiEbd.Core.Application.Dtos;

public record BrandDto(int Id, string Name, string CountryOriginName);

public record CreateBrandDto(string Name, int CountryOriginId);

public record UpdateBrandDto(string Name, int CountryOriginId);
