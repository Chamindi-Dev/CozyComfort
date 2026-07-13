using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class FactoryInventoryRepository : IFactoryInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public FactoryInventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<FactoryInventory>> GetAllAsync()
        {
            return await _context.FactoryInventories
                .Include(x => x.BlanketModel)
                .ToListAsync();
        }

        public async Task<FactoryInventory?> GetByIdAsync(int id)
        {
            return await _context.FactoryInventories
                .Include(x => x.BlanketModel)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(FactoryInventory inventory)
        {
            inventory.LastUpdated = DateTime.Now;

            await _context.FactoryInventories.AddAsync(inventory);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FactoryInventory inventory)
        {
            inventory.LastUpdated = DateTime.Now;

            _context.FactoryInventories.Update(inventory);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var inventory = await _context.FactoryInventories.FindAsync(id);

            if (inventory != null)
            {
                _context.FactoryInventories.Remove(inventory);

                await _context.SaveChangesAsync();
            }
        }
    }
}