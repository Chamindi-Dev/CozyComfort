using CozyComfort.Domain.DTOs;

namespace CozyComfort.Application.Interfaces
{
    public interface ISellerInventoryService
    {
        Task<IEnumerable<SellerInventoryDto>> GetAllAsync();

        Task<SellerInventoryDto?> GetByIdAsync(int id);

        Task<SellerInventoryDto> CreateAsync(CreateSellerInventoryDto dto);

        Task<bool> UpdateAsync(int id, UpdateSellerInventoryDto dto);

        Task<bool> DeleteAsync(int id);
    }
}