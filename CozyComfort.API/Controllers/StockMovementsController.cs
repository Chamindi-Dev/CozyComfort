using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.DTOs.StockMovement;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class StockMovementsController : ControllerBase
    {
        private readonly IStockMovementService _service;

        public StockMovementsController(IStockMovementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockMovementDto>>> GetAll()
        {
            var stockMovements = await _service.GetAllAsync();
            return Ok(stockMovements);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<StockMovementDto>> GetById(int id)
        {
            var stockMovement = await _service.GetByIdAsync(id);

            if (stockMovement == null)
                return NotFound();

            return Ok(stockMovement);
        }

        [HttpPost]
        public async Task<ActionResult<StockMovementDto>> Create(CreateStockMovementDto dto)
        {
            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

   
        [HttpPut("{id}")]
        public async Task<ActionResult<StockMovementDto>> Update(
            int id,
            UpdateStockMovementDto dto)
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