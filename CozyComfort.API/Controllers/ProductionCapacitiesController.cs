using CozyComfort.Application.DTOs;
using CozyComfort.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CozyComfort.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductionCapacitiesController : ControllerBase
    {

        private readonly IProductionCapacityService _service;


        public ProductionCapacitiesController(
            IProductionCapacityService service)
        {
            _service = service;
        }




        // GET: api/ProductionCapacities

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _service.GetAllAsync();

            return Ok(result);
        }




        // GET api/ProductionCapacities/1

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _service.GetByIdAsync(id);


            if (result == null)
                return NotFound();


            return Ok(result);
        }





        // POST api/ProductionCapacities

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateProductionCapacityDto dto)
        {

            var result = await _service.CreateAsync(dto);


            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result);
        }





        // PUT api/ProductionCapacities/1

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateProductionCapacityDto dto)
        {

            var result = await _service.UpdateAsync(id, dto);


            if (!result)
                return NotFound();


            return NoContent();
        }





        // DELETE api/ProductionCapacities/1

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            var result = await _service.DeleteAsync(id);


            if (!result)
                return NotFound();


            return NoContent();
        }

    }
}