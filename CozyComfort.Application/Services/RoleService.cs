using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.DTOs.Role;
using CozyComfort.Domain.Entities;
using CozyComfort.Infrastructure.Interfaces;

namespace CozyComfort.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RoleDto>> GetAllAsync()
        {
            var roles = await _repository.GetAllAsync();

            return roles.Select(x => new RoleDto
            {
                Id = x.Id,
                RoleName = x.RoleName
            });
        }

        public async Task<RoleDto?> GetByIdAsync(int id)
        {
            var role = await _repository.GetByIdAsync(id);

            if (role == null)
                return null;

            return new RoleDto
            {
                Id = role.Id,
                RoleName = role.RoleName
            };
        }

        public async Task<RoleDto> CreateAsync(CreateRoleDto dto)
        {
            var role = new Role
            {
                RoleName = dto.RoleName
            };

            var result = await _repository.CreateAsync(role);

            return new RoleDto
            {
                Id = result.Id,
                RoleName = result.RoleName
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateRoleDto dto)
        {
            var role = await _repository.GetByIdAsync(id);

            if (role == null)
                return false;

            role.RoleName = dto.RoleName;

            var result = await _repository.UpdateAsync(role);

            return result != null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}