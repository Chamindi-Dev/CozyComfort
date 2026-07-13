using CozyComfort.Domain.Entities;

namespace CozyComfort.Infrastructure.Interfaces
{
    public interface IFactoryInventoryRepository
    {
        Task<IEnumerable<FactoryInventory>> GetAllAsync();

        Task<FactoryInventory?> GetByIdAsync(int id);

        Task AddAsync(FactoryInventory inventory);

        Task UpdateAsync(FactoryInventory inventory);

        Task DeleteAsync(int id);
    }
}