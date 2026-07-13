using CozyComfort.Domain.DTOs.TransferOrderItem;
using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Domain.Entities;
using CozyComfort.Application.Interfaces;

namespace CozyComfort.Application.Services
{
    public class TransferOrderItemService : ITransferOrderItemService
    {
        private readonly ITransferOrderItemRepository _repository;

        public TransferOrderItemService(ITransferOrderItemRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TransferOrderItemDto>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();

            return items.Select(x => new TransferOrderItemDto
            {
                Id = x.Id,
                TransferOrderId = x.TransferOrderId,
                BlanketModelId = x.BlanketModelId,
                Quantity = x.Quantity
            });
        }

        public async Task<TransferOrderItemDto?> GetByIdAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);

            if (item == null)
                return null;

            return new TransferOrderItemDto
            {
                Id = item.Id,
                TransferOrderId = item.TransferOrderId,
                BlanketModelId = item.BlanketModelId,
                Quantity = item.Quantity
            };
        }

        public async Task<TransferOrderItemDto> CreateAsync(CreateTransferOrderItemDto dto)
        {
            var entity = new TransferOrderItem
            {
                TransferOrderId = dto.TransferOrderId,
                BlanketModelId = dto.BlanketModelId,
                Quantity = dto.Quantity
            };

            await _repository.AddAsync(entity);

            return new TransferOrderItemDto
            {
                Id = entity.Id,
                TransferOrderId = entity.TransferOrderId,
                BlanketModelId = entity.BlanketModelId,
                Quantity = entity.Quantity
            };
        }

        public async Task<TransferOrderItemDto?> UpdateAsync(int id, UpdateTransferOrderItemDto dto)
        {
            var entity = new TransferOrderItem
            {
                Id = id,
                TransferOrderId = dto.TransferOrderId,
                BlanketModelId = dto.BlanketModelId,
                Quantity = dto.Quantity
            };

            var updated = await _repository.UpdateAsync(entity);

            if (updated == null)
                return null;

            return new TransferOrderItemDto
            {
                Id = updated.Id,
                TransferOrderId = updated.TransferOrderId,
                BlanketModelId = updated.BlanketModelId,
                Quantity = updated.Quantity
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}