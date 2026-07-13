using CozyComfort.Domain.DTOs;
using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Domain.Entities;
using CozyComfort.Application.Interfaces;



namespace CozyComfort.Application.Services
{
    public class CustomerOrderService : ICustomerOrderService
    {

        private readonly ICustomerOrderRepository _repository;


        public CustomerOrderService(
            ICustomerOrderRepository repository)
        {
            _repository = repository;
        }
        public async Task<IEnumerable<CustomerOrderDto>> GetAllAsync()
        {

            var orders = await _repository.GetAllAsync();


            return orders.Select(o => new CustomerOrderDto
            {
                Id = o.Id,
                OrderNumber = o.OrderNumber,
                CustomerId = o.CustomerId,
                SellerId = o.SellerId,
                Status = o.Status,
                FinalSource = o.FinalSource,
                OrderDate = o.OrderDate,
                ExpectedDeliveryDate = o.ExpectedDeliveryDate,
                TotalAmount = o.TotalAmount,
                Remarks = o.Remarks
            });

        }

        public async Task<CustomerOrderDto?> GetByIdAsync(int id)
        {

            var order = await _repository.GetByIdAsync(id);


            if (order == null)
                return null;



            return new CustomerOrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber,
                CustomerId = order.CustomerId,
                SellerId = order.SellerId,
                Status = order.Status,
                FinalSource = order.FinalSource,
                OrderDate = order.OrderDate,
                ExpectedDeliveryDate = order.ExpectedDeliveryDate,
                TotalAmount = order.TotalAmount,
                Remarks = order.Remarks
            };

        }

        public async Task<CustomerOrderDto> CreateAsync(
            CreateCustomerOrderDto dto)
        {

            var order = new CustomerOrder
            {

                OrderNumber = dto.OrderNumber,

                CustomerId = dto.CustomerId,

                SellerId = dto.SellerId,

                Status = dto.Status,

                FinalSource = dto.FinalSource,

                OrderDate = DateTime.Now,

                ExpectedDeliveryDate = dto.ExpectedDeliveryDate,

                TotalAmount = dto.TotalAmount,

                Remarks = dto.Remarks
            };



            await _repository.AddAsync(order);



            return new CustomerOrderDto
            {

                Id = order.Id,

                OrderNumber = order.OrderNumber,

                CustomerId = order.CustomerId,

                SellerId = order.SellerId,

                Status = order.Status,

                FinalSource = order.FinalSource,

                OrderDate = order.OrderDate,

                ExpectedDeliveryDate = order.ExpectedDeliveryDate,

                TotalAmount = order.TotalAmount,

                Remarks = order.Remarks
            };

        }

        public async Task<bool> UpdateAsync(
            int id,
            UpdateCustomerOrderDto dto)
        {


            var order = await _repository.GetByIdAsync(id);


            if (order == null)
                return false;



            order.CustomerId = dto.CustomerId;

            order.SellerId = dto.SellerId;

            order.Status = dto.Status;

            order.FinalSource = dto.FinalSource;

            order.ExpectedDeliveryDate = dto.ExpectedDeliveryDate;

            order.TotalAmount = dto.TotalAmount;

            order.Remarks = dto.Remarks;



            await _repository.UpdateAsync(order);



            return true;

        }

        public async Task<bool> DeleteAsync(int id)
        {

            var order = await _repository.GetByIdAsync(id);


            if (order == null)
                return false;



            await _repository.DeleteAsync(id);


            return true;

        }

    }
}