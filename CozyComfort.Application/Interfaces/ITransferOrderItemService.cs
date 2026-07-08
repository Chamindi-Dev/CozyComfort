using CozyComfort.Application.DTOs.TransferOrderItem;

namespace CozyComfort.Application.Interfaces
{
    public interface ITransferOrderItemService
    {
        Task<IEnumerable<TransferOrderItemDto>> GetAllAsync();

        Task<TransferOrderItemDto?> GetByIdAsync(int id);

        Task<TransferOrderItemDto> CreateAsync(CreateTransferOrderItemDto dto);

        Task<TransferOrderItemDto?> UpdateAsync(int id, UpdateTransferOrderItemDto dto);

        Task<bool> DeleteAsync(int id);
    }
}