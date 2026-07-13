using CozyComfort.Domain.Entities;

namespace CozyComfort.Infrastructure.Interfaces
{
    public interface IDistributorInventoryRepository
    {
        Task<IEnumerable<DistributorInventory>> GetAllAsync();

        Task<DistributorInventory?> GetByIdAsync(int id);

        Task AddAsync(DistributorInventory inventory);

        Task UpdateAsync(DistributorInventory inventory);

        Task DeleteAsync(int id);
    }
}