using CozyComfort.Application.DTOs.StockMovement;
using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Services
{
    public class StockMovementService : IStockMovementService
    {
        private readonly IStockMovementRepository _repository;

        public StockMovementService(IStockMovementRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<StockMovementDto>> GetAllAsync()
        {
            var movements = await _repository.GetAllAsync();

            return movements.Select(x => new StockMovementDto
            {
                Id = x.Id,
                BlanketModelId = x.BlanketModelId,
                FactoryInventoryId = x.FactoryInventoryId,
                DistributorInventoryId = x.DistributorInventoryId,
                SellerInventoryId = x.SellerInventoryId,
                MovementType = x.MovementType,
                Quantity = x.Quantity,
                TransferOrderId = x.TransferOrderId,
                CustomerOrderId = x.CustomerOrderId,
                CreatedAt = x.CreatedAt
            });
        }

        public async Task<StockMovementDto?> GetByIdAsync(int id)
        {
            var movement = await _repository.GetByIdAsync(id);

            if (movement == null)
                return null;

            return new StockMovementDto
            {
                Id = movement.Id,
                BlanketModelId = movement.BlanketModelId,
                FactoryInventoryId = movement.FactoryInventoryId,
                DistributorInventoryId = movement.DistributorInventoryId,
                SellerInventoryId = movement.SellerInventoryId,
                MovementType = movement.MovementType,
                Quantity = movement.Quantity,
                TransferOrderId = movement.TransferOrderId,
                CustomerOrderId = movement.CustomerOrderId,
                CreatedAt = movement.CreatedAt
            };
        }

        public async Task<StockMovementDto> CreateAsync(CreateStockMovementDto dto)
        {
            var entity = new StockMovement
            {
                BlanketModelId = dto.BlanketModelId,
                FactoryInventoryId = dto.FactoryInventoryId,
                DistributorInventoryId = dto.DistributorInventoryId,
                SellerInventoryId = dto.SellerInventoryId,
                MovementType = dto.MovementType,
                Quantity = dto.Quantity,
                TransferOrderId = dto.TransferOrderId,
                CustomerOrderId = dto.CustomerOrderId,
                CreatedAt = dto.CreatedAt
            };

            await _repository.AddAsync(entity);

            return new StockMovementDto
            {
                Id = entity.Id,
                BlanketModelId = entity.BlanketModelId,
                FactoryInventoryId = entity.FactoryInventoryId,
                DistributorInventoryId = entity.DistributorInventoryId,
                SellerInventoryId = entity.SellerInventoryId,
                MovementType = entity.MovementType,
                Quantity = entity.Quantity,
                TransferOrderId = entity.TransferOrderId,
                CustomerOrderId = entity.CustomerOrderId,
                CreatedAt = entity.CreatedAt
            };
        }

        public async Task<StockMovementDto?> UpdateAsync(int id, UpdateStockMovementDto dto)
        {
            var entity = new StockMovement
            {
                Id = id,
                BlanketModelId = dto.BlanketModelId,
                FactoryInventoryId = dto.FactoryInventoryId,
                DistributorInventoryId = dto.DistributorInventoryId,
                SellerInventoryId = dto.SellerInventoryId,
                MovementType = dto.MovementType,
                Quantity = dto.Quantity,
                TransferOrderId = dto.TransferOrderId,
                CustomerOrderId = dto.CustomerOrderId,
                CreatedAt = dto.CreatedAt
            };

            var updated = await _repository.UpdateAsync(entity);

            if (updated == null)
                return null;

            return new StockMovementDto
            {
                Id = updated.Id,
                BlanketModelId = updated.BlanketModelId,
                FactoryInventoryId = updated.FactoryInventoryId,
                DistributorInventoryId = updated.DistributorInventoryId,
                SellerInventoryId = updated.SellerInventoryId,
                MovementType = updated.MovementType,
                Quantity = updated.Quantity,
                TransferOrderId = updated.TransferOrderId,
                CustomerOrderId = updated.CustomerOrderId,
                CreatedAt = updated.CreatedAt
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}