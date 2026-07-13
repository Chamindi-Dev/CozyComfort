using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.DTOs.Distributor;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DistributorsController : ControllerBase
    {
        private readonly IDistributorService _service;

        public DistributorsController(IDistributorService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DistributorDto>>> GetAll()
        {
            var distributors = await _service.GetAllAsync();
            return Ok(distributors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DistributorDto>> GetById(int id)
        {
            var distributor = await _service.GetByIdAsync(id);

            if (distributor == null)
                return NotFound();

            return Ok(distributor);
        }


        [HttpPost]
        public async Task<ActionResult<DistributorDto>> Create(CreateDistributorDto dto)
        {
            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<DistributorDto>> Update(
            int id,
            UpdateDistributorDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}