using StockCatalogService.Domain.Entities;

namespace StockCatalogService.Domain.Interfaces
{
    public interface IStockCatalogDomainService
    {
        Task<StockCatalog?> GetStockByIdAsync(Guid stockId);
        Task<IEnumerable<StockCatalog>> GetAllStocksAsync();
        Task AddStockAsync(StockCatalog stockCatalog);
        Task UpdateStockAsync(StockCatalog stockCatalog);
        Task DeleteStockAsync(Guid stockId);
    }
}
