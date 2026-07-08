using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface ICustomerOrderItemService
    {
        Task<IEnumerable<CustomerOrderItem>> GetAllAsync();

        Task<CustomerOrderItem?> GetByIdAsync(int id);

        Task<IEnumerable<CustomerOrderItem>> GetByOrderIdAsync(
            int customerOrderId);

        Task<CustomerOrderItem> CreateAsync(
            CustomerOrderItem customerOrderItem);

        Task UpdateAsync(
            CustomerOrderItem customerOrderItem);

        Task DeleteAsync(int id);
    }
}