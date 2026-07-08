using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SellersController : ControllerBase
    {
        private readonly ISellerRepository _repository;


        public SellersController(
            ISellerRepository repository)
        {
            _repository = repository;
        }





        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(
                await _repository.GetAllAsync()
            );
        }





        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var seller =
                await _repository.GetByIdAsync(id);


            if (seller == null)
                return NotFound();


            return Ok(seller);
        }





        [HttpPost]
        public async Task<IActionResult> Create(
            Seller seller)
        {
            var result =
                await _repository.CreateAsync(seller);


            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            Seller seller)
        {
            if (id != seller.Id)
                return BadRequest();


            var result =
                await _repository.UpdateAsync(seller);


            if (result == null)
                return NotFound();


            return Ok(result);
        }





        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result =
                await _repository.DeleteAsync(id);


            if (!result)
                return NotFound();


            return NoContent();
        }
    }
}
