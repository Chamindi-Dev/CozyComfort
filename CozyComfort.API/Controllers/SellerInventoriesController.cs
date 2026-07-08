using CozyComfort.Application.Interfaces;
using CozyComfort.Data.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellerInventoriesController : ControllerBase
    {
        private readonly ISellerInventoryService _sellerInventoryService;

        public SellerInventoriesController(ISellerInventoryService sellerInventoryService)
        {
            _sellerInventoryService = sellerInventoryService;
        }

        // GET: api/SellerInventories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellerInventoryDto>>> GetAll()
        {
            var inventories = await _sellerInventoryService.GetAllAsync();
            return Ok(inventories);
        }

        // GET: api/SellerInventories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SellerInventoryDto>> GetById(int id)
        {
            var inventory = await _sellerInventoryService.GetByIdAsync(id);

            if (inventory == null)
                return NotFound();

            return Ok(inventory);
        }

        // POST: api/SellerInventories
        [HttpPost]
        public async Task<ActionResult<SellerInventoryDto>> Create(CreateSellerInventoryDto dto)
        {
            var createdInventory = await _sellerInventoryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdInventory.Id },
                createdInventory);
        }

        // PUT: api/SellerInventories/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSellerInventoryDto dto)
        {
            var updated = await _sellerInventoryService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/SellerInventories/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _sellerInventoryService.DeleteAsync(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}