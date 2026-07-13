using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CozyComfort.Infrastructure.Repositories
{
    public class CustomerOrderRepository : ICustomerOrderRepository
    {

        private readonly ApplicationDbContext _context;


        public CustomerOrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<CustomerOrder>> GetAllAsync()
        {
            return await _context.CustomerOrders
                .Include(x => x.Customer)
                .Include(x => x.Seller)
                .ToListAsync();
        }




        public async Task<CustomerOrder?> GetByIdAsync(int id)
        {
            return await _context.CustomerOrders
                .Include(x => x.Customer)
                .Include(x => x.Seller)
                .FirstOrDefaultAsync(x => x.Id == id);
        }




        public async Task AddAsync(CustomerOrder order)
        {
            await _context.CustomerOrders.AddAsync(order);

            await _context.SaveChangesAsync();
        }





        public async Task UpdateAsync(CustomerOrder order)
        {
            _context.CustomerOrders.Update(order);

            await _context.SaveChangesAsync();
        }





        public async Task DeleteAsync(int id)
        {

            var order = await _context.CustomerOrders
                .FindAsync(id);


            if (order != null)
            {
                _context.CustomerOrders.Remove(order);

                await _context.SaveChangesAsync();
            }

        }

    }
}