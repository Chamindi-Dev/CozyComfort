using CozyComfort.Application.DTOs.Customer;

namespace CozyComfort.Application.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<CustomerDto>> GetAllAsync();

        Task<CustomerDto?> GetByIdAsync(int id);

        Task AddAsync(CreateCustomerDto dto);

        Task UpdateAsync(UpdateCustomerDto dto);

        Task DeleteAsync(int id);
    }
}