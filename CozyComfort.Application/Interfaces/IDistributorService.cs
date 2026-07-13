using CozyComfort.Domain.DTOs.Distributor;

namespace CozyComfort.Application.Interfaces
{
    public interface IDistributorService
    {
        Task<IEnumerable<DistributorDto>> GetAllAsync();

        Task<DistributorDto?> GetByIdAsync(int id);

        Task<DistributorDto> CreateAsync(CreateDistributorDto dto);

        Task<bool> UpdateAsync(int id, UpdateDistributorDto dto);

        Task<bool> DeleteAsync(int id);
    }
}