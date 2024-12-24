using StockCatalogService.Data.Entities;
using StockCatalogService.Data.Repositories;

namespace StockCatalogService.Logic
{
    public class StockCatalogLogic : IStockCatalogLogic
    {
        private readonly IStockCatalogRepository _stockCatalogRepository;

        public StockCatalogLogic(IStockCatalogRepository stockCatalogRepository)
        {
            _stockCatalogRepository = stockCatalogRepository;
        }

        public async Task AddStockAsync(StockCatalog stockCatalog)
        {
            // Add the stock to the database
            await _stockCatalogRepository.AddAsync(stockCatalog);
        }

        public async Task<StockCatalog?> GetStockByIdAsync(Guid stockId)
        {
            return await _stockCatalogRepository.GetByIdAsync(stockId);
        }

        public async Task<IEnumerable<StockCatalog>> GetAllStocksAsync()
        {
            return await _stockCatalogRepository.GetAllAsync();
        }

        public async Task UpdateStockAsync(StockCatalog stockCatalog)
        {
            await _stockCatalogRepository.UpdateAsync(stockCatalog);
        }

        public async Task DeleteStockAsync(Guid stockId)
        {
            await _stockCatalogRepository.DeleteAsync(stockId);
        }
    }
}
