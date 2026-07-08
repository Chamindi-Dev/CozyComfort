using CozyComfort.Application.DTOs;
using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Services
{
    public class DistributorInventoryService : IDistributorInventoryService
    {
        private readonly IDistributorInventoryRepository _repository;

        public DistributorInventoryService(IDistributorInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DistributorInventoryDto>> GetAllAsync()
        {
            var inventories = await _repository.GetAllAsync();

            return inventories.Select(x => new DistributorInventoryDto
            {
                Id = x.Id,
                DistributorId = x.DistributorId,
                BlanketModelId = x.BlanketModelId,
                QuantityOnHand = x.QuantityOnHand,
                ReservedQuantity = x.ReservedQuantity,
                AvailableQuantity = x.AvailableQuantity,
                LastUpdated = x.LastUpdated
            });
        }

        public async Task<DistributorInventoryDto?> GetByIdAsync(int id)
        {
            var inventory = await _repository.GetByIdAsync(id);

            if (inventory == null)
                return null;

            return new DistributorInventoryDto
            {
                Id = inventory.Id,
                DistributorId = inventory.DistributorId,
                BlanketModelId = inventory.BlanketModelId,
                QuantityOnHand = inventory.QuantityOnHand,
                ReservedQuantity = inventory.ReservedQuantity,
                AvailableQuantity = inventory.AvailableQuantity,
                LastUpdated = inventory.LastUpdated
            };
        }

        public async Task<DistributorInventoryDto> CreateAsync(CreateDistributorInventoryDto dto)
        {
            var inventory = new DistributorInventory
            {
                DistributorId = dto.DistributorId,
                BlanketModelId = dto.BlanketModelId,
                QuantityOnHand = dto.QuantityOnHand,
                ReservedQuantity = dto.ReservedQuantity,
                LastUpdated = DateTime.Now
            };

            await _repository.AddAsync(inventory);

            return new DistributorInventoryDto
            {
                Id = inventory.Id,
                DistributorId = inventory.DistributorId,
                BlanketModelId = inventory.BlanketModelId,
                QuantityOnHand = inventory.QuantityOnHand,
                ReservedQuantity = inventory.ReservedQuantity,
                AvailableQuantity = inventory.AvailableQuantity,
                LastUpdated = inventory.LastUpdated
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateDistributorInventoryDto dto)
        {
            var inventory = await _repository.GetByIdAsync(id);

            if (inventory == null)
                return false;

            inventory.DistributorId = dto.DistributorId;
            inventory.BlanketModelId = dto.BlanketModelId;
            inventory.QuantityOnHand = dto.QuantityOnHand;
            inventory.ReservedQuantity = dto.ReservedQuantity;
            inventory.LastUpdated = DateTime.Now;

            await _repository.UpdateAsync(inventory);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var inventory = await _repository.GetByIdAsync(id);

            if (inventory == null)
                return false;

            await _repository.DeleteAsync(id);

            return true;
        }
    }
}