using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface ITransferOrderRepository
    {
        Task<IEnumerable<TransferOrder>> GetAllAsync();

        Task<TransferOrder?> GetByIdAsync(int id);

        Task<TransferOrder> AddAsync(TransferOrder transferOrder);

        Task<TransferOrder?> UpdateAsync(TransferOrder transferOrder);

        Task<bool> DeleteAsync(int id);
    }
}