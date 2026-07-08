using CozyComfort.Application.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class DistributorRepository : IDistributorRepository
    {
        private readonly ApplicationDbContext _context;


        public DistributorRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<Distributor>> GetAllAsync()
        {
            return await _context.Distributors
                .ToListAsync();
        }



        public async Task<Distributor?> GetByIdAsync(int id)
        {
            return await _context.Distributors
                .FirstOrDefaultAsync(x => x.Id == id);
        }




        public async Task<Distributor> CreateAsync(
            Distributor distributor)
        {
            _context.Distributors.Add(distributor);

            await _context.SaveChangesAsync();

            return distributor;
        }




        public async Task<Distributor?> UpdateAsync(
            Distributor distributor)
        {
            var existing = await _context.Distributors
                .FindAsync(distributor.Id);


            if (existing == null)
                return null;


            existing.DistributorName = distributor.DistributorName;
            existing.Email = distributor.Email;
            existing.Phone = distributor.Phone;
            existing.Address = distributor.Address;
            existing.ServiceArea = distributor.ServiceArea;


            await _context.SaveChangesAsync();


            return existing;
        }




        public async Task<bool> DeleteAsync(int id)
        {
            var distributor = await _context.Distributors
                .FindAsync(id);


            if (distributor == null)
                return false;


            _context.Distributors.Remove(distributor);

            await _context.SaveChangesAsync();


            return true;
        }

        public Task AddAsync(Distributor distributor)
        {
            throw new NotImplementedException();
        }
    }
}