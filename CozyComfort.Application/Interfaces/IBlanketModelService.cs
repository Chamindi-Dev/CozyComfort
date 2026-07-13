using CozyComfort.Domain.DTOs.BlanketModel;

namespace CozyComfort.Application.Interfaces
{
    public interface IBlanketModelService
    {
        Task<IEnumerable<BlanketModelDto>> GetAllAsync();

        Task<BlanketModelDto?> GetByIdAsync(int id);

        Task<BlanketModelDto> CreateAsync(CreateBlanketModelDto dto);

        Task<bool> UpdateAsync(int id, UpdateBlanketModelDto dto);

        Task<bool> DeleteAsync(int id);
    }
}