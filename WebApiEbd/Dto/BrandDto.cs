namespace WebApiEbd.Dto;

public record BrandDto(int Id, string Name, string CountryOriginName);

public record CreateBrandDto(string Name, int CountryOriginId);

public record UpdateBrandDto(string Name, int CountryOriginId);
