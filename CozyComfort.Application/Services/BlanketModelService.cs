using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Services
{
    public class BlanketModelService : IBlanketModelService
    {
        private readonly IBlanketModelRepository _blanketModelRepository;

        public BlanketModelService(IBlanketModelRepository blanketModelRepository)
        {
            _blanketModelRepository = blanketModelRepository;
        }

        public async Task<IEnumerable<BlanketModel>> GetAllAsync()
        {
            return await _blanketModelRepository.GetAllAsync();
        }

        public async Task<BlanketModel?> GetByIdAsync(int id)
        {
            return await _blanketModelRepository.GetByIdAsync(id);
        }

        public async Task<BlanketModel> AddAsync(BlanketModel blanketModel)
        {
            return await _blanketModelRepository.AddAsync(blanketModel);
        }

        public async Task<BlanketModel?> UpdateAsync(int id, BlanketModel blanketModel)
        {
            var existing = await _blanketModelRepository.GetByIdAsync(id);

            if (existing == null)
                return null;

            existing.SKU = blanketModel.SKU;
            existing.ModelName = blanketModel.ModelName;
            existing.MaterialId = blanketModel.MaterialId;
            existing.Size = blanketModel.Size;
            existing.Color = blanketModel.Color;
            existing.UnitPrice = blanketModel.UnitPrice;
            existing.IsActive = blanketModel.IsActive;

            await _blanketModelRepository.UpdateAsync(existing);

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _blanketModelRepository.GetByIdAsync(id);

            if (existing == null)
                return false;

            await _blanketModelRepository.DeleteAsync(id);

            return true;
        }
    }
}