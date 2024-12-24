using Shared.Persistence;
using StockCatalogService.Data.Entities;

namespace StockCatalogService.Data.Repositories
{
    public interface IStockCatalogRepository : IRepository<StockCatalog>
    {
        Task<bool> ExistsAsync(string stockSymbol);
    }
}
