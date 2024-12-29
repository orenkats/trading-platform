using Microsoft.AspNetCore.Mvc;
using OrderService.Data.Entities;
using OrderService.Logic;

namespace OrderService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderLogic _orderLogic;

        public OrderController(IOrderLogic orderLogic)
        {
            _orderLogic = orderLogic;
        }

        [HttpPost]
        [Route("place-order")]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            await _orderLogic.PlaceOrderAsync(order);
            return Ok("Order placed successfully.");
        }

        [HttpGet]
        [Route("get-orders/{userId}")]
        public async Task<IActionResult> GetOrders(Guid userId)
        {
            var orders = await _orderLogic.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }
    }
}
