using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistributorsController : ControllerBase
    {
        private readonly IDistributorRepository _repository;


        public DistributorsController(
            IDistributorRepository repository)
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
            var distributor =
                await _repository.GetByIdAsync(id);


            if (distributor == null)
                return NotFound();


            return Ok(distributor);
        }





        [HttpPost]
        public async Task<IActionResult> Create(
            Distributor distributor)
        {
            var result =
                await _repository.CreateAsync(distributor);


            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            Distributor distributor)
        {
            if (id != distributor.Id)
                return BadRequest();


            var result =
                await _repository.UpdateAsync(distributor);


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
