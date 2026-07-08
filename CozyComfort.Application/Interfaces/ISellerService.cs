using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface ISellerService
    {
        Task<IEnumerable<Seller>> GetAllAsync();
        Task<Seller?> GetByIdAsync(int id);
        Task<Seller> AddAsync(Seller seller);
        Task UpdateAsync(Seller seller);
        Task DeleteAsync(int id);
    }
}