using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.DTOs.Auth;
using CozyComfort.Domain.Entities;
using CozyComfort.Infrastructure.Interfaces;

namespace CozyComfort.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public AuthService(IUserRepository userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto request)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
                return null;

            var isValid = PasswordHasher.VerifyPassword(request.Password, user.Password);
            if (!isValid)
                return null;

            var token = _tokenService.GenerateToken(user);

            return new LoginResponseDto
            {
                Token = token,
                UserId = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role?.RoleName ?? "User"
            };
        }
    }
}
