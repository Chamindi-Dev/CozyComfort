using CozyComfort.Domain.DTOs.Auth;

namespace CozyComfort.Application.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> LoginAsync(LoginRequestDto request);
    }
}
