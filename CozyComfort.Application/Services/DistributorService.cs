using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Services
{
    public class DistributorService : IDistributorService
    {
        private readonly IDistributorRepository _repository;

        public DistributorService(IDistributorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Distributor>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Distributor?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Distributor> AddAsync(Distributor distributor)
        {
            await _repository.AddAsync(distributor);
            return distributor;
        }

        public async Task UpdateAsync(Distributor distributor)
        {
            await _repository.UpdateAsync(distributor);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}