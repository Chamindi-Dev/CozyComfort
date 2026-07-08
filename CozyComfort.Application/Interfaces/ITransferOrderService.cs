using CozyComfort.Application.DTOs.TransferOrder;

namespace CozyComfort.Application.Interfaces
{
    public interface ITransferOrderService
    {
        Task<IEnumerable<TransferOrderDto>> GetAllAsync();

        Task<TransferOrderDto?> GetByIdAsync(int id);

        Task<TransferOrderDto> CreateAsync(CreateTransferOrderDto dto);

        Task<TransferOrderDto?> UpdateAsync(int id, UpdateTransferOrderDto dto);

        Task<bool> DeleteAsync(int id);
    }
}