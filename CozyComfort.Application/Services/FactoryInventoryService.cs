using CozyComfort.Application.DTOs;
using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Services
{
    public class FactoryInventoryService : IFactoryInventoryService
    {
        private readonly IFactoryInventoryRepository _repository;

        public FactoryInventoryService(IFactoryInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<FactoryInventoryDto>> GetAllAsync()
        {
            var inventories = await _repository.GetAllAsync();

            return inventories.Select(x => new FactoryInventoryDto
            {
                Id = x.Id,
                BlanketModelId = x.BlanketModelId,
                QuantityOnHand = x.QuantityOnHand,
                ReservedQuantity = x.ReservedQuantity,
                AvailableQuantity = x.AvailableQuantity,
                LastUpdated = x.LastUpdated
            });
        }

        public async Task<FactoryInventoryDto?> GetByIdAsync(int id)
        {
            var inventory = await _repository.GetByIdAsync(id);

            if (inventory == null)
                return null;

            return new FactoryInventoryDto
            {
                Id = inventory.Id,
                BlanketModelId = inventory.BlanketModelId,
                QuantityOnHand = inventory.QuantityOnHand,
                ReservedQuantity = inventory.ReservedQuantity,
                AvailableQuantity = inventory.AvailableQuantity,
                LastUpdated = inventory.LastUpdated
            };
        }

        public async Task<FactoryInventoryDto> CreateAsync(CreateFactoryInventoryDto dto)
        {
            var inventory = new FactoryInventory
            {
                BlanketModelId = dto.BlanketModelId,
                QuantityOnHand = dto.QuantityOnHand,
                ReservedQuantity = dto.ReservedQuantity,
                LastUpdated = DateTime.Now
            };

            await _repository.AddAsync(inventory);

            return new FactoryInventoryDto
            {
                Id = inventory.Id,
                BlanketModelId = inventory.BlanketModelId,
                QuantityOnHand = inventory.QuantityOnHand,
                ReservedQuantity = inventory.ReservedQuantity,
                AvailableQuantity = inventory.AvailableQuantity,
                LastUpdated = inventory.LastUpdated
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateFactoryInventoryDto dto)
        {
            var inventory = await _repository.GetByIdAsync(id);

            if (inventory == null)
                return false;

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