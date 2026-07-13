using CozyComfort.Domain.DTOs.Role;

namespace CozyComfort.Application.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto>> GetAllAsync();

        Task<RoleDto?> GetByIdAsync(int id);

        Task<RoleDto> CreateAsync(CreateRoleDto dto);

        Task<bool> UpdateAsync(int id, UpdateRoleDto dto);

        Task<bool> DeleteAsync(int id);
    }
}