using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderManagementServices.DTO;
using OrderManagementServices.OrderInterface;

namespace OrderManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrder_Repository _iorderRepository;

        public OrderController(IOrder_Repository iorderRepository)
        {
            _iorderRepository = iorderRepository;
        }

        /// <summary>
        /// Create a New Order
        /// </summary>
        /// <param name="orderCreateDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCreateDto orderCreateDto)
        {
            try
            {
                var order = await _iorderRepository.CreateOrderAsync(orderCreateDto);
                return CreatedAtAction(nameof(GetOrderById), new { orderId = order.OrderId }, order);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Get Order By OrderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpGet("{orderId}")]
        public async Task<IActionResult> GetOrderById(int orderId)
        {
            try
            {
                var order = await _iorderRepository.GetOrderByIdAsync(orderId);
                if (order == null)
                    return NotFound();

                return Ok(order);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Update Order By OrderId
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="orderUpdateDto"></param>
        /// <returns></returns>
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] OrderUpdateDto orderUpdateDto)
        {
            try
            {
                var updatedOrder = await _iorderRepository.UpdateOrderAsync(orderId, orderUpdateDto);
                return Ok(updatedOrder);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Delete Order
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        [HttpDelete("{orderId}")]
        public async Task<IActionResult> DeleteOrder(long orderId)
        {
            try
            {
                var deleted = await _iorderRepository.DeleteOrderAsync(orderId);
                if (!deleted)
                    return NotFound();

                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
