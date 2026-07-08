using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface IProductionCapacityRepository
    {
        Task<IEnumerable<ProductionCapacity>> GetAllAsync();

        Task<ProductionCapacity?> GetByIdAsync(int id);

        Task AddAsync(ProductionCapacity productionCapacity);

        Task UpdateAsync(ProductionCapacity productionCapacity);

        Task DeleteAsync(int id);
    }
}