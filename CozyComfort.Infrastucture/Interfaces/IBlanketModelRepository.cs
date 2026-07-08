using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface IBlanketModelRepository
    {
        Task<IEnumerable<BlanketModel>> GetAllAsync();

        Task<BlanketModel?> GetByIdAsync(int id);

        Task<BlanketModel> CreateAsync(BlanketModel blanketModel);

        Task<BlanketModel?> UpdateAsync(BlanketModel blanketModel);

        Task<bool> DeleteAsync(int id);
        Task<BlanketModel> AddAsync(BlanketModel blanketModel);
    }
}