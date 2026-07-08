using CozyComfort.Application.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CozyComfort.Infrastructure.Repositories
{
    public class AvailabilityRequestRepository
        : IAvailabilityRequestRepository
    {

        private readonly ApplicationDbContext _context;


        public AvailabilityRequestRepository(
            ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<AvailabilityRequest>> GetAllAsync()
        {
            return await _context.AvailabilityRequests
                .Include(x => x.CustomerOrder)
                .Include(x => x.BlanketModel)
                .Include(x => x.Seller)
                .Include(x => x.Distributor)
                .ToListAsync();
        }



        public async Task<AvailabilityRequest?> GetByIdAsync(int id)
        {
            return await _context.AvailabilityRequests
                .FirstOrDefaultAsync(x => x.Id == id);
        }



        public async Task<AvailabilityRequest> AddAsync(
            AvailabilityRequest request)
        {
            await _context.AvailabilityRequests.AddAsync(request);

            await _context.SaveChangesAsync();

            return request;
        }



        public async Task UpdateAsync(
            AvailabilityRequest request)
        {
            _context.AvailabilityRequests.Update(request);

            await _context.SaveChangesAsync();
        }



        public async Task DeleteAsync(
            AvailabilityRequest request)
        {
            _context.AvailabilityRequests.Remove(request);

            await _context.SaveChangesAsync();
        }



        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.AvailabilityRequests
                .AnyAsync(x => x.Id == id);
        }
    }
}