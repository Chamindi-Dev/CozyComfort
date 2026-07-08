using CozyComfort.Application.DTOs;
using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;

namespace CozyComfort.Application.Services
{
    public class ProductionCapacityService : IProductionCapacityService
    {
        private readonly IProductionCapacityRepository _repository;


        public ProductionCapacityService(
            IProductionCapacityRepository repository)
        {
            _repository = repository;
        }



        public async Task<IEnumerable<ProductionCapacityDto>> GetAllAsync()
        {
            var data = await _repository.GetAllAsync();

            return data.Select(x => new ProductionCapacityDto
            {
                Id = x.Id,
                BlanketModelId = x.BlanketModelId,
                DailyCapacity = x.DailyCapacity,
                WeeklyCapacity = x.WeeklyCapacity,
                CurrentPendingQuantity = x.CurrentPendingQuantity,
                LeadTimeDays = x.LeadTimeDays,
                LastUpdated = x.LastUpdated
            });
        }



        public async Task<ProductionCapacityDto?> GetByIdAsync(int id)
        {
            var data = await _repository.GetByIdAsync(id);

            if (data == null)
                return null;


            return new ProductionCapacityDto
            {
                Id = data.Id,
                BlanketModelId = data.BlanketModelId,
                DailyCapacity = data.DailyCapacity,
                WeeklyCapacity = data.WeeklyCapacity,
                CurrentPendingQuantity = data.CurrentPendingQuantity,
                LeadTimeDays = data.LeadTimeDays,
                LastUpdated = data.LastUpdated
            };
        }



        public async Task<ProductionCapacityDto> CreateAsync(
            CreateProductionCapacityDto dto)
        {

            var entity = new ProductionCapacity
            {
                BlanketModelId = dto.BlanketModelId,
                DailyCapacity = dto.DailyCapacity,
                WeeklyCapacity = dto.WeeklyCapacity,
                CurrentPendingQuantity = dto.CurrentPendingQuantity,
                LeadTimeDays = dto.LeadTimeDays,
                LastUpdated = DateTime.Now
            };


            await _repository.AddAsync(entity);


            return new ProductionCapacityDto
            {
                Id = entity.Id,
                BlanketModelId = entity.BlanketModelId,
                DailyCapacity = entity.DailyCapacity,
                WeeklyCapacity = entity.WeeklyCapacity,
                CurrentPendingQuantity = entity.CurrentPendingQuantity,
                LeadTimeDays = entity.LeadTimeDays,
                LastUpdated = entity.LastUpdated
            };
        }




        public async Task<bool> UpdateAsync(
            int id,
            UpdateProductionCapacityDto dto)
        {

            var existing = await _repository.GetByIdAsync(id);


            if (existing == null)
                return false;



            existing.BlanketModelId = dto.BlanketModelId;
            existing.DailyCapacity = dto.DailyCapacity;
            existing.WeeklyCapacity = dto.WeeklyCapacity;
            existing.CurrentPendingQuantity = dto.CurrentPendingQuantity;
            existing.LeadTimeDays = dto.LeadTimeDays;
            existing.LastUpdated = DateTime.Now;



            await _repository.UpdateAsync(existing);


            return true;
        }





        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);


            if (existing == null)
                return false;


            await _repository.DeleteAsync(id);


            return true;
        }
    }
}