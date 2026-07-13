using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SellersController : ControllerBase
    {
        private readonly ISellerService _sellerService;

        public SellersController(ISellerService sellerService)
        {
            _sellerService = sellerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var sellers = await _sellerService.GetAllAsync();
            return Ok(sellers);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var seller = await _sellerService.GetByIdAsync(id);

            if (seller == null)
                return NotFound();

            return Ok(seller);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Seller seller)
        {
            var result = await _sellerService.AddAsync(seller);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Seller seller)
        {
            if (id != seller.Id)
                return BadRequest();

            var existing = await _sellerService.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            await _sellerService.UpdateAsync(seller);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _sellerService.GetByIdAsync(id);

            if (existing == null)
                return NotFound();

            await _sellerService.DeleteAsync(id);

            return NoContent();
        }
    }
}