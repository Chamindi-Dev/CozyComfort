using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface IMaterialService
    {
        Task<IEnumerable<Material>> GetAllAsync();
        Task<Material?> GetByIdAsync(int id);
        Task<Material> AddAsync(Material material);
        Task<Material?> UpdateAsync(int id, Material material);
        Task<bool> DeleteAsync(int id);
    }
}