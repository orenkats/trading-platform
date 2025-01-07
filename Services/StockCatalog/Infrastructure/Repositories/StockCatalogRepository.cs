using Microsoft.EntityFrameworkCore;
using StockCatalogService.Domain.Entities;
using StockCatalogService.Domain.Interfaces;
using StockCatalogService.Infrastructure.DbContexts;

namespace StockCatalogService.Infrastructure.Repositories
{
    public class StockCatalogRepository : IStockCatalogRepository
    {
        private readonly StockCatalogDbContext _context;

        public StockCatalogRepository(StockCatalogDbContext context)
        {
            _context = context;
        }

        public async Task<StockCatalog?> GetByIdAsync(Guid id) => await _context.StockCatalogs.FindAsync(id);

        public async Task<IEnumerable<StockCatalog>> GetAllAsync() => await _context.StockCatalogs.ToListAsync();

        public async Task AddAsync(StockCatalog entity)
        {
            await _context.StockCatalogs.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(StockCatalog entity)
        {
            _context.StockCatalogs.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _context.StockCatalogs.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(string stockSymbol)
        {
            return await _context.StockCatalogs.AnyAsync(s => s.StockSymbol == stockSymbol);
        }
    }
}
