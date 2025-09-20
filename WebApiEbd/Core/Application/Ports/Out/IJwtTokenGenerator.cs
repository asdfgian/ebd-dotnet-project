using WebApiEbd.Core.Domain.Models;

namespace WebApiEbd.Core.Application.Ports.Out
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(User user);
    }
}