using CozyComfort.Application.DTOs.TransferOrder;
using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Services
{
    public class TransferOrderService : ITransferOrderService
    {
        private readonly ITransferOrderRepository _repository;

        public TransferOrderService(ITransferOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TransferOrderDto>> GetAllAsync()
        {
            var transferOrders = await _repository.GetAllAsync();

            return transferOrders.Select(x => new TransferOrderDto
            {
                Id = x.Id,
                TransferNumber = x.TransferNumber,
                CustomerOrderId = x.CustomerOrderId,
                FromLocationType = x.FromLocationType,
                ToLocationType = x.ToLocationType,
                FromDistributorId = x.FromDistributorId,
                ToDistributorId = x.ToDistributorId,
                FromSellerId = x.FromSellerId,
                ToSellerId = x.ToSellerId,
                Status = x.Status,
                RequestedDate = x.RequestedDate,
                ApprovedDate = x.ApprovedDate,
                CompletedDate = x.CompletedDate,
                Remarks = x.Remarks
            });
        }

        public async Task<TransferOrderDto?> GetByIdAsync(int id)
        {
            var x = await _repository.GetByIdAsync(id);

            if (x == null)
                return null;

            return new TransferOrderDto
            {
                Id = x.Id,
                TransferNumber = x.TransferNumber,
                CustomerOrderId = x.CustomerOrderId,
                FromLocationType = x.FromLocationType,
                ToLocationType = x.ToLocationType,
                FromDistributorId = x.FromDistributorId,
                ToDistributorId = x.ToDistributorId,
                FromSellerId = x.FromSellerId,
                ToSellerId = x.ToSellerId,
                Status = x.Status,
                RequestedDate = x.RequestedDate,
                ApprovedDate = x.ApprovedDate,
                CompletedDate = x.CompletedDate,
                Remarks = x.Remarks
            };
        }

        public async Task<TransferOrderDto> CreateAsync(CreateTransferOrderDto dto)
        {
            var entity = new TransferOrder
            {
                TransferNumber = dto.TransferNumber,
                CustomerOrderId = dto.CustomerOrderId,
                FromLocationType = dto.FromLocationType,
                ToLocationType = dto.ToLocationType,
                FromDistributorId = dto.FromDistributorId,
                ToDistributorId = dto.ToDistributorId,
                FromSellerId = dto.FromSellerId,
                ToSellerId = dto.ToSellerId,
                Status = dto.Status,
                RequestedDate = dto.RequestedDate,
                ApprovedDate = dto.ApprovedDate,
                CompletedDate = dto.CompletedDate,
                Remarks = dto.Remarks
            };

            await _repository.AddAsync(entity);

            return new TransferOrderDto
            {
                Id = entity.Id,
                TransferNumber = entity.TransferNumber,
                CustomerOrderId = entity.CustomerOrderId,
                FromLocationType = entity.FromLocationType,
                ToLocationType = entity.ToLocationType,
                FromDistributorId = entity.FromDistributorId,
                ToDistributorId = entity.ToDistributorId,
                FromSellerId = entity.FromSellerId,
                ToSellerId = entity.ToSellerId,
                Status = entity.Status,
                RequestedDate = entity.RequestedDate,
                ApprovedDate = entity.ApprovedDate,
                CompletedDate = entity.CompletedDate,
                Remarks = entity.Remarks
            };
        }

        public async Task<TransferOrderDto?> UpdateAsync(int id, UpdateTransferOrderDto dto)
        {
            var entity = new TransferOrder
            {
                Id = id,
                TransferNumber = dto.TransferNumber,
                CustomerOrderId = dto.CustomerOrderId,
                FromLocationType = dto.FromLocationType,
                ToLocationType = dto.ToLocationType,
                FromDistributorId = dto.FromDistributorId,
                ToDistributorId = dto.ToDistributorId,
                FromSellerId = dto.FromSellerId,
                ToSellerId = dto.ToSellerId,
                Status = dto.Status,
                RequestedDate = dto.RequestedDate,
                ApprovedDate = dto.ApprovedDate,
                CompletedDate = dto.CompletedDate,
                Remarks = dto.Remarks
            };

            var updated = await _repository.UpdateAsync(entity);

            if (updated == null)
                return null;

            return new TransferOrderDto
            {
                Id = updated.Id,
                TransferNumber = updated.TransferNumber,
                CustomerOrderId = updated.CustomerOrderId,
                FromLocationType = updated.FromLocationType,
                ToLocationType = updated.ToLocationType,
                FromDistributorId = updated.FromDistributorId,
                ToDistributorId = updated.ToDistributorId,
                FromSellerId = updated.FromSellerId,
                ToSellerId = updated.ToSellerId,
                Status = updated.Status,
                RequestedDate = updated.RequestedDate,
                ApprovedDate = updated.ApprovedDate,
                CompletedDate = updated.CompletedDate,
                Remarks = updated.Remarks
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}