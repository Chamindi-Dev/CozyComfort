using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class BlanketModelsController : ControllerBase
    {
        private readonly IBlanketModelRepository _repository;


        public BlanketModelsController(
            IBlanketModelRepository repository)
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
            var model = await _repository.GetByIdAsync(id);


            if (model == null)
                return NotFound();


            return Ok(model);
        }





        [HttpPost]
        public async Task<IActionResult> Create(
            BlanketModel model)
        {
            var result =
                await _repository.CreateAsync(model);


            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }





        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            BlanketModel model)
        {
            if (id != model.Id)
                return BadRequest();


            var result =
                await _repository.UpdateAsync(model);


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