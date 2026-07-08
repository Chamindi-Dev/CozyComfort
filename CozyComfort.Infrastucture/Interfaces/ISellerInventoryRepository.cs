using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Repositories
{
    public interface ISellerInventoryRepository
    {
        Task<IEnumerable<SellerInventory>> GetAllAsync();

        Task<SellerInventory?> GetByIdAsync(int id);

        Task AddAsync(SellerInventory sellerInventory);

        Task UpdateAsync(SellerInventory sellerInventory);

        Task DeleteAsync(int id);
    }
}