using StockCatalogService.Data.Entities;

namespace StockCatalogService.Logic
{
    public interface IStockCatalogLogic
    {
        Task<StockCatalog?> GetStockByIdAsync(Guid stockId);
        Task<IEnumerable<StockCatalog>> GetAllStocksAsync();
        Task AddStockAsync(StockCatalog stockCatalog);
        Task UpdateStockAsync(StockCatalog stockCatalog);
        Task DeleteStockAsync(Guid stockId);
    }
}
