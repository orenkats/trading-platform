using Microsoft.AspNetCore.Mvc;
using PortfolioService.Application.EventHandlers;
using PortfolioService.Application.DTOs;
using Shared.Events;

namespace PortfolioService.WebAPI.Controllers
{
    [Route("api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly DepositRequestedEventHandler _depositRequestedEventHandler;
        private readonly WithdrawalRequestedEventHandler _withdrawalRequestedEventHandler;

        public PortfolioController(
            DepositRequestedEventHandler depositRequestedEventHandler,
            WithdrawalRequestedEventHandler withdrawalRequestedEventHandler)
        {
            _depositRequestedEventHandler = depositRequestedEventHandler;
            _withdrawalRequestedEventHandler = withdrawalRequestedEventHandler;
        }

        // Process Deposit Request
        [HttpPost("deposit-request")]
        public async Task<IActionResult> ProcessDepositRequest([FromBody] DepositFundsRequest request)
        {
            try
            {
                var depositEvent = new DepositRequestedEvent
                {
                    EventId = Guid.NewGuid(),
                    UserId = request.UserId,
                    Amount = request.Amount,
                    Status = "Requested",
                    Timestamp = DateTime.UtcNow,
                    
                };

                await _depositRequestedEventHandler.HandleAsync(depositEvent);

                return Ok(new { Message = "Deposit request processed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Process Withdrawal Request
        [HttpPost("withdraw-request")]
        public async Task<IActionResult> ProcessWithdrawalRequest([FromBody] WithdrawFundsRequest request)
        {
            try
            {
                var withdrawalEvent = new WithdrawalRequestedEvent
                {
                    EventId = Guid.NewGuid(),
                    UserId = request.UserId,
                    Amount = request.Amount,
                    Status = "Requested",
                    Timestamp = DateTime.UtcNow,
                    
                };

                await _withdrawalRequestedEventHandler.HandleAsync(withdrawalEvent);

                return Ok(new { Message = "Withdrawal request processed successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
