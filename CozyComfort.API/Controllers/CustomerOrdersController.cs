using CozyComfort.Application.DTOs;
using CozyComfort.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;


namespace CozyComfort.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrdersController : ControllerBase
    {

        private readonly ICustomerOrderService _service;



        public CustomerOrdersController(
            ICustomerOrderService service)
        {
            _service = service;
        }





        // GET: api/CustomerOrders

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var result = await _service.GetAllAsync();


            return Ok(result);

        }







        // GET: api/CustomerOrders/1

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {

            var result = await _service.GetByIdAsync(id);



            if (result == null)
                return NotFound();



            return Ok(result);

        }








        // POST: api/CustomerOrders

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









        // PUT: api/CustomerOrders/1

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








        // DELETE: api/CustomerOrders/1

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