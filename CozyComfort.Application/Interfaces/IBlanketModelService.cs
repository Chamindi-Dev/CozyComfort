using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface IBlanketModelService
    {
        Task<IEnumerable<BlanketModel>> GetAllAsync();
        Task<BlanketModel?> GetByIdAsync(int id);
        Task<BlanketModel> AddAsync(BlanketModel blanketModel);
        Task<BlanketModel?> UpdateAsync(int id, BlanketModel blanketModel);
        Task<bool> DeleteAsync(int id);
    }
}