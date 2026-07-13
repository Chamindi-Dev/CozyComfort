using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.DTOs.User;
using CozyComfort.Domain.Entities;
using CozyComfort.Infrastructure.Interfaces;

namespace CozyComfort.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();

            return users.Select(x => new UserDto
            {
                Id = x.Id,
                FullName = x.FullName,
                Email = x.Email,
                Phone = x.Phone,
                Address = x.Address
            });
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _repository.GetByIdAsync(id);

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            };
        }

        public async Task<UserDto> AddAsync(CreateUserDto dto)
        {
            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                Password = "",
                Phone = dto.Phone,
                Address = dto.Address,
                RoleId = dto.RoleId
            };

            user.Password = PasswordHasher.HashPassword(dto.Password);

            await _repository.AddAsync(user);

            return new UserDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            };
        }

        public async Task<UserDto?> UpdateAsync(int id, UpdateUserDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);

            if (existing == null)
                return null;

            existing.FullName = dto.FullName;
            existing.Email = dto.Email;
            existing.Phone = dto.Phone;
            existing.Address = dto.Address;

            await _repository.UpdateAsync(existing);

            return new UserDto
            {
                Id = existing.Id,
                FullName = existing.FullName,
                Email = existing.Email,
                Phone = existing.Phone,
                Address = existing.Address
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}