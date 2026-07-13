using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class FactoryInventoriesController : ControllerBase
    {
        private readonly IFactoryInventoryService _factoryInventoryService;

        public FactoryInventoriesController(IFactoryInventoryService factoryInventoryService)
        {
            _factoryInventoryService = factoryInventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventories = await _factoryInventoryService.GetAllAsync();
            return Ok(inventories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inventory = await _factoryInventoryService.GetByIdAsync(id);

            if (inventory == null)
                return NotFound(new { message = "Factory Inventory not found." });

            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateFactoryInventoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdInventory = await _factoryInventoryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdInventory.Id },
                createdInventory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateFactoryInventoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _factoryInventoryService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound(new { message = "Factory Inventory not found." });

            return Ok(new
            {
                message = "Factory Inventory updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _factoryInventoryService.DeleteAsync(id);

            if (!deleted)
                return NotFound(new { message = "Factory Inventory not found." });

            return Ok(new
            {
                message = "Factory Inventory deleted successfully."
            });
        }
    }
}