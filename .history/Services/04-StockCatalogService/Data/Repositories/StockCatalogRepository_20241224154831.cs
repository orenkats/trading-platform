using Microsoft.EntityFrameworkCore;
using StockCatalogService.Data.Entities;
using Shared.Persistence;

namespace StockCatalogService.Data.Repositories
{
    public class StockCatalogRepository : Repository<StockCatalog>, IStockCatalogRepository
    {
        private readonly StockCatalogDbContext _context;

        public StockCatalogRepository(StockCatalogDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> ExistsAsync(string stockSymbol)
        {
            return await _context.StockCatalogs.AnyAsync(s => s.StockSymbol == stockSymbol);
        }
    }
}
