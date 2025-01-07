using Microsoft.AspNetCore.Mvc;
using OrderService.Domain.Entities;
using OrderService.Domain.Interfaces;

namespace OrderService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderDomainService _orderDomainService;

        public OrderController(IOrderDomainService orderDomainService)
        {
            _orderDomainService = orderDomainService;
        }

        [HttpPost]
        [Route("place-order")]
        public async Task<IActionResult> PlaceOrder(Order order)
        {
            await _orderDomainService.PlaceOrderAsync(order);
            return Ok("Order placed successfully.");
        }

        [HttpGet]
        [Route("get-orders/{userId}")]
        public async Task<IActionResult> GetOrders(Guid userId)
        {
            var orders = await _orderDomainService.GetOrdersByUserIdAsync(userId);
            return Ok(orders);
        }
    }
}
