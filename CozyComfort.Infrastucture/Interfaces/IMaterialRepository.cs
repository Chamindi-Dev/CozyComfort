using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> GetAllAsync();

        Task<Material?> GetByIdAsync(int id);

        Task<Material> CreateAsync(Material material);

        Task<Material?> UpdateAsync(Material material);

        Task<bool> DeleteAsync(int id);
        Task<Material> AddAsync(Material material);
    }
}