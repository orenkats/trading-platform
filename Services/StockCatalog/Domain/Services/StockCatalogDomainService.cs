using StockCatalogService.Domain.Entities;
using StockCatalogService.Domain.Interfaces;
using StockCatalogService.Infrastructure.Repositories;

namespace StockCatalogService.Domain.Services
{
    public class StockCatalogDomainService : IStockCatalogDomainService
    {
        private readonly IStockCatalogRepository _stockCatalogRepository;

        public StockCatalogDomainService(IStockCatalogRepository stockCatalogRepository)
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
