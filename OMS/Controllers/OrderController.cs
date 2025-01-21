using Microsoft.AspNetCore.Mvc;
using OMS.Services;
using System;

namespace OMS.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _service;

        public OrdersController(IOrderService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetOrders()
        {
            return Ok(_service.GetAllOrders());
        }

        [HttpPost]
        public IActionResult CreateOrder([FromBody] Models.Order order)
        {
            if (order == null || string.IsNullOrEmpty(order.OmsId))
            {
                return BadRequest("Invalid order data.");
            }

            _service.CreateOrder(order);
            return Ok(order);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOrder(string id, [FromBody] Models.Order updatedOrder)
        {
            try
            {
                _service.UpdateOrder(id, updatedOrder);
                return Ok(updatedOrder);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(string id)
        {
            try
            {
                _service.DeleteOrder(id);
                return NoContent();
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
