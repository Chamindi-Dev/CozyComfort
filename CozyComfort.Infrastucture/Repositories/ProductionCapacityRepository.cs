using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class ProductionCapacityRepository : IProductionCapacityRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductionCapacityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductionCapacity>> GetAllAsync()
        {
            return await _context.ProductionCapacities
                .Include(x => x.BlanketModel)
                .ToListAsync();
        }

        public async Task<ProductionCapacity?> GetByIdAsync(int id)
        {
            return await _context.ProductionCapacities
                .Include(x => x.BlanketModel)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(ProductionCapacity productionCapacity)
        {
            await _context.ProductionCapacities.AddAsync(productionCapacity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductionCapacity productionCapacity)
        {
            _context.ProductionCapacities.Update(productionCapacity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var productionCapacity = await _context.ProductionCapacities.FindAsync(id);

            if (productionCapacity != null)
            {
                _context.ProductionCapacities.Remove(productionCapacity);
                await _context.SaveChangesAsync();
            }
        }
    }
}