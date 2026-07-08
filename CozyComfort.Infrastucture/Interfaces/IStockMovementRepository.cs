using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface IStockMovementRepository
    {
        Task<IEnumerable<StockMovement>> GetAllAsync();

        Task<StockMovement?> GetByIdAsync(int id);

        Task AddAsync(StockMovement stockMovement);

        Task<StockMovement?> UpdateAsync(StockMovement stockMovement);

        Task<bool> DeleteAsync(int id);
    }
}