using CozyComfort.Application.DTOs.StockMovement;

namespace CozyComfort.Application.Interfaces
{
    public interface IStockMovementService
    {
        Task<IEnumerable<StockMovementDto>> GetAllAsync();

        Task<StockMovementDto?> GetByIdAsync(int id);

        Task<StockMovementDto> CreateAsync(CreateStockMovementDto dto);

        Task<StockMovementDto?> UpdateAsync(int id, UpdateStockMovementDto dto);

        Task<bool> DeleteAsync(int id);
    }
}