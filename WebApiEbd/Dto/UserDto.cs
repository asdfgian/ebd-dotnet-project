namespace WebApiEbd.Dto;

public record UserListDto(string Username, string Name, char Status, string RoleName);

public record UserDetailDto(
    int Id,
    string Email,
    string Username,
    string Name,
    string? Phone,
    char Status,
    char Gender,
    string? AvatarUrl,
    string RoleName,
    DateTime? CreatedAt,
    DateTime? UpdatedAt,
    int? DepartmentId
);

public record UpdateUserDto(
    string? Username,
    string? Phone,
    string? AvatarUrl,
    int? DepartmentId
);