using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEbd.Context;
using WebApiEbd.Dto;
using WebApiEbd.Models;
using WebApiEbd.Services;
using WebApiEbd.Utils;

namespace WebApiEbd.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(AppDbContext context, JwtService jwtService) : ControllerBase
{

    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] RegisterRequestDto request)
    {
        if (await context.User.AnyAsync(u => u.Email == request.Email))
        {
            return BadRequest("El email ya está registrado.");
        }

        if (await context.User.AnyAsync(u => u.Username == request.Username))
        {
            return BadRequest("El nombre de usuario ya está en uso.");
        }

        var hashedPassword = PasswordHasher.HashPassword(request.Password);

        var user = new User
        {
            Name = request.Name,
            Username = request.Username,
            Email = request.Email,
            Password = hashedPassword,
            Gender = request.Gender,
            AvatarUrl = $"https://avatar.iran.liara.run/public/{new Random().Next(1, 101)}",
            RoleId = 3
        };

        context.User.Add(user);
        await context.SaveChangesAsync();

        var token = jwtService.GenerateToken(user.Id, user.Username);

        return Ok(new LoginResponseDto(token));
    }


    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] LoginRequestDto request)
    {
        var user = await context.User.FirstOrDefaultAsync(u =>
            u.Username == request.Identifier || u.Email == request.Identifier);

        if (user == null || !PasswordHasher.VerifyPassword(request.Password, user.Password))
        {
            return Unauthorized("Usuario o contraseña incorrectos");
        }

        var token = jwtService.GenerateToken(user.Id, user.Username);

        return Ok(new LoginResponseDto(token));
    }

    [HttpPost("sign-out")]
    [Authorize]
    public new IActionResult SignOut()
    {
        return Ok(new { message = "Sesión cerrada correctamente" });
    }

}