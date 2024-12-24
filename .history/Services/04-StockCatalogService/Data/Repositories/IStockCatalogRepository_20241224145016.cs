using Shared.Persistence;
using StockCatalogService.Data.Entities;

namespace StockCatalogService.Data.Repositories
{
    public interface IStockCatalogRepository : IRepository<StockCatalog>
    {
        // Additional methods specific to StockCatalog can be added here if needed
    }
}
