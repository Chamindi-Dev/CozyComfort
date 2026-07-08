using CozyComfort.Application.DTOs.StockMovement;
using CozyComfort.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementsController : ControllerBase
    {
        private readonly IStockMovementService _service;

        public StockMovementsController(IStockMovementService service)
        {
            _service = service;
        }

        // GET: api/StockMovements
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockMovementDto>>> GetAll()
        {
            var stockMovements = await _service.GetAllAsync();
            return Ok(stockMovements);
        }

        // GET: api/StockMovements/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StockMovementDto>> GetById(int id)
        {
            var stockMovement = await _service.GetByIdAsync(id);

            if (stockMovement == null)
                return NotFound();

            return Ok(stockMovement);
        }

        // POST: api/StockMovements
        [HttpPost]
        public async Task<ActionResult<StockMovementDto>> Create(CreateStockMovementDto dto)
        {
            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        // PUT: api/StockMovements/5
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

        // DELETE: api/StockMovements/5
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