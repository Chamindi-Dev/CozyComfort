using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Domain.Entities;
using CozyComfort.Application.Interfaces;

namespace CozyComfort.Application.Services
{
    public class CustomerOrderItemService : ICustomerOrderItemService
    {
        private readonly ICustomerOrderItemRepository _repository;


        public CustomerOrderItemService(
            ICustomerOrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerOrderItem>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<CustomerOrderItem?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<CustomerOrderItem>> GetByOrderIdAsync(
            int customerOrderId)
        {
            return await _repository.GetByOrderIdAsync(customerOrderId);
        }

        public async Task<CustomerOrderItem> CreateAsync(
            CustomerOrderItem customerOrderItem)
        {
            if (customerOrderItem.Quantity <= 0)
            {
                throw new Exception(
                    "Quantity must be greater than zero");
            }


            if (customerOrderItem.UnitPrice < 0)
            {
                throw new Exception(
                    "Unit price cannot be negative");
            }


            return await _repository.AddAsync(customerOrderItem);
        }

        public async Task UpdateAsync(
            CustomerOrderItem customerOrderItem)
        {
            var exists = await _repository
                .ExistsAsync(customerOrderItem.Id);


            if (!exists)
            {
                throw new Exception(
                    "Customer order item not found");
            }


            await _repository.UpdateAsync(customerOrderItem);
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);


            if (item == null)
            {
                throw new Exception(
                    "Customer order item not found");
            }


            await _repository.DeleteAsync(item);
        }
    }
}