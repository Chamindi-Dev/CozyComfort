using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class DistributorInventoriesController : ControllerBase
    {
        private readonly IDistributorInventoryService _distributorInventoryService;

        public DistributorInventoriesController(IDistributorInventoryService distributorInventoryService)
        {
            _distributorInventoryService = distributorInventoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventories = await _distributorInventoryService.GetAllAsync();
            return Ok(inventories);
        }

      
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inventory = await _distributorInventoryService.GetByIdAsync(id);

            if (inventory == null)
            {
                return NotFound(new
                {
                    message = "Distributor Inventory not found."
                });
            }

            return Ok(inventory);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateDistributorInventoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdInventory = await _distributorInventoryService.CreateAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdInventory.Id },
                createdInventory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateDistributorInventoryDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updated = await _distributorInventoryService.UpdateAsync(id, dto);

            if (!updated)
            {
                return NotFound(new
                {
                    message = "Distributor Inventory not found."
                });
            }

            return Ok(new
            {
                message = "Distributor Inventory updated successfully."
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _distributorInventoryService.DeleteAsync(id);

            if (!deleted)
            {
                return NotFound(new
                {
                    message = "Distributor Inventory not found."
                });
            }

            return Ok(new
            {
                message = "Distributor Inventory deleted successfully."
            });
        }
    }
}
