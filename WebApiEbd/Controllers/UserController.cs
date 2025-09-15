using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEbd.Context;
using WebApiEbd.Dto;
namespace WebApiEbd.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(AppDbContext context) : ControllerBase
{
    // GET: api/user
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserListDto>>> GetAll()
    {
        var users = await context.User
            .AsNoTracking()
            .Include(u => u.Role)
            .Select(u => new UserListDto(
                u.Username,
                u.Name,
                u.Status,
                u.Role.Name
            ))
            .ToListAsync();

        return Ok(users);
    }

    // GET: api/user/{id}
    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDetailDto>> GetById(int id)
    {
        var user = await context.User
            .AsNoTracking()
            .Include(u => u.Role)
            .Where(u => u.Id == id)
            .Select(u => new UserDetailDto(
                u.Id,
                u.Email,
                u.Username,
                u.Name,
                u.Phone,
                u.Status,
                u.Gender,
                u.AvatarUrl,
                u.Role.Name,
                u.CreatedAt,
                u.UpdatedAt,
                u.DepartmentId
            ))
            .FirstOrDefaultAsync();

        if (user == null)
            return NotFound($"No se encontró el usuario con id {id}");

        return Ok(user);
    }

    // PUT: api/user/{id}
    [HttpPut("{id:int}")]
    public async Task<ActionResult<UserDetailDto>> Update(int id, [FromBody] UpdateUserDto dto)
    {
        var user = await context.User
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return NotFound($"No se encontró el usuario con id {id}");

        if (!string.IsNullOrWhiteSpace(dto.Username))
            user.Username = dto.Username;

        if (!string.IsNullOrWhiteSpace(dto.Phone))
            user.Phone = dto.Phone;

        if (!string.IsNullOrWhiteSpace(dto.AvatarUrl))
            user.AvatarUrl = dto.AvatarUrl;

        if (dto.DepartmentId != null)
            user.DepartmentId = dto.DepartmentId;

        user.UpdatedAt = DateTime.Now;

        await context.SaveChangesAsync();

        var result = new UserDetailDto(
            user.Id,
            user.Email,
            user.Username,
            user.Name,
            user.Phone,
            user.Status,
            user.Gender,
            user.AvatarUrl,
            user.Role.Name,
            user.CreatedAt,
            user.UpdatedAt,
            user.DepartmentId
        );

        return Ok(result);
    }

}