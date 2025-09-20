using WebApiEbd.Application.Dtos;
using WebApiEbd.Application.Ports.In;
using WebApiEbd.Application.Ports.Out;
using WebApiEbd.Domain.Models;
using WebApiEbd.Infrastructure.Common;

namespace WebApiEbd.Application.Services
{
    public class UserAuthService(
        IUserAuthRepository repository,
        IJwtTokenGenerator tokenGenerator) : IUserAuthService
    {
        public async Task<AuthResultDto> SignIn(string identifier, string password)
        {
            var user = identifier.Contains('@')
                ? await repository.GetByEmailAsync(identifier)
                : await repository.GetByUsernameAsync(identifier);

            if (user == null) return new AuthResultDto(false, null, "Usuario no encontrado");

            var verified = PasswordHasher.VerifyPassword(password, user.Password);
            if (!verified) return new AuthResultDto(false, null, "Credenciales inválidas");

            var token = tokenGenerator.GenerateToken(user);
            return new AuthResultDto(true, token, null);
        }

        public async Task<AuthResultDto> SignUp(RegisterDto dto)
        {
            if (await repository.GetByEmailAsync(dto.Email) != null)
            {
                return new AuthResultDto(false, null, "Email ya registrado");
            }

            if (await repository.GetByUsernameAsync(dto.Username) != null)
            {
                return new AuthResultDto(false, null, "El nombre de usuario ya está en uso.");
            }

            var hashedPassword = PasswordHasher.HashPassword(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Username = dto.Username,
                Email = dto.Email,
                Password = hashedPassword,
                Gender = dto.Gender,
                AvatarUrl = $"https://avatar.iran.liara.run/public/{new Random().Next(1, 101)}",
                RoleId = 3
            };

            await repository.AddAsync(user);

            var token = tokenGenerator.GenerateToken(user);

            return new AuthResultDto(true, token, null);
        }
    }
}