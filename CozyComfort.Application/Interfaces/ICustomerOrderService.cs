using CozyComfort.Application.DTOs;

namespace CozyComfort.Application.Interfaces
{
    public interface ICustomerOrderService
    {
        Task<IEnumerable<CustomerOrderDto>> GetAllAsync();

        Task<CustomerOrderDto?> GetByIdAsync(int id);

        Task<CustomerOrderDto> CreateAsync(CreateCustomerOrderDto dto);

        Task<bool> UpdateAsync(int id, UpdateCustomerOrderDto dto);

        Task<bool> DeleteAsync(int id);
    }
}