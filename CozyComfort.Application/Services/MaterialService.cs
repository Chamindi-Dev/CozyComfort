using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;

        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }

        public async Task<IEnumerable<Material>> GetAllAsync()
        {
            return await _materialRepository.GetAllAsync();
        }

        public async Task<Material?> GetByIdAsync(int id)
        {
            return await _materialRepository.GetByIdAsync(id);
        }

        public async Task<Material> AddAsync(Material material)
        {
            return await _materialRepository.AddAsync(material);
        }

        public async Task<Material?> UpdateAsync(int id, Material material)
        {
            var existing = await _materialRepository.GetByIdAsync(id);

            if (existing == null)
                return null;

            existing.MaterialName = material.MaterialName;
            existing.Description = material.Description;

            await _materialRepository.UpdateAsync(existing);

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _materialRepository.GetByIdAsync(id);

            if (existing == null)
                return false;

            await _materialRepository.DeleteAsync(id);

            return true;
        }
    }
}