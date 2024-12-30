using Microsoft.AspNetCore.Mvc;
using PaymentService.Application.Services;
using PaymentService.Application.DTOs;

namespace PaymentService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentAppService _paymentAppService;

        public PaymentController(IPaymentAppService paymentAppService)
        {
            _paymentAppService = paymentAppService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessPayment([FromBody] PaymentRequestDto request)
        {
            if (request == null || request.Amount <= 0)
            {
                return BadRequest(new { Message = "Invalid payment request." });
            }

            var response = await _paymentAppService.ProcessPaymentAsync(request.UserId, request.Amount);

            return Ok(response);
        }
    }
}
