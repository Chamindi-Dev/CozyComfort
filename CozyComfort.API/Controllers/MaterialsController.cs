using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly IMaterialRepository _repository;


        public MaterialsController(IMaterialRepository repository)
        {
            _repository = repository;
        }



        // GET: api/materials
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var materials = await _repository.GetAllAsync();

            return Ok(materials);
        }



        // GET: api/materials/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var material = await _repository.GetByIdAsync(id);


            if (material == null)
                return NotFound();


            return Ok(material);
        }



        // POST: api/materials
        [HttpPost]
        public async Task<IActionResult> Create(Material material)
        {
            var result = await _repository.CreateAsync(material);

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }



        // PUT: api/materials/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            Material material)
        {
            if (id != material.Id)
                return BadRequest();


            var result = await _repository.UpdateAsync(material);


            if (result == null)
                return NotFound();


            return Ok(result);
        }




        // DELETE: api/materials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _repository.DeleteAsync(id);


            if (!result)
                return NotFound();


            return NoContent();
        }
    }
}
