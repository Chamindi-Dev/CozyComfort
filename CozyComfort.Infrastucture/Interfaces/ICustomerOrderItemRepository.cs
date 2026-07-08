using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface ICustomerOrderItemRepository
    {
        // Get all order items
        Task<IEnumerable<CustomerOrderItem>> GetAllAsync();


        // Get single order item by Id
        Task<CustomerOrderItem?> GetByIdAsync(int id);


        // Get items by Customer Order Id
        Task<IEnumerable<CustomerOrderItem>> GetByOrderIdAsync(int customerOrderId);


        // Add new order item
        Task<CustomerOrderItem> AddAsync(CustomerOrderItem customerOrderItem);


        // Update existing order item
        Task UpdateAsync(CustomerOrderItem customerOrderItem);


        // Delete order item
        Task DeleteAsync(CustomerOrderItem customerOrderItem);


        // Check if exists
        Task<bool> ExistsAsync(int id);
    }
}