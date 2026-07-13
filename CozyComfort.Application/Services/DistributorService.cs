using CozyComfort.Infrastructure.Interfaces;
using CozyComfort.Domain.DTOs.Distributor;
using CozyComfort.Domain.Entities;
using CozyComfort.Application.Interfaces;


namespace CozyComfort.Application.Services
{
    public class DistributorService : IDistributorService
    {
        private readonly IDistributorRepository _repository;

        public DistributorService(IDistributorRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DistributorDto>> GetAllAsync()
        {
            var distributors = await _repository.GetAllAsync();

            return distributors.Select(x => new DistributorDto
            {
                Id = x.Id,
                DistributorName = x.DistributorName,
                Email = x.Email,
                Phone = x.Phone,
                Address = x.Address,
                ServiceArea = x.ServiceArea,
                CreatedAt = x.CreatedAt
            });
        }

        public async Task<DistributorDto?> GetByIdAsync(int id)
        {
            var distributor = await _repository.GetByIdAsync(id);

            if (distributor == null)
                return null;

            return new DistributorDto
            {
                Id = distributor.Id,
                DistributorName = distributor.DistributorName,
                Email = distributor.Email,
                Phone = distributor.Phone,
                Address = distributor.Address,
                ServiceArea = distributor.ServiceArea,
                CreatedAt = distributor.CreatedAt
            };
        }

        public async Task<DistributorDto> CreateAsync(CreateDistributorDto dto)
        {
            var distributor = new Distributor
            {
                DistributorName = dto.DistributorName,
                Email = dto.Email,
                Phone = dto.Phone,
                Address = dto.Address,
                ServiceArea = dto.ServiceArea,
                CreatedAt = DateTime.Now
            };

            await _repository.AddAsync(distributor);

            return new DistributorDto
            {
                Id = distributor.Id,
                DistributorName = distributor.DistributorName,
                Email = distributor.Email,
                Phone = distributor.Phone,
                Address = distributor.Address,
                ServiceArea = distributor.ServiceArea,
                CreatedAt = distributor.CreatedAt
            };
        }

        public async Task<bool> UpdateAsync(int id, UpdateDistributorDto dto)
        {
            var distributor = await _repository.GetByIdAsync(id);

            if (distributor == null)
                return false;

            distributor.DistributorName = dto.DistributorName;
            distributor.Email = dto.Email;
            distributor.Phone = dto.Phone;
            distributor.Address = dto.Address;
            distributor.ServiceArea = dto.ServiceArea;

            await _repository.UpdateAsync(distributor);

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var distributor = await _repository.GetByIdAsync(id);

            if (distributor == null)
                return false;

            await _repository.DeleteAsync(id);

            return true;
        }
    }
}