using Microsoft.AspNetCore.Mvc;
using OMS.Models;
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

            foreach (var product in order.Products)
            {
                if (product.GTIN.Length != 14)
                {
                    return BadRequest($"GTIN for product must be exactly 14 characters.");
                }
            }

            try
            {
                // Create the order in the database
                _service.CreateOrder(order);

                // Generate a unique orderId and completion time
                var response = new
                {
                    omsId = order.OmsId,
                    orderId = Guid.NewGuid().ToString(),
                    expectedCompletionTime = new Random().Next(5000, 15000) // Simulate completion time
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public IActionResult UpdateOrder(string id, [FromBody] Models.Order updatedOrder)
        {
            if (updatedOrder == null || string.IsNullOrEmpty(updatedOrder.OmsId))
            {
                return BadRequest("Invalid updated order data.");
            }

            foreach (var product in updatedOrder.Products)
            {
                if (product.GTIN.Length != 14)
                {
                    return BadRequest($"GTIN for product must be exactly 14 characters.");
                }
            }

            try
            {
                var existingOrder = _service.GetOrderById(id);
                if (existingOrder == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                _service.UpdateOrder(id, updatedOrder);

                // Simulate response with the updated data
                var response = new
                {
                    omsId = updatedOrder.OmsId,
                    orderId = id,
                    expectedCompletionTime = new Random().Next(3000, 10000) // Simulate completion time
                };

                return Ok(response);
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes
                Console.WriteLine($"Error updating order: {ex.Message}");
                return StatusCode(500, $"An error occurred: {ex.Message}");
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
