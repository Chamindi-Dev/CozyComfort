using CozyComfort.Application.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class DistributorInventoryRepository : IDistributorInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public DistributorInventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DistributorInventory>> GetAllAsync()
        {
            return await _context.DistributorInventories
                .Include(x => x.Distributor)
                .Include(x => x.BlanketModel)
                .ToListAsync();
        }

        public async Task<DistributorInventory?> GetByIdAsync(int id)
        {
            return await _context.DistributorInventories
                .Include(x => x.Distributor)
                .Include(x => x.BlanketModel)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(DistributorInventory inventory)
        {
            inventory.LastUpdated = DateTime.Now;

            await _context.DistributorInventories.AddAsync(inventory);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(DistributorInventory inventory)
        {
            inventory.LastUpdated = DateTime.Now;

            _context.DistributorInventories.Update(inventory);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var inventory = await _context.DistributorInventories.FindAsync(id);

            if (inventory != null)
            {
                _context.DistributorInventories.Remove(inventory);

                await _context.SaveChangesAsync();
            }
        }
    }
}