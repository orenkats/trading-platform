using Microsoft.EntityFrameworkCore;
using StockCatalogService.Domain.Entities;
using StockCatalogService.Infrastructure.Persistence;
using Shared.Persistence;

namespace StockCatalogService.Infrastructure.Repositories
{
    public class StockCatalogRepository : Repository<StockCatalog>, IStockCatalogRepository
    {
        private new readonly StockCatalogDbContext _context;

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
