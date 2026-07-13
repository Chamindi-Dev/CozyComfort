using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace CozyComfort.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CustomerOrdersController : ControllerBase
    {

        private readonly ICustomerOrderService _service;



        public CustomerOrdersController(
            ICustomerOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var result = await _service.GetAllAsync();


            return Ok(result);

        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _service.GetByIdAsync(id);



            if (result == null)
                return NotFound();



            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> Create(
            CreateCustomerOrderDto dto)
        {

            var result = await _service.CreateAsync(dto);



            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Id },
                result
            );

        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateCustomerOrderDto dto)
        {

            var result = await _service.UpdateAsync(id, dto);



            if (!result)
                return NotFound();



            return NoContent();

        }

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