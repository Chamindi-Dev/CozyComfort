using CozyComfort.Domain.DTOs.User;

namespace CozyComfort.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllAsync();

        Task<UserDto?> GetByIdAsync(int id);

        Task<UserDto> AddAsync(CreateUserDto dto);

        Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto);

        Task<bool> DeleteAsync(int id);
    }
}