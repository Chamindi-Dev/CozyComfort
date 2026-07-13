using CozyComfort.Domain.DTOs.AvailabilityRequest;
using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Domain.Entities;
using CozyComfort.Application.Interfaces;



namespace CozyComfort.Application.Services
{
    public class AvailabilityRequestService
        : IAvailabilityRequestService
    {

        private readonly IAvailabilityRequestRepository _repository;


        public AvailabilityRequestService(
            IAvailabilityRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<AvailabilityRequestDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();


            return data.Select(x => new AvailabilityRequestDto
            {
                Id = x.Id,
                RequestNumber = x.RequestNumber,
                CustomerOrderId = x.CustomerOrderId,
                BlanketModelId = x.BlanketModelId,
                SellerId = x.SellerId,
                DistributorId = x.DistributorId,
                RequestedQuantity = x.RequestedQuantity,
                RequestLevel = x.RequestLevel,
                Status = x.Status,
                RequestedDate = x.RequestedDate,
                ResponseDate = x.ResponseDate,
                ExpectedLeadTimeDays = x.ExpectedLeadTimeDays,
                ResponseMessage = x.ResponseMessage
            });
        }

        public async Task<AvailabilityRequestDto?> GetByIdAsync(int id)
        {
            var x = await _repository.GetByIdAsync(id);


            if (x == null)
                return null;


            return new AvailabilityRequestDto
            {
                Id = x.Id,
                RequestNumber = x.RequestNumber,
                CustomerOrderId = x.CustomerOrderId,
                BlanketModelId = x.BlanketModelId,
                SellerId = x.SellerId,
                DistributorId = x.DistributorId,
                RequestedQuantity = x.RequestedQuantity,
                RequestLevel = x.RequestLevel,
                Status = x.Status,
                RequestedDate = x.RequestedDate,
                ResponseDate = x.ResponseDate,
                ExpectedLeadTimeDays = x.ExpectedLeadTimeDays,
                ResponseMessage = x.ResponseMessage
            };
        }

        public async Task<AvailabilityRequestDto> CreateAsync(
            CreateAvailabilityRequestDto dto)
        {

            var entity = new AvailabilityRequest
            {
                RequestNumber = dto.RequestNumber,
                CustomerOrderId = dto.CustomerOrderId,
                BlanketModelId = dto.BlanketModelId,
                SellerId = dto.SellerId,
                DistributorId = dto.DistributorId,
                RequestedQuantity = dto.RequestedQuantity,
                RequestLevel = dto.RequestLevel,
                Status = dto.Status,
                ExpectedLeadTimeDays = dto.ExpectedLeadTimeDays,
                ResponseMessage = dto.ResponseMessage,
                RequestedDate = DateTime.Now
            };


            var result = await _repository.AddAsync(entity);


            return await GetByIdAsync(result.Id)
                   ?? throw new Exception("Create failed");
        }


        public async Task UpdateAsync(
            UpdateAvailabilityRequestDto dto)
        {

            var entity = await _repository.GetByIdAsync(dto.Id);


            if (entity == null)
                throw new Exception("Request not found");


            entity.Status = dto.Status;
            entity.ResponseDate = dto.ResponseDate;
            entity.ExpectedLeadTimeDays = dto.ExpectedLeadTimeDays;
            entity.ResponseMessage = dto.ResponseMessage;


            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {

            var entity = await _repository.GetByIdAsync(id);


            if (entity == null)
                throw new Exception("Request not found");


            await _repository.DeleteAsync(entity);
        }
    }
}