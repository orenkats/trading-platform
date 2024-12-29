using Microsoft.AspNetCore.Mvc;
using StockCatalogService.Data.Entities;
using StockCatalogService.Logic;
using StockCatalogService.Data.Repositories;
namespace StockCatalogService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/stock")]
    public class StockCatalogController : ControllerBase
    {
        private readonly IStockCatalogLogic _stockCatalogLogic;
        private readonly IStockCatalogRepository _stockCatalogRepository;

        public StockCatalogController(IStockCatalogLogic stockCatalogLogic,IStockCatalogRepository stockCatalogRepository)
        {
            _stockCatalogLogic = stockCatalogLogic;
            _stockCatalogRepository = stockCatalogRepository;
        }

        [HttpGet]
        [Route("get-stock/{id}")]
        public async Task<IActionResult> GetStockById(Guid id)
        {
            var stock = await _stockCatalogLogic.GetStockByIdAsync(id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }

        [HttpGet]
        [Route("get-all-stocks")]
        public async Task<IActionResult> GetAllStocks()
        {
            var stocks = await _stockCatalogLogic.GetAllStocksAsync();
            return Ok(stocks);
        }

        [HttpPost]
        [Route("add-stock")]
        public async Task<IActionResult> AddStock(StockCatalog stockCatalog)
        {
            await _stockCatalogLogic.AddStockAsync(stockCatalog);
            return CreatedAtAction(nameof(GetStockById), new { id = stockCatalog.Id }, stockCatalog);
        }

        [HttpPut]
        [Route("update-stock/{id}")]
        public async Task<IActionResult> UpdateStock(Guid id, StockCatalog stockCatalog)
        {
            if (id != stockCatalog.Id)
            {
                return BadRequest("Stock ID mismatch.");
            }

            await _stockCatalogLogic.UpdateStockAsync(stockCatalog);
            return NoContent();
        }

        [HttpDelete]
        [Route("delete-stock/{id}")]
        public async Task<IActionResult> DeleteStock(Guid id)
        {
            await _stockCatalogLogic.DeleteStockAsync(id);
            return NoContent();
        }

        [HttpGet]
        [Route("validate/{stockSymbol}")]
        public async Task<IActionResult> ValidateStockSymbol(string stockSymbol)
        {
            var stockExists = await _stockCatalogRepository.ExistsAsync(stockSymbol);
            return Ok(stockExists);
        }
    }
}
