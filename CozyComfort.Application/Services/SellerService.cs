using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Services
{
    public class SellerService : ISellerService
    {
        private readonly ISellerRepository _repository;

        public SellerService(ISellerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Seller>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Seller?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Seller> AddAsync(Seller seller)
        {
            await _repository.AddAsync(seller);
            return seller;
        }

        public async Task UpdateAsync(Seller seller)
        {
            await _repository.UpdateAsync(seller);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}