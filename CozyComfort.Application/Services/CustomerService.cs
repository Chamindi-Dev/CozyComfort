using CozyComfort.Application.DTOs.Customer;
using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            var customers = await _repository.GetAllAsync();

            return customers.Select(c => new CustomerDto
            {
                Id = c.Id,
                CustomerName = c.CustomerName,
                Email = c.Email,
                Phone = c.Phone,
                Address = c.Address
            });
        }

        public async Task<CustomerDto?> GetByIdAsync(int id)
        {
            var customer = await _repository.GetByIdAsync(id);

            if (customer == null)
                return null;

            return new CustomerDto
            {
                Id = customer.Id,
                CustomerName = customer.CustomerName,
                Email = customer.Email,
                Phone = customer.Phone,
                Address = customer.Address
            };
        }

        public async Task AddAsync(CreateCustomerDto dto)
        {
            var customer = new Customer
            {
                CustomerName = dto.CustomerName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address
            };

            await _repository.AddAsync(customer);
        }

        public async Task UpdateAsync(UpdateCustomerDto dto)
        {
            var customer = await _repository.GetByIdAsync(dto.Id);

            if (customer == null)
                throw new Exception("Customer not found.");

            customer.CustomerName = dto.CustomerName;
            customer.Email = dto.Email;
            customer.Phone = dto.Phone;
            customer.Address = dto.Address;

            await _repository.UpdateAsync(customer);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}