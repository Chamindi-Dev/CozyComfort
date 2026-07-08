using CozyComfort.Application.DTOs.TransferOrderItem;
using CozyComfort.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferOrderItemsController : ControllerBase
    {
        private readonly ITransferOrderItemService _service;

        public TransferOrderItemsController(ITransferOrderItemService service)
        {
            _service = service;
        }

        // GET: api/TransferOrderItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransferOrderItemDto>>> GetAll()
        {
            var items = await _service.GetAllAsync();
            return Ok(items);
        }

        // GET: api/TransferOrderItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransferOrderItemDto>> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        // POST: api/TransferOrderItems
        [HttpPost]
        public async Task<ActionResult<TransferOrderItemDto>> Create(CreateTransferOrderItemDto dto)
        {
            var created = await _service.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        // PUT: api/TransferOrderItems/5
        [HttpPut("{id}")]
        public async Task<ActionResult<TransferOrderItemDto>> Update(
            int id,
            UpdateTransferOrderItemDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);

            if (updated == null)
                return NotFound();

            return Ok(updated);
        }

        // DELETE: api/TransferOrderItems/5
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