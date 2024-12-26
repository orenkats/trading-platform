using Microsoft.AspNetCore.Mvc;
using PortfolioService.Logic;
using System;
using System.Threading.Tasks;

namespace PortfolioService.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly IPortfolioLogic _portfolioLogic;

        public PortfolioController(IPortfolioLogic portfolioLogic)
        {
            _portfolioLogic = portfolioLogic;
        }

        // Deposit Funds
        [HttpPost("deposit")]
        public async Task<IActionResult> DepositFunds(Guid userId, decimal amount)
        {
            try
            {
                // Call the logic layer to deposit funds
                bool result = await _portfolioLogic.DepositFundsAsync(userId, amount);

                if (result)
                {
                    return Ok(new { Message = "Funds deposited successfully." });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to deposit funds." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        // Withdraw Funds
        [HttpPost("withdraw")]
        public async Task<IActionResult> WithdrawFunds(Guid userId, decimal amount)
        {
            try
            {
                // Call the logic layer to withdraw funds
                bool result = await _portfolioLogic.WithdrawFundsAsync(userId, amount);

                if (result)
                {
                    return Ok(new { Message = "Funds withdrawn successfully." });
                }
                else
                {
                    return BadRequest(new { Message = "Failed to withdraw funds." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
