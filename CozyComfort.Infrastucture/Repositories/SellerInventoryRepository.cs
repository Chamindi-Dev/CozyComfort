using CozyComfort.Application.Repositories;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class SellerInventoryRepository : ISellerInventoryRepository
    {
        private readonly ApplicationDbContext _context;

        public SellerInventoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SellerInventory>> GetAllAsync()
        {
            return await _context.SellerInventories
                .Include(x => x.Seller)
                .Include(x => x.BlanketModel)
                .ToListAsync();
        }

        public async Task<SellerInventory?> GetByIdAsync(int id)
        {
            return await _context.SellerInventories
                .Include(x => x.Seller)
                .Include(x => x.BlanketModel)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(SellerInventory sellerInventory)
        {
            await _context.SellerInventories.AddAsync(sellerInventory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SellerInventory sellerInventory)
        {
            _context.SellerInventories.Update(sellerInventory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var sellerInventory = await _context.SellerInventories.FindAsync(id);

            if (sellerInventory != null)
            {
                _context.SellerInventories.Remove(sellerInventory);
                await _context.SaveChangesAsync();
            }
        }
    }
}