using CozyComfort.Domain.DTOs;

namespace CozyComfort.Application.Interfaces
{
    public interface IProductionCapacityService
    {
        Task<IEnumerable<ProductionCapacityDto>> GetAllAsync();

        Task<ProductionCapacityDto?> GetByIdAsync(int id);

        Task<ProductionCapacityDto> CreateAsync(CreateProductionCapacityDto dto);

        Task<bool> UpdateAsync(int id, UpdateProductionCapacityDto dto);

        Task<bool> DeleteAsync(int id);
    }
}