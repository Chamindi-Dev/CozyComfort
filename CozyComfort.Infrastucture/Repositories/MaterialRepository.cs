
using CozyComfort.Application.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.API.Repositories
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly ApplicationDbContext _context;

        public MaterialRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Material>> GetAllAsync()
        {
            return await _context.Materials
                .OrderBy(m => m.MaterialName)
                .ToListAsync();
        }

        public async Task<Material?> GetByIdAsync(int id)
        {
            return await _context.Materials
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Material> CreateAsync(Material material)
        {
            _context.Materials.Add(material);

            await _context.SaveChangesAsync();

            return material;
        }

        public async Task<Material?> UpdateAsync(Material material)
        {
            var existingMaterial = await _context.Materials.FindAsync(material.Id);

            if (existingMaterial == null)
                return null;

            existingMaterial.MaterialName = material.MaterialName;
            existingMaterial.Description = material.Description;

            await _context.SaveChangesAsync();

            return existingMaterial;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var material = await _context.Materials.FindAsync(id);

            if (material == null)
                return false;

            _context.Materials.Remove(material);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> MaterialExistsAsync(string materialName)
        {
            return await _context.Materials
                .AnyAsync(m => m.MaterialName == materialName);
        }

        public async Task<bool> MaterialExistsAsync(int id)
        {
            return await _context.Materials
                .AnyAsync(m => m.Id == id);
        }

        public Task<Material> AddAsync(Material material)
        {
            throw new NotImplementedException();
        }
    }
}