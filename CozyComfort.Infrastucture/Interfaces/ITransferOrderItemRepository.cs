using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface ITransferOrderItemRepository
    {
        Task<IEnumerable<TransferOrderItem>> GetAllAsync();

        Task<TransferOrderItem?> GetByIdAsync(int id);

        Task AddAsync(TransferOrderItem transferOrderItem);

        Task<TransferOrderItem?> UpdateAsync(TransferOrderItem transferOrderItem);

        Task<bool> DeleteAsync(int id);
    }
}