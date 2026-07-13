using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class StockMovementRepository : IStockMovementRepository
    {
        private readonly ApplicationDbContext _context;

        public StockMovementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<StockMovement>> GetAllAsync()
        {
            return await _context.StockMovements
                .Include(x => x.BlanketModel)
                .Include(x => x.FactoryInventory)
                .Include(x => x.DistributorInventory)
                .Include(x => x.SellerInventory)
                .Include(x => x.TransferOrder)
                .Include(x => x.CustomerOrder)
                .ToListAsync();
        }

        public async Task<StockMovement?> GetByIdAsync(int id)
        {
            return await _context.StockMovements
                .Include(x => x.BlanketModel)
                .Include(x => x.FactoryInventory)
                .Include(x => x.DistributorInventory)
                .Include(x => x.SellerInventory)
                .Include(x => x.TransferOrder)
                .Include(x => x.CustomerOrder)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(StockMovement stockMovement)
        {
            _context.StockMovements.Add(stockMovement);
            await _context.SaveChangesAsync();
        }

        public async Task<StockMovement?> UpdateAsync(StockMovement stockMovement)
        {
            var existing = await _context.StockMovements.FindAsync(stockMovement.Id);

            if (existing == null)
                return null;

            existing.BlanketModelId = stockMovement.BlanketModelId;
            existing.FactoryInventoryId = stockMovement.FactoryInventoryId;
            existing.DistributorInventoryId = stockMovement.DistributorInventoryId;
            existing.SellerInventoryId = stockMovement.SellerInventoryId;
            existing.MovementType = stockMovement.MovementType;
            existing.Quantity = stockMovement.Quantity;
            existing.TransferOrderId = stockMovement.TransferOrderId;
            existing.CustomerOrderId = stockMovement.CustomerOrderId;
            existing.CreatedAt = stockMovement.CreatedAt;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.StockMovements.FindAsync(id);

            if (existing == null)
                return false;

            _context.StockMovements.Remove(existing);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}