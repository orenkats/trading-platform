using Shared.Persistence;
using StockCatalogService.Data.Entities;

namespace StockCatalogService.Data.Repositories
{
    public class StockCatalogRepository : Repository<StockCatalog>, IStockCatalogRepository
    {
        public StockCatalogRepository(StockCatalogDbContext context) : base(context)
        {
        }
    }
}
