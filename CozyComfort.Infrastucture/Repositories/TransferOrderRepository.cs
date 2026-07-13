using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class TransferOrderRepository : ITransferOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public TransferOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TransferOrder>> GetAllAsync()
        {
            return await _context.TransferOrders
                .Include(x => x.CustomerOrder)
                .Include(x => x.FromDistributor)
                .Include(x => x.ToDistributor)
                .Include(x => x.FromSeller)
                .Include(x => x.ToSeller)
                .ToListAsync();
        }

        public async Task<TransferOrder?> GetByIdAsync(int id)
        {
            return await _context.TransferOrders
                .Include(x => x.CustomerOrder)
                .Include(x => x.FromDistributor)
                .Include(x => x.ToDistributor)
                .Include(x => x.FromSeller)
                .Include(x => x.ToSeller)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<TransferOrder> AddAsync(TransferOrder transferOrder)
        {
            _context.TransferOrders.Add(transferOrder);

            await _context.SaveChangesAsync();

            return transferOrder;
        }

        public async Task<TransferOrder?> UpdateAsync(TransferOrder transferOrder)
        {
            var existing = await _context.TransferOrders.FindAsync(transferOrder.Id);

            if (existing == null)
                return null;

            existing.TransferNumber = transferOrder.TransferNumber;
            existing.CustomerOrderId = transferOrder.CustomerOrderId;
            existing.FromLocationType = transferOrder.FromLocationType;
            existing.ToLocationType = transferOrder.ToLocationType;
            existing.FromDistributorId = transferOrder.FromDistributorId;
            existing.ToDistributorId = transferOrder.ToDistributorId;
            existing.FromSellerId = transferOrder.FromSellerId;
            existing.ToSellerId = transferOrder.ToSellerId;
            existing.Status = transferOrder.Status;
            existing.RequestedDate = transferOrder.RequestedDate;
            existing.ApprovedDate = transferOrder.ApprovedDate;
            existing.CompletedDate = transferOrder.CompletedDate;
            existing.Remarks = transferOrder.Remarks;

            await _context.SaveChangesAsync();

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var transferOrder = await _context.TransferOrders.FindAsync(id);

            if (transferOrder == null)
                return false;

            _context.TransferOrders.Remove(transferOrder);

            await _context.SaveChangesAsync();

            return true;
        }
    }
}