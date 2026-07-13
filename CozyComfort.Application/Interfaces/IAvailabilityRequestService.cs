using CozyComfort.Domain.DTOs.AvailabilityRequest;

namespace CozyComfort.Application.Interfaces
{
    public interface IAvailabilityRequestService
    {
        Task<IEnumerable<AvailabilityRequestDto>> GetAllAsync();

        Task<AvailabilityRequestDto?> GetByIdAsync(int id);

        Task<AvailabilityRequestDto> CreateAsync(
            CreateAvailabilityRequestDto dto);

        Task UpdateAsync(
            UpdateAvailabilityRequestDto dto);

        Task DeleteAsync(int id);
    }
}