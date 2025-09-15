namespace WebApiEbd.Dto;

public record RegisterRequestDto(string Name, string Username, string Email, char Gender, string Password);

public record LoginRequestDto(string Identifier, string Password);

public record LoginResponseDto(string Token);