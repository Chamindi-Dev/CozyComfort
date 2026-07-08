using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface IDistributorService
    {
        Task<IEnumerable<Distributor>> GetAllAsync();
        Task<Distributor?> GetByIdAsync(int id);
        Task<Distributor> AddAsync(Distributor distributor);
        Task UpdateAsync(Distributor distributor);
        Task DeleteAsync(int id);
    }
}