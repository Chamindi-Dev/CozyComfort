using CozyComfort.Domain.Entities;

namespace CozyComfort.Infrastructure.Interfaces
{
    public interface IBlanketModelRepository
    {
        Task<IEnumerable<BlanketModel>> GetAllAsync();

        Task<BlanketModel?> GetByIdAsync(int id);

        Task<BlanketModel> CreateAsync(BlanketModel blanketModel);

        Task<BlanketModel?> UpdateAsync(BlanketModel blanketModel);

        Task<bool> DeleteAsync(int id);
    }
}