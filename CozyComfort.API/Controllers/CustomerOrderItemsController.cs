using CozyComfort.Application.Interfaces;
using CozyComfort.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CozyComfort.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerOrderItemsController : ControllerBase
    {
        private readonly ICustomerOrderItemService _service;


        public CustomerOrderItemsController(
            ICustomerOrderItemService service)
        {
            _service = service;
        }



        // GET: api/customerorderitems
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();

            return Ok(items);
        }



        // GET: api/customerorderitems/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);


            if (item == null)
            {
                return NotFound(
                    "Customer order item not found");
            }


            return Ok(item);
        }



        // GET: api/customerorderitems/order/5
        [HttpGet("order/{customerOrderId}")]
        public async Task<IActionResult> GetByOrderId(
            int customerOrderId)
        {
            var items =
                await _service.GetByOrderIdAsync(customerOrderId);


            return Ok(items);
        }



        // POST: api/customerorderitems
        [HttpPost]
        public async Task<IActionResult> Create(
            CustomerOrderItem customerOrderItem)
        {
            try
            {
                var created =
                    await _service.CreateAsync(customerOrderItem);


                return CreatedAtAction(
                    nameof(GetById),
                    new { id = created.Id },
                    created);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        // PUT: api/customerorderitems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            CustomerOrderItem customerOrderItem)
        {
            if (id != customerOrderItem.Id)
            {
                return BadRequest(
                    "Id mismatch");
            }


            try
            {
                await _service.UpdateAsync(customerOrderItem);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }



        // DELETE: api/customerorderitems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteAsync(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}