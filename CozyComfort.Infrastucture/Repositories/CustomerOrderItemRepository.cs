using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Domain.Entities;
using CozyComfort.Data;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class CustomerOrderItemRepository : ICustomerOrderItemRepository
    {
        private readonly ApplicationDbContext _context;


        public CustomerOrderItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        // Get All Customer Order Items
        public async Task<IEnumerable<CustomerOrderItem>> GetAllAsync()
        {
            return await _context.CustomerOrderItems
                .Include(x => x.CustomerOrder)
                .Include(x => x.BlanketModel)
                .ToListAsync();
        }



        // Get By Id
        public async Task<CustomerOrderItem?> GetByIdAsync(int id)
        {
            return await _context.CustomerOrderItems
                .Include(x => x.CustomerOrder)
                .Include(x => x.BlanketModel)
                .FirstOrDefaultAsync(x => x.Id == id);
        }



        // Get Items By Customer Order Id
        public async Task<IEnumerable<CustomerOrderItem>> GetByOrderIdAsync(int customerOrderId)
        {
            return await _context.CustomerOrderItems
                .Where(x => x.CustomerOrderId == customerOrderId)
                .Include(x => x.BlanketModel)
                .ToListAsync();
        }



        // Add
        public async Task<CustomerOrderItem> AddAsync(
            CustomerOrderItem customerOrderItem)
        {
            await _context.CustomerOrderItems.AddAsync(customerOrderItem);

            await _context.SaveChangesAsync();

            return customerOrderItem;
        }



        // Update
        public async Task UpdateAsync(
            CustomerOrderItem customerOrderItem)
        {
            _context.CustomerOrderItems.Update(customerOrderItem);

            await _context.SaveChangesAsync();
        }



        // Delete
        public async Task DeleteAsync(
            CustomerOrderItem customerOrderItem)
        {
            _context.CustomerOrderItems.Remove(customerOrderItem);

            await _context.SaveChangesAsync();
        }



        // Exists
        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.CustomerOrderItems
                .AnyAsync(x => x.Id == id);
        }
    }
}