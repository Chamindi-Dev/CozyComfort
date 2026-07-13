using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
