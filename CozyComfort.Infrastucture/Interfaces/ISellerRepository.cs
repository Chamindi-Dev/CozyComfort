using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface ISellerRepository
    {
        Task<IEnumerable<Seller>> GetAllAsync();

        Task<Seller?> GetByIdAsync(int id);

        Task<Seller> CreateAsync(Seller seller);

        Task<Seller?> UpdateAsync(Seller seller);

        Task<bool> DeleteAsync(int id);
        Task AddAsync(Seller seller);
    }
}
