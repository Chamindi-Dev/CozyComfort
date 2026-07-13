using CozyComfort.Domain.DTOs.Material;
using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface IMaterialService
    {
        Task<IEnumerable<MaterialDto>> GetAllAsync();

        Task<MaterialDto?> GetByIdAsync(int id);

        Task<MaterialDto> CreateAsync(CreateMaterialDto dto);

        Task<bool> UpdateAsync(int id, UpdateMaterialDto dto);

        Task<bool> DeleteAsync(int id);
    }
}