using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Domain.DTOs.Material;
using CozyComfort.Domain.Entities;
using CozyComfort.Application.Interfaces;

namespace CozyComfort.Application.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _repository;

        public MaterialService(IMaterialRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<MaterialDto>> GetAllAsync()
        {
            var materials = await _repository.GetAllAsync();

            return materials.Select(x => new MaterialDto
            {
                Id = x.Id,
                MaterialName = x.MaterialName,
                Description = x.Description
            });
        }

        public async Task<MaterialDto?> GetByIdAsync(int id)
        {
            var material = await _repository.GetByIdAsync(id);

            if (material == null)
                return null;

            return new MaterialDto
            {
                Id = material.Id,
                MaterialName = material.MaterialName,
                Description = material.Description
            };
        }

        public async Task<MaterialDto> CreateAsync(CreateMaterialDto dto)
        {
            var material = new Material
            {
                MaterialName = dto.MaterialName,
                Description = dto.Description
            };

            await _repository.AddAsync(material);

            return new MaterialDto
            {
                Id = material.Id,
                MaterialName = material.MaterialName,
                Description = material.Description
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateMaterialDto dto)
        {
            var material = await _repository.GetByIdAsync(id);

            if (material == null)
                return false;

            material.MaterialName = dto.MaterialName;
            material.Description = dto.Description;

            await _repository.UpdateAsync(material);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var material = await _repository.GetByIdAsync(id);

            if (material == null)
                return false;

            await _repository.DeleteAsync(id);

            return true;
        }
    }
}