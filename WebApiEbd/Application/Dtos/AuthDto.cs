namespace WebApiEbd.Application.Dtos;

public record RegisterDto(string Name, string Username, string Email, char Gender, string Password);

public record LoginDto(string Identifier, string Password);

public record AuthResultDto(bool Success, string? Token, string? Error);