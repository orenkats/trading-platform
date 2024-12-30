using Microsoft.AspNetCore.Mvc;
using PortfolioService.Application.Services;
using PortfolioService.Application.DTOs;
using PortfolioService.Application.Queries;
using System;
using System.Threading.Tasks;

namespace PortfolioService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioAppService _portfolioAppService;
        private readonly GetAccountBalanceQuery _getAccountBalanceQuery;

        public PortfolioController(
            IPortfolioAppService portfolioAppService,
            GetAccountBalanceQuery getAccountBalanceQuery)
        {
            _portfolioAppService = portfolioAppService;
            _getAccountBalanceQuery = getAccountBalanceQuery;
        }

        // Deposit Funds
        [HttpPost("deposit")]
        public async Task<IActionResult> DepositFunds([FromBody] DepositFundsRequest request)
        {
            try
            {
                await _portfolioAppService.DepositFundsAsync(request.UserId, request.Amount);
                return Ok(new { Message = "Funds deposited successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Withdraw Funds
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawFunds([FromBody] WithdrawFundsRequest request)
        {
            try
            {
                await _portfolioAppService.WithdrawFundsAsync(request.UserId, request.Amount);
                return Ok(new { Message = "Funds withdrawn successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Get Account Balance
        [HttpGet("balance/{userId}")]
        public async Task<IActionResult> GetAccountBalance(Guid userId)
        {
            try
            {
                var balance = await _getAccountBalanceQuery.ExecuteAsync(userId);
                return Ok(new { Balance = balance });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
