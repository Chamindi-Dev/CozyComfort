using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Interfaces
{
    public interface IAvailabilityRequestRepository
    {
        Task<IEnumerable<AvailabilityRequest>> GetAllAsync();

        Task<AvailabilityRequest?> GetByIdAsync(int id);

        Task<AvailabilityRequest> AddAsync(
            AvailabilityRequest request);

        Task UpdateAsync(
            AvailabilityRequest request);

        Task DeleteAsync(
            AvailabilityRequest request);

        Task<bool> ExistsAsync(int id);
    }
}