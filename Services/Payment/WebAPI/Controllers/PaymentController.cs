using Microsoft.AspNetCore.Mvc;
using PaymentService.Application;
using PaymentService.Application.DTOs;
using System;
using System.Threading.Tasks;

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

        // Deposit Funds
        [HttpPost("deposit")]
        public async Task<IActionResult> DepositFunds([FromBody] PaymentRequestDto request)
        {
            try
            {
                var response = await _paymentAppService.ProcessPaymentAsync(request.UserId, request.Amount);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Withdraw Funds
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawFunds([FromBody] PaymentRequestDto request)
        {
            try
            {
                var response = await _paymentAppService.ProcessWithdrawalAsync(request.UserId, request.Amount);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
