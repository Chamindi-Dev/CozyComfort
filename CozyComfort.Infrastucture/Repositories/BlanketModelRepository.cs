using CozyComfort.Application.Interfaces;
using CozyComfort.Data;
using CozyComfort.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CozyComfort.Infrastructure.Repositories
{
    public class BlanketModelRepository : IBlanketModelRepository
    {
        private readonly ApplicationDbContext _context;


        public BlanketModelRepository(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IEnumerable<BlanketModel>> GetAllAsync()
        {
            return await _context.BlanketModels
                .Include(x => x.Material)
                .ToListAsync();
        }



        public async Task<BlanketModel?> GetByIdAsync(int id)
        {
            return await _context.BlanketModels
                .Include(x => x.Material)
                .FirstOrDefaultAsync(x => x.Id == id);
        }



        public async Task<BlanketModel> CreateAsync(
            BlanketModel blanketModel)
        {
            _context.BlanketModels.Add(blanketModel);

            await _context.SaveChangesAsync();

            return blanketModel;
        }




        public async Task<BlanketModel?> UpdateAsync(
            BlanketModel blanketModel)
        {
            var existing = await _context.BlanketModels
                .FindAsync(blanketModel.Id);


            if (existing == null)
                return null;


            existing.SKU = blanketModel.SKU;
            existing.ModelName = blanketModel.ModelName;
            existing.MaterialId = blanketModel.MaterialId;
            existing.Size = blanketModel.Size;
            existing.Color = blanketModel.Color;
            existing.UnitPrice = blanketModel.UnitPrice;
            existing.IsActive = blanketModel.IsActive;


            await _context.SaveChangesAsync();


            return existing;
        }





        public async Task<bool> DeleteAsync(int id)
        {
            var model = await _context.BlanketModels
                .FindAsync(id);


            if (model == null)
                return false;


            _context.BlanketModels.Remove(model);

            await _context.SaveChangesAsync();


            return true;
        }

        public Task<BlanketModel> AddAsync(BlanketModel blanketModel)
        {
            throw new NotImplementedException();
        }
    }
}