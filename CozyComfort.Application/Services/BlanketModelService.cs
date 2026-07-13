using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.DTOs.BlanketModel;
using CozyComfort.Domain.Entities;
using CozyComfort.Infrastructure.Interfaces;

namespace CozyComfort.Application.Services
{
    public class BlanketModelService : IBlanketModelService
    {
        private readonly IBlanketModelRepository _repository;

        public BlanketModelService(IBlanketModelRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<BlanketModelDto>> GetAllAsync()
        {
            var models = await _repository.GetAllAsync();

            return models.Select(x => new BlanketModelDto
            {
                Id = x.Id,
                SKU = x.SKU,
                ModelName = x.ModelName,
                MaterialId = x.MaterialId,
                MaterialName = x.Material?.MaterialName,
                Size = x.Size,
                Color = x.Color,
                UnitPrice = x.UnitPrice,
                IsActive = x.IsActive
            });
        }

        public async Task<BlanketModelDto?> GetByIdAsync(int id)
        {
            var model = await _repository.GetByIdAsync(id);

            if (model == null)
                return null;

            return new BlanketModelDto
            {
                Id = model.Id,
                SKU = model.SKU,
                ModelName = model.ModelName,
                MaterialId = model.MaterialId,
                MaterialName = model.Material?.MaterialName,
                Size = model.Size,
                Color = model.Color,
                UnitPrice = model.UnitPrice,
                IsActive = model.IsActive
            };
        }

        public async Task<BlanketModelDto> CreateAsync(CreateBlanketModelDto dto)
        {
            var entity = new BlanketModel
            {
                SKU = dto.SKU,
                ModelName = dto.ModelName,
                MaterialId = dto.MaterialId,
                Size = dto.Size,
                Color = dto.Color,
                UnitPrice = dto.UnitPrice,
                IsActive = dto.IsActive
            };

            var result = await _repository.CreateAsync(entity);

            return new BlanketModelDto
            {
                Id = result.Id,
                SKU = result.SKU,
                ModelName = result.ModelName,
                MaterialId = result.MaterialId,
                MaterialName = result.Material?.MaterialName,
                Size = result.Size,
                Color = result.Color,
                UnitPrice = result.UnitPrice,
                IsActive = result.IsActive
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateBlanketModelDto dto)
        {
            var entity = await _repository.GetByIdAsync(id);

            if (entity == null)
                return false;

            entity.SKU = dto.SKU;
            entity.ModelName = dto.ModelName;
            entity.MaterialId = dto.MaterialId;
            entity.Size = dto.Size;
            entity.Color = dto.Color;
            entity.UnitPrice = dto.UnitPrice;
            entity.IsActive = dto.IsActive;

            var result = await _repository.UpdateAsync(entity);

            return result != null;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}