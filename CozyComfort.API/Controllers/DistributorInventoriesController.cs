using CozyComfort.Application.DTOs;
using CozyComfort.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorInventoriesController : ControllerBase
    {
        private readonly IDistributorInventoryService _distributorInventoryService;

        public DistributorInventoriesController(IDistributorInventoryService distributorInventoryService)
        {
            _distributorInventoryService = distributorInventoryService;
        }

        // GET: api/DistributorInventories
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inventories = await _distributorInventoryService.GetAllAsync();
            return Ok(inventories);
        }

        // GET: api/DistributorInventories/5
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

        // POST: api/DistributorInventories
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

        // PUT: api/DistributorInventories/5
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

        // DELETE: api/DistributorInventories/5
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
