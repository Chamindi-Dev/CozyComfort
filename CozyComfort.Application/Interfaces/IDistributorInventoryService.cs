using CozyComfort.Domain.DTOs;

namespace CozyComfort.Application.Interfaces
{
    public interface IDistributorInventoryService
    {
        Task<IEnumerable<DistributorInventoryDto>> GetAllAsync();

        Task<DistributorInventoryDto?> GetByIdAsync(int id);

        Task<DistributorInventoryDto> CreateAsync(CreateDistributorInventoryDto dto);

        Task<bool> UpdateAsync(int id, UpdateDistributorInventoryDto dto);

        Task<bool> DeleteAsync(int id);
    }
}