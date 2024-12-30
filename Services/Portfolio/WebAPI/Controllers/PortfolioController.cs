using Microsoft.AspNetCore.Mvc;
using PortfolioService.Application.DTOs;
using PortfolioService.Application.Services;
using PortfolioService.Domain.Exceptions;
using Shared.Events;
using Shared.Messaging;
using System;
using System.Threading.Tasks;

namespace PortfolioService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IEventBus _eventBus;
        private readonly IPortfolioAppService _portfolioAppService; // Injected dependency

        public PortfolioController(IEventBus eventBus, IPortfolioAppService portfolioAppService)
        {
            _eventBus = eventBus;
            _portfolioAppService = portfolioAppService;
        }

        // Publish Deposit Request
        [HttpPost("publish-deposit-request")]
        public IActionResult PublishDepositRequest([FromBody] DepositFundsRequest request)
        {
            try
            {
                // Publish DepositRequestedEvent
                var depositEvent = new DepositRequestedEvent
                {
                    UserId = request.UserId,
                    Amount = request.Amount,
                    RequestedAt = DateTime.UtcNow
                };
                _eventBus.Publish(depositEvent, "PortfolioExchange");

                return Ok(new { Message = "Deposit request published successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Publish Withdrawal Request
        [HttpPost("publish-withdrawal-request")]
        public async Task<IActionResult> PublishWithdrawalRequest([FromBody] WithdrawFundsRequest request)
        {
            try
            {
                // Call the application service to handle the withdrawal request
                await _portfolioAppService.WithdrawFundsAsync(request.UserId, request.Amount);

                return Ok(new { Message = "Withdrawal processed successfully." });
            }
            catch (InsufficientFundsException)
            {
                return BadRequest(new { Message = "Insufficient balance for withdrawal." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
