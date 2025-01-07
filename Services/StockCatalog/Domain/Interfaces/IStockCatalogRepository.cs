using StockCatalogService.Domain.Entities;

namespace StockCatalogService.Domain.Interfaces
{
    public interface IStockCatalogRepository
    {
        Task<StockCatalog?> GetByIdAsync(Guid id);
        Task<IEnumerable<StockCatalog>> GetAllAsync();
        Task AddAsync(StockCatalog entity);
        Task UpdateAsync(StockCatalog entity);
        Task DeleteAsync(Guid id);
        Task<bool> ExistsAsync(string stockSymbol);
    }
}
