using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SellerInventoriesController : ControllerBase
    {
        private readonly ISellerInventoryService _sellerInventoryService;

        public SellerInventoriesController(ISellerInventoryService sellerInventoryService)
        {
            _sellerInventoryService = sellerInventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SellerInventoryDto>>> GetAll()
        {
            var inventories = await _sellerInventoryService.GetAllAsync();
            return Ok(inventories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SellerInventoryDto>> GetById(int id)
        {
            var inventory = await _sellerInventoryService.GetByIdAsync(id);

            if (inventory == null)
                return NotFound();

            return Ok(inventory);
        }

        [HttpPost]
        public async Task<ActionResult<SellerInventoryDto>> Create(CreateSellerInventoryDto dto)
        {
            var createdInventory = await _sellerInventoryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdInventory.Id },
                createdInventory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateSellerInventoryDto dto)
        {
            var updated = await _sellerInventoryService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound();

            return NoContent();
        }

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