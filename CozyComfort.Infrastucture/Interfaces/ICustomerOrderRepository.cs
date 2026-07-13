using CozyComfort.Domain.Entities;

namespace CozyComfort.Infrastructure.Interfaces
{
    public interface ICustomerOrderRepository
    {

        Task<IEnumerable<CustomerOrder>> GetAllAsync();


        Task<CustomerOrder?> GetByIdAsync(int id);


        Task AddAsync(CustomerOrder order);


        Task UpdateAsync(CustomerOrder order);


        Task DeleteAsync(int id);

    }
}