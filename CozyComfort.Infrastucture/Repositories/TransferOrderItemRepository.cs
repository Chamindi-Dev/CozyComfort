using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class TransferOrderItemRepository : ITransferOrderItemRepository
    {
        private readonly ApplicationDbContext _context;

        public TransferOrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransferOrderItem>> GetAllAsync()
        {
            return await _context.TransferOrderItems
                .Include(x => x.TransferOrder)
                .Include(x => x.BlanketModel)
                .ToListAsync();
        }

        public async Task<TransferOrderItem?> GetByIdAsync(int id)
        {
            return await _context.TransferOrderItems
                .Include(x => x.TransferOrder)
                .Include(x => x.BlanketModel)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(TransferOrderItem transferOrderItem)
        {
            _context.TransferOrderItems.Add(transferOrderItem);
            await _context.SaveChangesAsync();
        }

        public async Task<TransferOrderItem?> UpdateAsync(TransferOrderItem transferOrderItem)
        {
            var existing = await _context.TransferOrderItems.FindAsync(transferOrderItem.Id);

            if (existing == null)
                return null;

            existing.TransferOrderId = transferOrderItem.TransferOrderId;
            existing.BlanketModelId = transferOrderItem.BlanketModelId;
            existing.Quantity = transferOrderItem.Quantity;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _context.TransferOrderItems.FindAsync(id);

            if (existing == null)
                return false;

            _context.TransferOrderItems.Remove(existing);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}