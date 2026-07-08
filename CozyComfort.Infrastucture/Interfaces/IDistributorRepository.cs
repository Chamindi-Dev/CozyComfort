using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface IDistributorRepository
    {
        Task<IEnumerable<Distributor>> GetAllAsync();

        Task<Distributor?> GetByIdAsync(int id);

        Task<Distributor> CreateAsync(Distributor distributor);

        Task<Distributor?> UpdateAsync(Distributor distributor);

        Task<bool> DeleteAsync(int id);
        Task AddAsync(Distributor distributor);
    }
}