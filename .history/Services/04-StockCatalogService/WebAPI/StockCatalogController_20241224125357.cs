using Microsoft.AspNetCore.Mvc;
using StockCatalogService.Data.Entities;
using StockCatalogService.Data.Repositories;

namespace StockCatalogService.WebAPI.Controllers
{
    [ApiController]
    [Route("api/stock-catalog")]
    public class StockCatalogController : ControllerBase
    {
        private readonly IStockCatalogRepository _stockCatalogRepository;

        public StockCatalogController(IStockCatalogRepository stockCatalogRepository)
        {
            _stockCatalogRepository = stockCatalogRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockCatalogRepository.GetAllAsync();
            return Ok(stocks);
        }

        [HttpPost]
        public async Task<IActionResult> AddStock([FromBody] StockCatalog stock)
        {
            await _stockCatalogRepository.AddAsync(stock);
            return CreatedAtAction(nameof(GetAll), stock);
        }
    }
}
