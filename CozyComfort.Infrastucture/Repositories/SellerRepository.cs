using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class SellerRepository : ISellerRepository
    {
        private readonly ApplicationDbContext _context;


        public SellerRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Seller>> GetAllAsync()
        {
            return await _context.Sellers
                .Include(x => x.Distributor)
                .ToListAsync();
        }


        public async Task<Seller?> GetByIdAsync(int id)
        {
            return await _context.Sellers
                .Include(x => x.Distributor)
                .FirstOrDefaultAsync(x => x.Id == id);
        }


        public async Task<Seller> CreateAsync(
            Seller seller)
        {
            _context.Sellers.Add(seller);

            await _context.SaveChangesAsync();

            return seller;
        }


        public async Task<Seller?> UpdateAsync(
            Seller seller)
        {
            var existing = await _context.Sellers
                .FindAsync(seller.Id);


            if (existing == null)
                return null;


            existing.DistributorId = seller.DistributorId;
            existing.SellerName = seller.SellerName;
            existing.StoreType = seller.StoreType;
            existing.Email = seller.Email;
            existing.Phone = seller.Phone;
            existing.Address = seller.Address;


            await _context.SaveChangesAsync();


            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var seller = await _context.Sellers
                .FindAsync(id);


            if (seller == null)
                return false;


            _context.Sellers.Remove(seller);

            await _context.SaveChangesAsync();


            return true;
        }

        public Task AddAsync(Seller seller)
        {
            throw new NotImplementedException();
        }
    }
}