using CozyComfort.Domain.Entities;

namespace CozyComfort.Infrastructure.Interfaces
{
    public interface ICustomerOrderItemRepository
    {
        Task<IEnumerable<CustomerOrderItem>> GetAllAsync();

        Task<CustomerOrderItem?> GetByIdAsync(int id);

        Task<IEnumerable<CustomerOrderItem>> GetByOrderIdAsync(int customerOrderId);

        Task<CustomerOrderItem> AddAsync(CustomerOrderItem customerOrderItem);

        Task UpdateAsync(CustomerOrderItem customerOrderItem);

        Task DeleteAsync(CustomerOrderItem customerOrderItem);

        Task<bool> ExistsAsync(int id);
    }
}