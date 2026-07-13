using CozyComfort.Domain.DTOs;

namespace CozyComfort.Application.Interfaces
{
    public interface IFactoryInventoryService
    {
        Task<IEnumerable<FactoryInventoryDto>> GetAllAsync();

        Task<FactoryInventoryDto?> GetByIdAsync(int id);

        Task<FactoryInventoryDto> CreateAsync(CreateFactoryInventoryDto dto);

        Task<bool> UpdateAsync(int id, UpdateFactoryInventoryDto dto);

        Task<bool> DeleteAsync(int id);
    }
}