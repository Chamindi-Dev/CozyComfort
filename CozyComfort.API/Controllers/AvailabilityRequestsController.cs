using CozyComfort.Application.DTOs.AvailabilityRequest;
using CozyComfort.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CozyComfort.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityRequestsController : ControllerBase
    {

        private readonly IAvailabilityRequestService _service;


        public AvailabilityRequestsController(
            IAvailabilityRequestService service)
        {
            _service = service;
        }



        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }



        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _service.GetByIdAsync(id);


            if (result == null)
                return NotFound();


            return Ok(result);
        }



        [HttpPost]
        public async Task<IActionResult> Create(
            CreateAvailabilityRequestDto dto)
        {
            var result =
                await _service.CreateAsync(dto);


            return Ok(result);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateAvailabilityRequestDto dto)
        {

            dto.Id = id;

            await _service.UpdateAsync(dto);

            return NoContent();
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
    }
}