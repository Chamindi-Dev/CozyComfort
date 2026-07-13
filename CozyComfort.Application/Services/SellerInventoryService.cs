using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Infrastructure.Repositories;
using CozyComfort.Domain.DTOs;
using CozyComfort.Domain.Entities;
using CozyComfort.Application.Interfaces;

namespace CozyComfort.Application.Services
{
    public class SellerInventoryService : ISellerInventoryService
    {
        private readonly ISellerInventoryRepository _repository;

        public SellerInventoryService(ISellerInventoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SellerInventoryDto>> GetAllAsync()
        {
            var inventories = await _repository.GetAllAsync();

            return inventories.Select(i => new SellerInventoryDto
            {
                Id = i.Id,
                SellerId = i.SellerId,
                BlanketModelId = i.BlanketModelId,
                QuantityOnHand = i.QuantityOnHand,
                ReservedQuantity = i.ReservedQuantity,
                AvailableQuantity = i.AvailableQuantity,
                LastUpdated = i.LastUpdated
            });
        }

        public async Task<SellerInventoryDto?> GetByIdAsync(int id)
        {
            var inventory = await _repository.GetByIdAsync(id);

            if (inventory == null)
                return null;

            return new SellerInventoryDto
            {
                Id = inventory.Id,
                SellerId = inventory.SellerId,
                BlanketModelId = inventory.BlanketModelId,
                QuantityOnHand = inventory.QuantityOnHand,
                ReservedQuantity = inventory.ReservedQuantity,
                AvailableQuantity = inventory.AvailableQuantity,
                LastUpdated = inventory.LastUpdated
            };
        }

        public async Task<SellerInventoryDto> CreateAsync(CreateSellerInventoryDto dto)
        {
            var inventory = new SellerInventory
            {
                SellerId = dto.SellerId,
                BlanketModelId = dto.BlanketModelId,
                QuantityOnHand = dto.QuantityOnHand,
                ReservedQuantity = dto.ReservedQuantity,
                LastUpdated = DateTime.Now
            };

            await _repository.AddAsync(inventory);

            return new SellerInventoryDto
            {
                Id = inventory.Id,
                SellerId = inventory.SellerId,
                BlanketModelId = inventory.BlanketModelId,
                QuantityOnHand = inventory.QuantityOnHand,
                ReservedQuantity = inventory.ReservedQuantity,
                AvailableQuantity = inventory.AvailableQuantity,
                LastUpdated = inventory.LastUpdated
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateSellerInventoryDto dto)
        {
            var inventory = await _repository.GetByIdAsync(id);

            if (inventory == null)
                return false;

            inventory.SellerId = dto.SellerId;
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